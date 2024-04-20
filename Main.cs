using EasyModbus;
using EngineNumber_checker;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using System.Text;
using System.Windows.Xps.Packaging;
using System.Xml;
using System.IO;
using System.Net.Sockets;
using System.Threading;


namespace SQS_station_cycle_time
{
    public partial class Main : Form
    {
        DateTime appStartTime;
        Logger logger = new Logger();

        //string connectionString = @"Data Source=localhost;Initial Catalog=SQS_SCT;User ID=SQS_SCT-user;Password=sqs;Trusted_Connection=True";
        string connectionString = @"Data Source=172.16.200.10;Initial Catalog=SQS_SCT;User ID=SQS_SCT-user;Password=sqs";

        ModbusServer ModServer;
        string ipAddress = "127.0.0.1";

        Stopwatch stopwatch = new Stopwatch();
        TimeSpan elapsedTMax;
        TimeSpan elapsedTMin;

        string pathPrintOut = "C:\\ProgramData\\Atlas Copco\\SQS\\LBMS\\work\\printout";
        string printOutResult = null;
        DateTime printOutDateTime;

        bool mem = false;
        bool newPID_mem = false;
        bool memServer = false;
        bool newIncompletePID_mem = false;

        CancellationTokenSource cts = new CancellationTokenSource();
        Thread TCPThread;

        TcpListener server = null;
        bool isListening = false;

        public Main()
        {
            InitializeComponent();
            this.FormClosing += Main_FormClosing;
            logger.Log("EngineNumber-Checker app Opened!");

            this.TopMost = true;

            appStartTime = DateTime.Now;
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if(isListening)
                    server.Stop();

                cts.Cancel();

                //  TCPThread.Abort();
                TCPThread = null;
            }
            catch (SocketException se)
            {
                logger.Log("SocketException on FormClosing: " + se);
            }
            catch (ObjectDisposedException oe)
            {
                logger.Log("ObjectDisposedException on FormClosing: " + oe);
            }
            catch (AggregateException ae)
            {
                logger.Log("AggregateException on FormClosing: " + ae);
            }
            catch (ThreadStateException te)
            {
                logger.Log("ThreadStateException on FormClosing: " + te);
            }


            logger.Log("SQS_SCT app closed");

        }

        public string ExtractTextFromXps(string filePath)
        {
            string[] fileNames = Directory.GetFiles(filePath);

            XpsDocument _xpsDocument = new XpsDocument(fileNames[fileNames.Length - 1], System.IO.FileAccess.Read);
            IXpsFixedDocumentSequenceReader fixedDocSeqReader = _xpsDocument.FixedDocumentSequenceReader;
            IXpsFixedDocumentReader _document = fixedDocSeqReader.FixedDocuments[0];
            StringBuilder _currentText = new StringBuilder();

            for (int pageCount = 0; pageCount < _document.FixedPages.Count; ++pageCount)
            {
                IXpsFixedPageReader _page = _document.FixedPages[pageCount];
                XmlReader _pageContentReader = _page.XmlReader;

                if (_pageContentReader != null)
                {
                    while (_pageContentReader.Read())
                    {
                        if (_pageContentReader.Name == "Glyphs")
                        {
                            if (_pageContentReader.HasAttributes)
                            {
                                string unicodeString = _pageContentReader.GetAttribute("UnicodeString");
                                if (unicodeString != null)
                                {
                                    _currentText.Append(unicodeString);
                                }
                            }
                        }
                    }
                }
            }

            return _currentText.ToString();
        }

