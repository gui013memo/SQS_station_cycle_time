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
            }
            else if (Btn_connect.Text == "Stop Listening")
            {
                cts.Cancel();
                ClientThread = null;

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
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                    this.Invoke((MethodInvoker)delegate {
                        // Updating label text.
                        Tb_ClientConsole.Text = data;
                    });
                }
            }
        }
    }
}
