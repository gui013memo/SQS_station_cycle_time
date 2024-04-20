namespace Results_client
{
    partial class newPID
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
            this.lb_CurrentEngineNumber = new System.Windows.Forms.Label();
            this.lb_CurrentEngineNumberValue = new System.Windows.Forms.Label();
            this.lb_ElapsedTime = new System.Windows.Forms.Label();
            this.lb_StationBeingMonitored = new System.Windows.Forms.Label();
            this.lb_ElapedTimeValue = new System.Windows.Forms.Label();
            this.newPID_timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lb_CurrentEngineNumber
            // 
            this.lb_CurrentEngineNumber.AutoSize = true;
            this.lb_CurrentEngineNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 58F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_CurrentEngineNumber.Location = new System.Drawing.Point(60, 145);
            this.lb_CurrentEngineNumber.Name = "lb_CurrentEngineNumber";
            this.lb_CurrentEngineNumber.Size = new System.Drawing.Size(1645, 109);
            this.lb_CurrentEngineNumber.TabIndex = 0;
            this.lb_CurrentEngineNumber.Text = "CURRENT ENGINE NUMBER (PID):";
            // 
            // lb_CurrentEngineNumberValue
            // 
            this.lb_CurrentEngineNumberValue.AutoSize = true;
            this.lb_CurrentEngineNumberValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 120F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_CurrentEngineNumberValue.Location = new System.Drawing.Point(546, 283);
            this.lb_CurrentEngineNumberValue.Name = "lb_CurrentEngineNumberValue";
            this.lb_CurrentEngineNumberValue.Size = new System.Drawing.Size(761, 226);
            this.lb_CurrentEngineNumberValue.TabIndex = 1;
            this.lb_CurrentEngineNumberValue.Text = "112233";
            // 
            // lb_ElapsedTime
            // 
            this.lb_ElapsedTime.AutoSize = true;
            this.lb_ElapsedTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 58F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_ElapsedTime.Location = new System.Drawing.Point(610, 633);
            this.lb_ElapsedTime.Name = "lb_ElapsedTime";
            this.lb_ElapsedTime.Size = new System.Drawing.Size(661, 109);
            this.lb_ElapsedTime.TabIndex = 2;
            this.lb_ElapsedTime.Text = "Elapesed time";
            // 
            // lb_StationBeingMonitored
            // 
            this.lb_StationBeingMonitored.AutoSize = true;
            this.lb_StationBeingMonitored.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_StationBeingMonitored.Location = new System.Drawing.Point(12, 9);
            this.lb_StationBeingMonitored.Name = "lb_StationBeingMonitored";
            this.lb_StationBeingMonitored.Size = new System.Drawing.Size(136, 25);
            this.lb_StationBeingMonitored.TabIndex = 3;
            this.lb_StationBeingMonitored.Text = "OP50M1 - M9";
            // 
            // lb_ElapedTimeValue
            // 
            this.lb_ElapedTimeValue.AutoSize = true;
            this.lb_ElapedTimeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 120F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_ElapedTimeValue.Location = new System.Drawing.Point(300, 730);
            this.lb_ElapedTimeValue.Name = "lb_ElapedTimeValue";
            this.lb_ElapedTimeValue.Size = new System.Drawing.Size(1262, 226);
            this.lb_ElapedTimeValue.TabIndex = 4;
            this.lb_ElapedTimeValue.Text = "00:00:00.000";
            // 
            // newPID_timer
            // 
            this.newPID_timer.Interval = 250;
            this.newPID_timer.Tick += new System.EventHandler(this.newPID_timer_Tick);
            // 
            // newPID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.ClientSize = new System.Drawing.Size(1902, 1033);
            this.Controls.Add(this.lb_ElapedTimeValue);
            this.Controls.Add(this.lb_StationBeingMonitored);
            this.Controls.Add(this.lb_ElapsedTime);
            this.Controls.Add(this.lb_CurrentEngineNumberValue);
            this.Controls.Add(this.lb_CurrentEngineNumber);
            this.Name = "newPID";
            this.Text = "newPID";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_CurrentEngineNumber;
        private System.Windows.Forms.Label lb_ElapsedTime;
        private System.Windows.Forms.Label lb_StationBeingMonitored;
        private System.Windows.Forms.Label lb_ElapedTimeValue;
        public System.Windows.Forms.Label lb_CurrentEngineNumberValue;
        public System.Windows.Forms.Timer newPID_timer;
    }
}