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
            this.button1 = new System.Windows.Forms.Button();
            this.tb_test = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Btn_start
            // 
            this.Btn_start.Location = new System.Drawing.Point(16, 350);
            this.Btn_start.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.Tb_Console.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Tb_Console.Multiline = true;
            this.Tb_Console.Name = "Tb_Console";
            this.Tb_Console.Size = new System.Drawing.Size(283, 334);
            this.Tb_Console.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(352, 369);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 50);
            this.button1.TabIndex = 6;
            this.button1.Text = "START";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tb_test
            // 
            this.tb_test.Location = new System.Drawing.Point(319, 319);
            this.tb_test.Margin = new System.Windows.Forms.Padding(4);
            this.tb_test.Name = "tb_test";
            this.tb_test.Size = new System.Drawing.Size(83, 22);
            this.tb_test.TabIndex = 7;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(415, 432);
            this.Controls.Add(this.tb_test);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Tb_Console);
            this.Controls.Add(this.lb_ClientsQty);
            this.Controls.Add(this.lb_Clients);
            this.Controls.Add(this.lb_Server);
            this.Controls.Add(this.lb_ServerStatus);
            this.Controls.Add(this.Btn_start);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tb_test;
    }
}

