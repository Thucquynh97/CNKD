using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chiecnonkidieu
{
    public partial class FormChienthang : Form
    {
        public FormChienthang()
        {
            InitializeComponent();
        }

        private void bttieptuc_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bttieptuc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                this.Close();
        }
    }
}
