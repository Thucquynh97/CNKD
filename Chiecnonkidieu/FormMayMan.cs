using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chiecnonkidieu
{
    public partial class Formmayman : Form
    {
        private int length = 0;
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
        private void Formmayman_Load(object sender, EventArgs e)
        {
            string strPattern = @"[\s]+";
            Regex rgx = new Regex(strPattern);
            string output = rgx.Replace(Connectsql.arrAnswer1[countNumOfQuestion].ToString(), "");//loại bỏ khoảng trắng (space)
            length = output.Length;
        }
        private void timerProgressBar_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(1);
            if (progressBar1.Value == 95)
            {
                timerProgressBar.Stop();
                progressBar1.Value = 0;
                this.Close();
          
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
            if (txtmayman.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập ô may mắn", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else if (txtmayman.Text != "" && int.Parse(txtmayman.Text) >= 1 && int.Parse(txtmayman.Text) <= length)
            {

                Functionplaygame.select = (int.Parse(txtmayman.Text)) - 1;
                this.Hide();
            }
            else
            {
                MessageBox.Show("Bạn nhập gia trị sai\nNhập giá trị >= 1 và <= " + length, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void Formmayman_FormClosed(object sender, FormClosedEventArgs e)
        {
            Functionplaygame.select = -1;
        }
    }
}
