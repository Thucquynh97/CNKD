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
using System.Text.RegularExpressions;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing.Drawing2D;

namespace Chiecnonkidieu
{
    public partial class FormPlaygame : Form
    {
        private bool flag; //Biến cờ kiểm soát thao tác người dùng VD:Quay nón xong phải chọn chữ và ngược lại
        private int ketqua; //Biến lưu trữ kết quả   
        private int count = 0; //Tạo hiệu ừng Quay ( timer2)
        private Image img; //Lưu trữ hình chiếc nón kỳ diệu
        private int angle; //góc của chiếc nón 15 điêm tương đương với 1 giá trị
        private float width;
        private float height;
        private Random rand; //Biến lưu kết quả ngẫu nhiên
        public static int select { get; set; }//lựa chọn ô may mắn của người dùng
        private Connectsql cn = null;
        private Functionplaygame Func = null;
        public FormPlaygame()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cn = new Connectsql();
            Func = new Functionplaygame();
            rand = new Random();
            img = Image.FromFile(Application.StartupPath + @"\chiecnon.png");
            txtMang.Text = "";
            groupBox2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (flag == true)
            {

                Button b = (Button)sender;
                char charClicked = b.Text.ToCharArray()[0];
                if (Func.CheckCharClicked(charClicked))
                {

                    lbthongbao.Text = "Bạn đã trả lời đúng";

                    if (Func.SelectQuestion(charClicked, ketqua) == false)
                    {

                        txtMang.Text = Func.soMang.ToString();
                        txtdiem.Text = Func.diem.ToString();
                        b.Enabled = false;
                    }
                    else
                    {
                        FormChienthang frm1 = new FormChienthang();
                        frm1.ShowDialog();
                        FormYnghiacautraloi frm2 = new FormYnghiacautraloi(Func.numQuest);
                        frm2.ShowDialog();
                        txtMang.Text = Func.soMang.ToString();
                        txtdiem.Text = Func.diem.ToString();
                        lbchoi.Text = Func.lbchoi;
                        EnableTrue();
                    }
                }
                else
                {
                    if (Func.soMang >= 0)
                        lbthongbao.Text = "Bạn đã trả lời sai";

                    else
                        MessageBox.Show("Ban da thua\nDiem cua ban: " + Func.diem.ToString());
                }
                flag = false;
            }
            else
            {
                MessageBox.Show("Bạn chưa quay chiếc nón kỳ diệu", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void btchoi_Click(object sender, EventArgs e)
        {
            btchoi.Enabled = false;
            EnableTrue();
            flag = false;
            Func.gbdapan = gbdapan;
            Func.StartGame();
            lbchoi.Text = Func.lbchoi;
            pictureBox1.Enabled = true;
            groupBox2.Enabled = true;
            Func.diem = 0;
            Func.soMang = 2;
            txtdiem.Text = Func.diem.ToString();
            txtMang.Text = Func.soMang.ToString();

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

        //Việc quay nón
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TranslateTransform(pictureBox1.Width / 2, pictureBox1.Height / 2);
            g.RotateTransform(angle);
            g.TranslateTransform(-pictureBox1.Width / 2, -pictureBox1.Height / 2);
            g.DrawImage(img, 0, 0);
        }

        //Làm nón quay
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Random rand; //chọn ngẫu nhiên kết quả

            if (flag == false)
            {
                rand = new Random();
                ketqua = rand.Next(1, 25) * 15;
                timer1.Interval = 30;
                timer1.Start();
                timer2.Start();
                btthoat.Enabled = false;
                flag = true;

            }
            else
            {
                MessageBox.Show("Bạn chưa chọn chữ cái", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                flag = true;
            }

        }

        //kết thúc trò chơi
        private void Endgame()
        {
            Formluudiem frm = new Formluudiem(Func.diem);
            frm.ShowDialog();
            this.Hide();

        }


        //Sử dụng cho chiếc nón
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
            if (ketqua == angle && timer2.Enabled == false)
            {
                timer1.Stop();
                lbthongbao.Text = Func.Showpoint(ketqua);
                txtdiem.Text = Func.diem.ToString();
                txtMang.Text = Func.soMang.ToString();
                flag = true;
            }
        }

        //Sử dụng làm chiếc nón quay một thời gian rồi ngừng
        private void timer2_Tick(object sender, EventArgs e)
        {

            count++;
            if (count == 20)
            {
                timer2.Enabled = false;
                timer2.Stop();
                btthoat.Enabled = true;
                count = 0;
            }
        }
        private void btthoat_Click(object sender, EventArgs e)
        {
            Endgame();
        }
        private void FormPlaygame_FormClosing(object sender, FormClosingEventArgs e)
        {
            Formmain frm = new Formmain();
            frm.Show();
            this.Hide();
        }

        //Vẽ nền cho form
        private void FormPlaygame_Paint(object sender, PaintEventArgs e)
        {
            Bitmap btm = new Bitmap(Application.StartupPath + @"\Picture\background1.jpg");
            Graphics g = e.Graphics;
            g.DrawImage((Image)btm, ClientRectangle);
        }
    }
}