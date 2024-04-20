using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Results_client
{
    public partial class newPID : Form
    {

        public Stopwatch stopwatch = new Stopwatch();

        public newPID()
        {
            InitializeComponent();
            this.FormClosing += NewPID_FormClosing;
        }

        private void NewPID_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
        }

        public void AssyEnded(TimeSpan timeSpan)
        {
            lb_ElapedTimeValue.Text = timeSpan.ToString();
            this.BackColor = Color.Lime;
            lb_CurrentEngineNumber.BackColor = Color.Lime;
            lb_CurrentEngineNumberValue.BackColor = Color.Lime;
            lb_ElapedTimeValue.BackColor = Color.Lime;
            lb_ElapsedTime.BackColor = Color.Lime;
            lb_StationBeingMonitored.BackColor = Color.Lime;

        }

        private void newPID_timer_Tick(object sender, EventArgs e)
        {
            if (this.BackColor == Color.Lime)
                this.BackColor = Color.White;

            lb_ElapedTimeValue.Text = stopwatch.Elapsed.ToString();

            if (this.BackColor == Color.White)
            {
                this.BackColor = Color.DodgerBlue;
                lb_CurrentEngineNumber.BackColor = Color.DodgerBlue;
                lb_CurrentEngineNumberValue.BackColor = Color.DodgerBlue;
                lb_ElapedTimeValue.BackColor = Color.DodgerBlue;
                lb_ElapsedTime.BackColor = Color.DodgerBlue;
                lb_StationBeingMonitored.BackColor = Color.DodgerBlue;
            }
            else
            {
                this.BackColor = Color.White;
                lb_CurrentEngineNumber.BackColor = Color.White;
                lb_CurrentEngineNumberValue.BackColor = Color.White;
                lb_ElapedTimeValue.BackColor = Color.White;
                lb_ElapsedTime.BackColor = Color.White;
                lb_StationBeingMonitored.BackColor = Color.White;
            }

        }
    }
}
