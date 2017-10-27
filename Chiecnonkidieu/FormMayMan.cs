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
    public partial class Formmayman : Form
    {
        private int count = 0;
        private int countNumOfQuestion; //đếm kí tự của câu trả lời
        public Formmayman()
        {
            InitializeComponent();
        }
        public Formmayman(int a)
        {
            this.countNumOfQuestion = a;
            InitializeComponent();
        }

        private void timerProgressBar_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(1);
            if (progressBar1.Value == 95)
            {
                timerProgressBar.Stop();
                progressBar1.Value = 0;
                if (txtmayman.Text == "")
                {
                    FormPlaygame.select = -1;
                    this.Close();
                }
                else
                {
                    FormPlaygame.select = int.Parse(txtmayman.Text) - 1;
                    this.Close();
                }

            }
        }

        private void txtmayman_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar)) //nếu là kí tự chữ
            {
                e.Handled = true;

            }
        }

        private void btsubmit_Click(object sender, EventArgs e)
        {
            if(txtmayman.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập ô may mắn", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        
            else if (txtmayman.Text != "" && int.Parse(txtmayman.Text) <= count)
            {
  
                FormPlaygame.select = int.Parse(txtmayman.Text) - 1;
                this.Close();
            }
            else
            {
                MessageBox.Show("Bạn nhập gia trị sai\nNhập giá trị < " + count, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void Formmayman_Load(object sender, EventArgs e)
        {
            count = Import.arrAnswer1[countNumOfQuestion].ToString().Length;
        }
    }
}
