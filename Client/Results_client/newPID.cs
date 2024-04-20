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
        }

        private void newPID_timer_Tick(object sender, EventArgs e)
        {
            lb_ElapedTimeValue.Text = stopwatch.ToString();

            if (this.BackColor == Color.White)
                this.BackColor = Color.DodgerBlue;
            else
                this.BackColor = Color.White;
        }
    }
}
