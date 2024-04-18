namespace Results_client
{
    partial class Results
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
            this.Btn_connect = new System.Windows.Forms.Button();
            this.Tb_ClientConsole = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Btn_connect
            // 
            this.Btn_connect.Location = new System.Drawing.Point(92, 283);
            this.Btn_connect.Name = "Btn_connect";
            this.Btn_connect.Size = new System.Drawing.Size(132, 63);
            this.Btn_connect.TabIndex = 0;
            this.Btn_connect.Text = "Start Listening";
            this.Btn_connect.UseVisualStyleBackColor = true;
            this.Btn_connect.Click += new System.EventHandler(this.Btn_connect_Click);
            // 
            // Tb_ClientConsole
            // 
            this.Tb_ClientConsole.Location = new System.Drawing.Point(12, 12);
            this.Tb_ClientConsole.Multiline = true;
            this.Tb_ClientConsole.Name = "Tb_ClientConsole";
            this.Tb_ClientConsole.Size = new System.Drawing.Size(346, 253);
            this.Tb_ClientConsole.TabIndex = 1;
            // 
            // Results
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 358);
            this.Controls.Add(this.Tb_ClientConsole);
            this.Controls.Add(this.Btn_connect);
            this.Name = "Results";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_connect;
        private System.Windows.Forms.TextBox Tb_ClientConsole;
    }
}

