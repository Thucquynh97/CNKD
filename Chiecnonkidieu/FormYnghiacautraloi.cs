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
    public partial class FormYnghiacautraloi : Form
    {
        private int numAnswer; //đọc câu trả lời
        private Connectsql cn = null;
        public FormYnghiacautraloi()
        {
            InitializeComponent();
        }
        public FormYnghiacautraloi(int answer)
        {
            this.numAnswer = answer;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormYnghiacautraloi_Load(object sender, EventArgs e)
        {
            txttext.Text = Connectsql.arrAnswer2[numAnswer].ToString();
        }
    }
}
