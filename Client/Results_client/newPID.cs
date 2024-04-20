using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Results_client
{
    public partial class newPID : Form
    {
        public newPID()
        {
            InitializeComponent();
            this.FormClosing += NewPID_FormClosing;
        }

        private void NewPID_FormClosing(object sender, FormClosingEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