        public void UpdateParameters()
        {
            string query = @"SELECT [STATION]
                                  ,[SCREEN]
                                  ,[ET_MAX]
                                  ,[ET_MIN]
                              FROM [SQS_SCT].[dbo].[PARAMETERS]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        elapsedTMax = reader.GetTimeSpan(2);
                        elapsedTMin = reader.GetTimeSpan(3);
                    }
                }
            }
        }

        public void InsertValuesDB(TimeSpan elapsedTime)
        {
            UpdateParameters();

            string productID = ExtractTextFromXps(pathPrintOut);

            string result = null;

            if (elapsedTime > elapsedTMax)
                result = "NG HIGH";
            else if (elapsedTime < elapsedTMin)
                result = "NG LOW";
            else
                result = "OK";

            string sql = @"
            INSERT INTO [dbo].[RESULTS]
            ([STATION],[SCREEN],[PRODUCT_ID],[ELAPSED_TIME],[ET_MAX],[ET_MIN],[RESULT]) 
            VALUES
            (@STATION,@SCREEN,@PRODUCT_ID,@ELAPSED_TIME,@ET_MAX,@ET_MIN,@RESULT)";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@STATION", "9");
                        cmd.Parameters.AddWithValue("@SCREEN", "9-3");
                        cmd.Parameters.AddWithValue("@PRODUCT_ID", productID);
                        cmd.Parameters.AddWithValue("@ELAPSED_TIME", elapsedTime);
                        cmd.Parameters.AddWithValue("@ET_MAX", elapsedTMax.ToString()); //CHECK THE OUTPUT ON THE DB
                        cmd.Parameters.AddWithValue("@ET_MIN", elapsedTMin);            //CHECK ALSO
                        cmd.Parameters.AddWithValue("@RESULT", result);

                        string rowsAffected = cmd.ExecuteNonQuery().ToString();

                        Tb_Console.Text += "Rows: " + rowsAffected + "\r\n";

                        logger.Log("inserted (" + rowsAffected + ") rows"
                            + "\r\nElapsed time: " + elapsedTime
                            + "\r\nProd ID 1: " + productID
                            + "\r\nResult: " + result);
                    }
                }
            }
            catch (SqlException ex)
            {
                Tb_Console.Text = "ERROR INSERTING SQL";

                logger.Log("Error on inserting: "
                    + "\r\nElapsed time: " + elapsedTime
                    + "\r\nProd ID 1: " + productID
                    + "\r\nResult: " + result
                    + "\r\n##### Exception message: \r\n" + ex.Message);
            }
        }

        public void ReadValues()
        {
            //To Do:  TREAT THE EXCEPTIONS SUCH AS: PRODUCT RELEASE...
            //Elapsed time is get by application stopwatch, exact time of beginning and ending of screen is disregarded
            string[] fileNames = Directory.GetFiles(pathPrintOut);

            printOutDateTime = Directory.GetCreationTime(fileNames[fileNames.Length - 1]);

            if (printOutDateTime > appStartTime)
            {
                appStartTime = printOutDateTime; // I do it for update the last PID 

                printOutResult = ExtractTextFromXps(pathPrintOut);

                logger.Log("New PID: " + printOutResult);


                if (mem || newPID_mem)
                {
                    newIncompletePID_mem = true; //Used just for TCP Client restart process on client side (the client side restart this flag)

                    mem = false;
                    newPID_mem = false;
                    Tb_Console.Text += "newPID without end process (maybe PID release at SQS)";
                    logger.Log("newPID without end process (maybe PID release at SQS)");
                }
                else { newPID_mem = true; }

            }

            if (ModServer.holdingRegisters[1] == 1 && !mem && newPID_mem)
            {
                mem = true;
                stopwatch.Restart();
                Tb_Console.Text += "Timer STARTED to PID: " + printOutResult + "\r\n";

                logger.Log("Timer started to PID: " + printOutResult);
            }
            else if (ModServer.holdingRegisters[1] == 2 && mem)
            {
                mem = false;
                newPID_mem = false;

                stopwatch.Stop();
                InsertValuesDB(stopwatch.Elapsed);

                Tb_Console.Text += "Timer ENDED to PID: " + printOutResult + "\r\n";
                Tb_Console.Text += "Elapsed time: " + stopwatch.Elapsed + "\r\n";

                logger.Log("Timer ended to PID: " + printOutResult);
            }
        }

        private void Btn_start_Click(object sender, EventArgs e)
        {
            if (Btn_start.Text == "START")
            {
                logger.Log("Button START hitted - TIMER STARTED");

                ModServer = new EasyModbus.ModbusServer();

                IPAddress address = IPAddress.Parse(ipAddress);
                Byte[] ipBytes = address.GetAddressBytes();
                IPAddress localhost = new IPAddress(ipBytes);

                ModServer.LocalIPAddress = localhost;
                ModServer.Listen();

                Timer1.Start();

                Btn_start.Text = "STOP";
                lb_ServerStatus.BackColor = Color.LimeGreen;
            }
            else if (Btn_start.Text == "STOP")
            {
                logger.Log("Button STOP hitted");

                try
                {
                    ModServer.StopListening();
                }
                catch(ThreadStateException ex)
                {
                    logger.Log("Exception on stopping medserver on stop button: " + ex);
                }
                ModServer = null;

                Timer1.Stop();

                Btn_start.Text = "START";
                lb_ServerStatus.BackColor = Color.Crimson;


                lb_ClientsQty.Text = "0";
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (Btn_start.Text == "STOP")
            {
                ReadValues();

                if (ModServer.NumberOfConnections >= 1)
                {
                    lb_ClientsQty.Text = ModServer.NumberOfConnections.ToString();
                }
                else
                {
                    lb_ClientsQty.Text = ModServer.NumberOfConnections.ToString();
                }

                if (lb_ServerStatus.BackColor == Color.LimeGreen)
                    lb_ServerStatus.BackColor = Color.Transparent;
                else
                    lb_ServerStatus.BackColor = Color.LimeGreen;
            }
        }

        private void Btn_startTCPServer_Click(object sender, EventArgs e)
        {
            if (Btn_startTCPServer.Text == "GO TCP")
            {
                TCPThread = new Thread(() => TCPWorkThread(cts.Token));
                TCPThread.Start();

                Btn_startTCPServer.Text = "TOFF TCP";

                memServer = true;
            }
            else if (Btn_startTCPServer.Text == "TOFF TCP")
            {
                try
                {

                    server.Stop();

                    cts.Cancel();

                    //  TCPThread.Abort();
                    TCPThread = null;
                }
                catch (SocketException se)
                {
                    logger.Log("SocketException on FormClosing: " + se);
                }
                catch (ObjectDisposedException oe)
                {
                    logger.Log("ObjectDisposedException on FormClosing: " + oe);
                }
                catch (AggregateException ae)
                {
                    logger.Log("AggregateException on FormClosing: " + ae);
                }
                catch (ThreadStateException te)
                {
                    logger.Log("ThreadStateException on FormClosing: " + te);
                }

                Btn_startTCPServer.Text = "GO TCP";

                Tb_TCPConsole.Text = "";
            }
        }

        private void TCPWorkThread(CancellationToken token)
        {
            bool tmem = false;
            bool tmem2 = false;

            Int32 port = 13000;
            IPAddress localAddr = IPAddress.Parse(Tb_connStringTCPServer.Text);

            server = new TcpListener(localAddr, port);

            while (!token.IsCancellationRequested)
            {
                try
                {
                    server.Start();
                    isListening = true; 

                    this.Invoke((MethodInvoker)delegate
                    {
                        Tb_TCPConsole.Text += ("Waiting for a connection... ");
                    });

                    TcpClient client = server.AcceptTcpClient();
                    this.Invoke((MethodInvoker)delegate
                    {
                        Tb_TCPConsole.Text += ("+ Connected!");
                    });

                    NetworkStream stream = client.GetStream();

                    while (client.Connected)
                    {
                        if (newIncompletePID_mem)
                        {
                            tmem = false;
                            tmem2 = false;
                            newIncompletePID_mem = false;
                        }


                        if (newPID_mem && !tmem)
                        {
                            Byte[] data = System.Text.Encoding.ASCII.GetBytes("cmd-NewPID:" + printOutResult);
                            stream.Write(data, 0, data.Length);

                            tmem = true;
                        }


                        if (mem && tmem && !tmem2)
                        {
                            int i;

                            Byte[] data = System.Text.Encoding.ASCII.GetBytes("cmd-TimerStart:" + stopwatch.Elapsed);
                            stream.Write(data, 0, data.Length);

                            tmem2 = true;

                        }


                        if (!mem && tmem2)
                        {
                            Byte[] data = System.Text.Encoding.ASCII.GetBytes("cmd-TimerStop:" + stopwatch.Elapsed);
                            stream.Write(data, 0, data.Length);

                            tmem = false;
                            tmem2 = false;
                        }
                    }

                    if (!client.Connected)
                        Tb_TCPConsole.Text += " - Client desconnected!";
                }
                catch (SocketException exc)
                {
                    string str = exc.ToString();
                    if (str.Contains("0x80004005"))
                    {
                        logger.Log("Exception on TCPWorkThread func controlled (0x80004005), the cancelation token went to true and stopped TCPThread.");
                    }
                    else
                    {
                        logger.Log("SocketException on TCPWorkThread func: \r\n" + exc);
                    }
                    server.Stop();
                    isListening = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s1 = "cmd-NewPID:" + "REM1";
            string s2 = "cmd-TimerStart:" + "REM22";
            string s3 = "cmd-TimerStop:" + "REM333";

            Tb_Console.Text += s1.Remove(0, s1.IndexOf(':')) + "\r\n";
            Tb_Console.Text += s2.Remove(0, s2.IndexOf(':')) + "\r\n";
            Tb_Console.Text += s3.Remove(0, s3.IndexOf(':')) + "\r\n";
        }
    }
}
