using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        TcpClient client;
        NetworkStream stream;
        Byte[] bytes = new Byte[256];
        String data = null;

        CancellationTokenSource cts = new CancellationTokenSource();
        Thread ClientThread;

        bool mem = false;
        bool newPID = false;
        bool timerStart = false;
        bool timerStop = false;

        public Results()
        {
            InitializeComponent();
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

            while (true)
            {
                int i;
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0 && !mem)
                {
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                    this.Invoke((MethodInvoker)delegate
                    {
                        // Updating label text.
                        Tb_ClientConsole.Text += "cmd received: " + data + "\r\n";

                        switch (data)
                        {
                            case "cmd-NewPID":
                                newPID = true;
                                break;
                            case "cmd-TimerStart":
                                timerStart = true;
                                break;
                            case "cmd-TimerStop":
                                timerStop = true;
                                break;
                        }
                    });
                    mem = true;
                }

            }
        }

        private void tim1_Tick(object sender, EventArgs e)
        {
            if (newPID)
            {

            }
        }
    }
}
