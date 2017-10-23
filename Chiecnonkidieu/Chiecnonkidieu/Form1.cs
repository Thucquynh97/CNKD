using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Chiecnonkidieu
{
    public partial class Form1 : Form
    {
        Random rand; //Chọn ngẫu nhiên kết quả
        int ketqua; //kết quả sau khi ngừng quay nón
        int angle; //góc quay
        int count = 0; //đếm số lần lặp timer2
        Image img;
        String[] arrQuestion = new String[100];
        String[] arrAnswer = new String[100];
        List<Label> labels = new List<Label>();
        int numQuest = 0; //câu hỏi : 0 = câu 1
        int answerLength = 0; //kiểm tra người dùng trả lời xong câu hỏi chưa
        double diem = 0;
        int soMang = 3; //mạng của người chơi
        int space = 0;  //biến đếm số khoảng trắng trong cau trả lời

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            img = Image.FromFile(Application.StartupPath + @"\chiecnon.png");
            DialogResult dlg = MessageBox.Show("Chào mừng bạn đến với trò chơi Chiếc nón kí diệu \n -Nhấn nút Chơi để bắt đầu \n -Nhấn nút Thoát để thoát chương trình", "Chào mừng", MessageBoxButtons.OKCancel);
            if (dlg == DialogResult.OK)
            {
                MessageBox.Show("Bạn được tặng 500 điểm");
                diem = 500;
                txtdiem.Text = diem.ToString();
            }
            if (dlg != DialogResult.OK)
            {
                MessageBox.Show("GoodBye!");
                this.Close();
            }
            txtdiem.Text = diem.ToString() ;
            txtMang.Text = soMang.ToString();
            groupBox2.Enabled = false;

        }

        
        //đọc câu hỏi + câu trả lời từ file
        private void ImportQA(string str)
        {
            int icount = 0;
            int jcount = 0;
            bool flag = true;
            string[] lines = File.ReadAllLines(str);
            foreach (string s in lines)
            {
                if (flag == true)
                {
                    arrQuestion[icount++] = s;
                    flag = false;
                }
                else
                {
                    arrAnswer[jcount++] = s;
                    flag = true;
                }
             }
        }

        //Thêm kí tự chữ trong đáp án vào groupbox
        private void Addlabels()
        {
            gbdapan.Controls.Clear();
            labels.Clear();
            char[] wordChars = arrAnswer[numQuest].ToCharArray(); //chuyển đáp án thành từng kí tự
            
            //đếm các khoảng trắng giữa các từ
            foreach(char c in wordChars)
            {
                if (c == ' ')
                    space++;
            }

            int len = wordChars.Length;
            int refer = gbdapan.Width / len/2; // chia khoảng cách từng kí tự trong gourpbox
            for (int i = 0; i < len; i++)
            {
                Label l = new Label();
                if (wordChars[i] != ' ')
                    l.Text = "_";
                else
                l.Text = " "; 
                l.Location = new Point(10*15 + i * refer, gbdapan.Height - 60);
                l.Parent = gbdapan;
                l.BringToFront(); //mang labels ra trước groupbox, bảo đảm các labels được nhìn thấy
                labels.Add(l);
            }
        }

        //Chọn câu trả lời
        private void button1_Click(object sender, EventArgs e)
        {

            if (pictureBox1.Enabled == false)
            {
                Button b = (Button)sender;
                //char charClicked = b.Text.ToCharArray()[0];
                SelectQuestion(numQuest, b.Text.ToCharArray()[0]);

                b.Enabled = false;
                if (answerLength == arrAnswer[numQuest].Length - space)
                {
                    numQuest++;
                    space = 0;
                    answerLength = 0; //reset lại biến space và answerLength
                    Addlabels();
                    lbchoi.Text = "Câu " + (numQuest + 1) + " :" + arrQuestion[numQuest].ToString();
                    EnableTrue();
                }
            }
            pictureBox1.Enabled = true;
        }

        private void btchoi_Click(object sender, EventArgs e)
        {
            groupBox2.Enabled = true;
            ImportQA(Application.StartupPath + @"\cautraloi.txt");
            lbchoi.Text = "Câu " + (numQuest + 1) + " :" + arrQuestion[numQuest].ToString();
            Addlabels();
        }

        //Chọn câu hỏi
        private void SelectQuestion(int question, char charClicked)
        {
            arrAnswer[question] = arrAnswer[question].ToUpper();

            //Người chơi chọn đúng kí tự trong câu trả lời
            if (arrAnswer[question].Contains(charClicked))
            {
                lbstatus.Parent = gbdapan;
                char[] wordchar = arrAnswer[question].ToCharArray(); // chuyển chuỗi kết quả thành mảng kí tự
                for (int i = 0; i < wordchar.Length; i++)
                {
                    if (charClicked == wordchar[i])
                    {
                        answerLength++;
                        lbstatus.Text = "Bạn đã trả lời đúng!";
                        labels[i].Text = charClicked.ToString();
                        diem += 500;
                        txtdiem.Text = diem.ToString();
                    }
                }
            }
            else
            {
                lbstatus.Text = "Bạn đã trả lời sai!";
                if (soMang > 1)
                    soMang--;
                else
                {
                    soMang--;
                    txtMang.Text = soMang.ToString();
                    MessageBox.Show("Ban da thua\nDiem cua ban: " + diem.ToString());
                    groupBox2.Enabled = false;
                }
                //txtdiem.Text = diem.ToString();
                txtMang.Text = soMang.ToString();
            }
        }
        private void EnableTrue()
        {
            bta.Enabled = true;
            btb.Enabled = true;
            btc.Enabled = true;
            btd.Enabled = true;
            bte.Enabled = true;
            btf.Enabled = true;
            btg.Enabled = true;
            bti.Enabled = true;
            btj.Enabled = true;
            btk.Enabled = true;
            btl.Enabled = true;
            btm.Enabled = true;
            btn.Enabled = true;
            bto.Enabled = true;
            btp.Enabled = true;
            btq.Enabled = true;
            bts.Enabled = true;
            btt.Enabled = true;
            btu.Enabled = true;
            btv.Enabled = true;
            btx.Enabled = true;
            bty.Enabled = true;
            bth.Enabled = true;
            btr.Enabled = true;
            btw.Enabled = true;
            btz.Enabled = true;
        }
        private void EnableFalse(char text)//khóa 1 nút button chỉ định
        {
            switch (text)
            {
                case 'A': bta.Enabled = false; break;
                case 'B': btb.Enabled = false; break;
                case 'C': btc.Enabled = false; break;
                case 'D': btd.Enabled = false; break;
                case 'E': bte.Enabled = false; break;
                case 'F': btf.Enabled = false; break;
                case 'G': btg.Enabled = false; break;
                case 'H': bth.Enabled = false; break;
                case 'I': bti.Enabled = false; break;
                case 'J': btj.Enabled = false; break;
                case 'K': btk.Enabled = false; break;
                case 'L': btl.Enabled = false; break;
                case 'M': btm.Enabled = false; break;
                case 'N': btn.Enabled = false; break;
                case 'O': bto.Enabled = false; break;
                case 'P': btp.Enabled = false; break;
                case 'Q': btp.Enabled = false; break;
                case 'R': btr.Enabled = false; break;
                case 'S': bts.Enabled = false; break;
                case 'U': btu.Enabled = false; break;
                case 'V': btv.Enabled = false; break;
                case 'X': btx.Enabled = false; break;
                case 'Y': bty.Enabled = false; break;
                case 'W': btw.Enabled = false; break;
                case 'Z': btz.Enabled = false; break;
            }
        }
        public void tinhdiem(int x)
        {
            switch (x)
            {
                case 0:
                    {
                        MessageBox.Show("Bạn đã quay vào ô Nhân đôi điểm");
                        diem = diem * 2;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 15:
                    {
                        MessageBox.Show("Bạn đã quay vào ô 200 điểm");
                        diem += 200;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 30:
                    {
                        MessageBox.Show("Bạn đã quay vào ô 700 điểm");
                        diem += 700;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 45:
                    {
                        MessageBox.Show("Bạn đã quay vào ô 1000 điểm");
                        diem += 1000;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 60:
                    {
                        MessageBox.Show("Bạn đã quay vào ô 400 điểm");
                        diem += 400;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 75:
                    {
                        MessageBox.Show("Bạn đã quay vào ô Mất lượt");
                        soMang--;
                        txtMang.Text = soMang.ToString();
                        break;
                    }
                case 90:
                    {
                        MessageBox.Show("Bạn đã quay vào ô 900 điểm");
                        diem += 900;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 105:
                    {
                        MessageBox.Show("Bạn đã quay vào ô May mắn\n" +
                         "Bạn được chọn 1 ký tự");
                        break;
                    }
                case 120:
                    {
                        MessageBox.Show("Bạn đã quay vào ô 300 điểm");
                        diem += 300;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 135:
                    {
                        MessageBox.Show("Bạn đã quay vào ô 800 điểm");
                        diem += 800;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 150:
                    {
                        MessageBox.Show("Bạn đã quay vào ô Mất điểm");
                        diem = 0;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 165:
                    {
                        MessageBox.Show("Bạn đã quay vào ô 1000 điểm");
                        diem += 1000;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 180:
                    {
                        MessageBox.Show("Bạn đã quay vào ô 400 điểm");
                        diem += 400;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 195:
                    {
                        MessageBox.Show("Bạn đã quay vào ô 600 điểm");
                        diem += 600;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 200:
                    {
                        MessageBox.Show("Bạn đã quay vào ô 300 điểm");
                        diem += 300;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 225:
                    {
                        MessageBox.Show("Bạn đã quay vào ô Thêm lượt");
                        soMang++;
                        txtMang.Text = soMang.ToString();
                        break;
                    }
                case 240:
                    {
                        MessageBox.Show("Bạn đã quay vào ô 200 điểm");
                        diem += 200;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 255:
                    {
                        MessageBox.Show("Bạn đã quay vào ô 900 điểm");
                        diem += 900;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 270:
                    {
                        MessageBox.Show("Bạn đã quay vào ô 700 điểm");
                        diem += 700;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 285:
                    {
                        MessageBox.Show("Bạn đã quay vào ô Chia đôi điểm");
                        diem = diem / 2;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 300:
                    {
                        MessageBox.Show("Bạn đã quay vào ô 300 điểm");
                        diem += 300;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 315:
                    {
                        MessageBox.Show("Bạn đã quay vào ô 2000 điểm");
                        diem += 2000;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 330:
                    {
                        MessageBox.Show("Bạn đã quay vào ô 100 điểm");
                        diem += 100;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 345:
                    {
                        MessageBox.Show("Bạn đã quay vào ô 500 điểm");
                        diem += 500;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 360:
                    {
                        MessageBox.Show("Bạn đã quay vào ô Nhân đôi điểm");
                        diem = diem * 2;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            rand = new Random();
            ketqua = rand.Next(1, 25) * 15;
            timer1.Interval = 30;
            timer1.Start();
            timer2.Start();
            pictureBox1.Enabled = false;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TranslateTransform(pictureBox1.Width / 2, pictureBox1.Height / 2);
            g.RotateTransform(angle);
            g.TranslateTransform(-pictureBox1.Width / 2, -pictureBox1.Height / 2);
            g.DrawImage(img, 0, 0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (angle == 360)
            {
                angle = 0;
            }
            else
                angle += 15;

            this.Text = angle.ToString(); ;
            pictureBox1.Invalidate(); // vẽ lại trên picturebox
            Invalidate(); // vẽ lại trên form
            if (ketqua == angle && timer2.Enabled == false)
            {
                timer1.Stop();
                tinhdiem(ketqua);

            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            count++;
            if (count == 20)
            {
                timer2.Enabled = false;
                timer2.Stop();
                count = 0;
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            DialogResult dlg = MessageBox.Show("Bạn có muốn thoát không", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dlg == DialogResult.Yes)
                this.Close();
        }
    }
}