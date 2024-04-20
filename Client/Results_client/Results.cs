using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Results_client
{
    public partial class Results : Form
    {
        newPID newPID_form = new newPID();

        TcpClient client;
        NetworkStream stream;
        Byte[] bytes = new Byte[256];
        String data = null;

        CancellationTokenSource cts = new CancellationTokenSource();
        Thread ClientThread;

        bool mem = false;
        bool newPIDmem = false;
        bool timerStart = false;
        bool timerStop = false;

        string newPID = null;

        Stopwatch stopwatch = new Stopwatch();
        TimeSpan elapsedTimeFromServer;

        public Results()
        {
            InitializeComponent();
            this.FormClosing += Results_FormClosing;
        }

        private void Results_FormClosing(object sender, FormClosingEventArgs e)
        {
            cts.Cancel();
            client.Close();
        }

        private void Btn_connect_Click(object sender, EventArgs e)
        {
            if (Btn_connect.Text == "Start Listening")
            {
                ClientThread = new Thread(() => ClientWorkThread(cts.Token));
                ClientThread.Start();

                Btn_connect.Text = "Stop Listening";



                tim1.Start();
            }
            else if (Btn_connect.Text == "Stop Listening")
            {
                cts.Cancel();
                ClientThread = null;

                tim1.Stop();

                Btn_connect.Text = "Start Listening";
            }
        }

        private void ClientWorkThread(CancellationToken token)
        {
            client = new TcpClient("172.16.90.231", 13000);
            stream = client.GetStream();

            bool m1 = false;
            bool m2 = false;

            while (client.Connected)
            {
                int i;
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                    this.Invoke((MethodInvoker)delegate
                    {
                        Tb_ClientConsole.Text += "cmd received: " + data + "\r\n";

                        if (data.Contains("cmd-NewPID") && !m1)
                        {
                            m1 = true;

                            newPID = data.Remove(0, data.IndexOf(':'));

                            if (newPID_form.Visible == false)
                            {
                                newPID_form.Show();
                                newPID_form.TopLevel = true;
                            }

                            this.Invoke((MethodInvoker)delegate
                            {
                                newPID_form.lb_CurrentEngineNumberValue.Text = newPID;
                                newPID_form.Refresh();
                            });
                        }
                        else if (data.Contains("cmd-TimerStart") && !m2)
                        {
                            m2 = true;

                            newPID_form.stopwatch.Restart();
                            newPID_form.newPID_timer.Start();
                        }
                        else if (data.Contains("cmd-TimerStop"))
                        {
                            m1 = false;
                            m2 = false;

                            newPID_form.stopwatch.Stop();
                            newPID_form.newPID_timer.Stop();

                            TimeSpan.TryParse(data.Remove(0, data.IndexOf(':')), out elapsedTimeFromServer);

                            newPID_form.AssyEnded(elapsedTimeFromServer);
                        }
                    });
                }
            }



        }

        private void tim1_Tick(object sender, EventArgs e)
        {
        }
    }
}
