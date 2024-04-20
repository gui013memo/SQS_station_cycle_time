namespace SQS_station_cycle_time
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Btn_start = new System.Windows.Forms.Button();
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.lb_ServerStatus = new System.Windows.Forms.Label();
            this.lb_Server = new System.Windows.Forms.Label();
            this.lb_Clients = new System.Windows.Forms.Label();
            this.lb_ClientsQty = new System.Windows.Forms.Label();
            this.Tb_Console = new System.Windows.Forms.TextBox();
            this.Btn_startTCPServer = new System.Windows.Forms.Button();
            this.Tb_connStringTCPServer = new System.Windows.Forms.TextBox();
            this.Tb_TCPConsole = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_start
            // 
            this.Btn_start.Location = new System.Drawing.Point(16, 350);
            this.Btn_start.Margin = new System.Windows.Forms.Padding(4);
            this.Btn_start.Name = "Btn_start";
            this.Btn_start.Size = new System.Drawing.Size(284, 69);
            this.Btn_start.TabIndex = 0;
            this.Btn_start.Text = "START";
            this.Btn_start.UseVisualStyleBackColor = true;
            this.Btn_start.Click += new System.EventHandler(this.Btn_start_Click);
            // 
            // Timer1
            // 
            this.Timer1.Interval = 300;
            this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // lb_ServerStatus
            // 
            this.lb_ServerStatus.AutoSize = true;
            this.lb_ServerStatus.BackColor = System.Drawing.Color.Red;
            this.lb_ServerStatus.Location = new System.Drawing.Point(373, 11);
            this.lb_ServerStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_ServerStatus.Name = "lb_ServerStatus";
            this.lb_ServerStatus.Size = new System.Drawing.Size(13, 16);
            this.lb_ServerStatus.TabIndex = 1;
            this.lb_ServerStatus.Text = "  ";
            // 
            // lb_Server
            // 
            this.lb_Server.AutoSize = true;
            this.lb_Server.Location = new System.Drawing.Point(324, 11);
            this.lb_Server.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_Server.Name = "lb_Server";
            this.lb_Server.Size = new System.Drawing.Size(47, 16);
            this.lb_Server.TabIndex = 2;
            this.lb_Server.Text = "Server";
            // 
            // lb_Clients
            // 
            this.lb_Clients.AutoSize = true;
            this.lb_Clients.Location = new System.Drawing.Point(324, 27);
            this.lb_Clients.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_Clients.Name = "lb_Clients";
            this.lb_Clients.Size = new System.Drawing.Size(47, 16);
            this.lb_Clients.TabIndex = 3;
            this.lb_Clients.Text = "Clients";
            // 
            // lb_ClientsQty
            // 
            this.lb_ClientsQty.AutoSize = true;
            this.lb_ClientsQty.BackColor = System.Drawing.Color.Transparent;
            this.lb_ClientsQty.Location = new System.Drawing.Point(375, 28);
            this.lb_ClientsQty.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_ClientsQty.Name = "lb_ClientsQty";
            this.lb_ClientsQty.Size = new System.Drawing.Size(14, 16);
            this.lb_ClientsQty.TabIndex = 4;
            this.lb_ClientsQty.Text = "0";
            // 
            // Tb_Console
            // 
            this.Tb_Console.Location = new System.Drawing.Point(16, 7);
            this.Tb_Console.Margin = new System.Windows.Forms.Padding(4);
            this.Tb_Console.Multiline = true;
            this.Tb_Console.Name = "Tb_Console";
            this.Tb_Console.Size = new System.Drawing.Size(283, 334);
            this.Tb_Console.TabIndex = 5;
            // 
            // Btn_startTCPServer
            // 
            this.Btn_startTCPServer.Location = new System.Drawing.Point(336, 369);
            this.Btn_startTCPServer.Margin = new System.Windows.Forms.Padding(4);
            this.Btn_startTCPServer.Name = "Btn_startTCPServer";
            this.Btn_startTCPServer.Size = new System.Drawing.Size(53, 50);
            this.Btn_startTCPServer.TabIndex = 6;
            this.Btn_startTCPServer.Text = "GO TCP";
            this.Btn_startTCPServer.UseVisualStyleBackColor = true;
            this.Btn_startTCPServer.Click += new System.EventHandler(this.Btn_startTCPServer_Click);
            // 
            // Tb_connStringTCPServer
            // 
            this.Tb_connStringTCPServer.Location = new System.Drawing.Point(319, 339);
            this.Tb_connStringTCPServer.Margin = new System.Windows.Forms.Padding(4);
            this.Tb_connStringTCPServer.Name = "Tb_connStringTCPServer";
            this.Tb_connStringTCPServer.Size = new System.Drawing.Size(102, 22);
            this.Tb_connStringTCPServer.TabIndex = 7;
            this.Tb_connStringTCPServer.Text = "172.16.90.231";
            // 
            // Tb_TCPConsole
            // 
            this.Tb_TCPConsole.Location = new System.Drawing.Point(319, 245);
            this.Tb_TCPConsole.Margin = new System.Windows.Forms.Padding(4);
            this.Tb_TCPConsole.Multiline = true;
            this.Tb_TCPConsole.Name = "Tb_TCPConsole";
            this.Tb_TCPConsole.Size = new System.Drawing.Size(102, 86);
            this.Tb_TCPConsole.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(327, 160);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(434, 432);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Tb_TCPConsole);
            this.Controls.Add(this.Tb_connStringTCPServer);
            this.Controls.Add(this.Btn_startTCPServer);
            this.Controls.Add(this.Tb_Console);
            this.Controls.Add(this.lb_ClientsQty);
            this.Controls.Add(this.lb_Clients);
            this.Controls.Add(this.lb_Server);
            this.Controls.Add(this.lb_ServerStatus);
            this.Controls.Add(this.Btn_start);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Main";
            this.Text = "SQS_Station_cycle_time";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_start;
        private System.Windows.Forms.Timer Timer1;
        private System.Windows.Forms.Label lb_ServerStatus;
        private System.Windows.Forms.Label lb_Server;
        private System.Windows.Forms.Label lb_Clients;
        private System.Windows.Forms.Label lb_ClientsQty;
        private System.Windows.Forms.TextBox Tb_Console;
        private System.Windows.Forms.Button Btn_startTCPServer;
        private System.Windows.Forms.TextBox Tb_connStringTCPServer;
        private System.Windows.Forms.TextBox Tb_TCPConsole;
        private System.Windows.Forms.Button button1;
    }
}

