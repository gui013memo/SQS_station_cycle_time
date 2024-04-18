using EasyModbus;
using EngineNumber_checker;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using GroupDocs.Viewer;
using GroupDocs.Viewer.Options;
using System.Text;
using System.Windows.Xps.Packaging;
using System.Xml;
using System.IO;
using System.Net.Sockets;
using GroupDocs.Viewer.Results;
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

        bool mem = false;
        bool mem2 = false;
        bool memServer = false;

        CancellationTokenSource cts = new CancellationTokenSource();
        Thread TCPThread;

        TcpListener server = null;

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
            if (memServer)
            {
                server.Stop();
                cts.Cancel();
            }
          
            logger.Log("SQS_SCT app closed");
            TCPThread = null;
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

        public void GetParameters()
        {
            string query = @"SELECT [STATION]
                                  ,[SCREEN]
                                  ,[ET_MAX]
                                  ,[ET_MIN]
                              FROM [SQS_SCT].[dbo].[PARAMETERS]";
        }

        public void InsertValuesDB(TimeSpan elapsedTime)
        {
            string productID = ExtractTextFromXps(pathPrintOut);

            string result = null;

            if (elapsedTime > elapsedTMax)
                result = "NG HIGH - Time above max";
            else if (elapsedTime < elapsedTMin)
                result = "NG LOW - Time below min";
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

            if (Directory.GetCreationTime(fileNames[fileNames.Length - 1]) > appStartTime && !mem2)
            {
                logger.Log("New PID: " + ExtractTextFromXps(pathPrintOut));

                mem2 = true;
            }

            if (ModServer.holdingRegisters[1] == 1 && !mem)
            {
                mem = true;
                stopwatch.Restart();
                Tb_Console.Text += "Timer started \r\n";

                logger.Log("Timer started");
            }
            else if (ModServer.holdingRegisters[1] == 2 && mem)
            {
                mem = false;
                mem2 = false;
                stopwatch.Stop();
                InsertValuesDB(stopwatch.Elapsed);

                Tb_Console.Text += "Timer ended \r\n";
                Tb_Console.Text += "Elapsed time: " + stopwatch.Elapsed + "\r\n";

                logger.Log("Timer ended with elapsed time: " + stopwatch.Elapsed);
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
                ModServer.StopListening();
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
                server.Stop();

                cts.Cancel();
                TCPThread = null;

                Btn_startTCPServer.Text = "GO TCP";

                Tb_TCPConsole.Text = "";
            }
        }

        private void TCPWorkThread(CancellationToken token)
        {
            bool tmem = false;

            Int32 port = 13000;
            IPAddress localAddr = IPAddress.Parse(Tb_connStringTCPServer.Text);

            server = new TcpListener(localAddr, port);

            try
            {
                server.Start();
                this.Invoke((MethodInvoker)delegate
                {
                    Tb_TCPConsole.Text += ("Waiting for a connection... ");
                });


                TcpClient client = server.AcceptTcpClient();
                server.Start();
                this.Invoke((MethodInvoker)delegate
                {
                    Tb_TCPConsole.Text += ("Connected!");
                });


                NetworkStream stream = client.GetStream();

                while (client.Connected)
                {
                    if (mem2 && !tmem)
                    {
                        Byte[] data = System.Text.Encoding.ASCII.GetBytes("cmd-NewPID");

                        stream.Write(data, 0, data.Length);

                        tmem = true;
                    }

                    if (mem && tmem)
                    {
                        Byte[] data = System.Text.Encoding.ASCII.GetBytes("cmd-TimerStart");

                        stream.Write(data, 0, data.Length);
                    }
                    else if (!mem && tmem)
                    {
                        Byte[] data = System.Text.Encoding.ASCII.GetBytes("cmd-TimerStop");

                        stream.Write(data, 0, data.Length);

                        tmem = false;
                    }

                }
            }
            catch (SocketException exc)
            {
                logger.Log("SocketException: \r\n" + exc);
            }
        }
    }
}
