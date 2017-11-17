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
        private Random rand; //Biến lưu kết quả ngẫu nhiên
        private Connectsql cn = null;
        private Functionplaygame Func = null;
        private char charClicked;
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
            Button b = (Button)sender;
            charClicked = b.Text.ToCharArray()[0];
            if (flag == true)
            {

                //Kiểm tra câu trả lời của người dùng
                if (Func.CheckCharClicked(charClicked))
                {

                    lbthongbao.Text = "Bạn đã trả lời đúng";

                    if (Func.SelectQuestion(charClicked, ketqua) == false)  //Kiểm tra nếu chưa trả lời hết câu hỏi
                    {

                        txtMang.Text = Func.soMang.ToString();
                        txtdiem.Text = Func.diem.ToString();
                        b.Enabled = false;
                    }
                    else
                    {
                        
                        ChuyenCauHoi();
                        Func.NextQuestion();
                        lbchoi.Text = Func.lbchoi;
                        txtMang.Text = Func.soMang.ToString();
                        txtdiem.Text = Func.diem.ToString();

                    }

                }
                else
                {
                    if (Func.soMang > 0)
                    {
                        lbthongbao.Text = "Bạn đã trả lời sai";
                        txtMang.Text = Func.soMang.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Ban da thua\nDiem cua ban: " + Func.diem.ToString());
                        Func.Endgame();
                        this.Hide();
                    }
                    EnableFalse(b.Text);

                }
                flag = false;
            }
            else
            {
                MessageBox.Show("Bạn chưa quay chiếc nón kỳ diệu", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void ChuyenCauHoi()
        {
            FormChienthang frm1 = new FormChienthang();
            frm1.ShowDialog();
            FormYnghiacautraloi frm2 = new FormYnghiacautraloi(Func.numQuest);
            frm2.ShowDialog();

            EnableTrue();
            flag = false;
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
        private void EnableFalse(string text)//khóa 1 nút button chỉ định
        {
            if (text != null)
            {
                switch (text)
                {
                    case "A": bta.Enabled = false; break;
                    case "B": btb.Enabled = false; break;
                    case "C": btc.Enabled = false; break;
                    case "D": btd.Enabled = false; break;
                    case "E": bte.Enabled = false; break;
                    case "F": btf.Enabled = false; break;
                    case "G": btg.Enabled = false; break;
                    case "H": bth.Enabled = false; break;
                    case "I": bti.Enabled = false; break;
                    case "J": btj.Enabled = false; break;
                    case "K": btk.Enabled = false; break;
                    case "L": btl.Enabled = false; break;
                    case "M": btm.Enabled = false; break;
                    case "N": btn.Enabled = false; break;
                    case "O": bto.Enabled = false; break;
                    case "P": btp.Enabled = false; break;
                    case "Q": btq.Enabled = false; break;
                    case "R": btr.Enabled = false; break;
                    case "T": btt.Enabled = false; break;
                    case "S": bts.Enabled = false; break;
                    case "U": btu.Enabled = false; break;
                    case "V": btv.Enabled = false; break;
                    case "X": btx.Enabled = false; break;
                    case "Y": bty.Enabled = false; break;
                    case "W": btw.Enabled = false; break;
                    case "Z": btz.Enabled = false; break;
                }
            }
        }
        //Việc quay nón
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Func.pictureBox_Paint(g, pictureBox1, angle, img);
        }

        //Làm nón quay
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Random rand; //chọn ngẫu nhiên kết quả

            if (flag == false)
            {
                rand = new Random();
                ketqua = rand.Next(1, 25) * 15;
                //ketqua = 105;
                timer1.Interval = 30;
                timer1.Start();
                timer2.Start();
                btthoat.Enabled = false;
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn chữ cái", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                flag = true;
            }

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
            pictureBox1.Invalidate(); // vẽ lại trên picturebox

            if (ketqua == angle && timer2.Enabled == false)
            {
                timer1.Stop();
                lbthongbao.Text = Func.Showpoint(ketqua);
                txtdiem.Text = Func.diem.ToString();
                txtMang.Text = Func.soMang.ToString();
                if (ketqua == 105) //Quay vào ô may mắn
                {
                    string kq = Func.OMayMan(ketqua);
                    if (kq == null) //Nếu trả lời xong câu hỏi
                    {
                        ChuyenCauHoi();
                        Func.NextQuestion();
                        lbchoi.Text = Func.lbchoi;
                        txtMang.Text = Func.soMang.ToString();
                        txtdiem.Text = Func.diem.ToString();
                    }
                    else
                    {
                        EnableFalse(kq);
                        flag = true;
                    }
                }
                else
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
           this.Close();
        }
        //Vẽ nền cho form
        private void FormPlaygame_Paint(object sender, PaintEventArgs e)
        {
            Bitmap btm = new Bitmap(Application.StartupPath + @"\Picture\background1.jpg");
            Graphics g = e.Graphics;
            g.DrawImage((Image)btm, ClientRectangle);
        }

        private void FormPlaygame_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dlg = MessageBox.Show("Bạn có chắc chắn muốn thoát không ?", "Cảnh Báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dlg == DialogResult.OK)
            {
                Func.Endgame();
            }
            else
                e.Cancel = true;
        }
    }
}