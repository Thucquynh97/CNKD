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
        private Image img;
        private int angle; //góc của chiếc nón 15 điêm tương đương với 1 giá trị
        private float width;
        private float height;
        private List<PictureBox> picture = new List<PictureBox>();
        private int numQuest { get; set; } //câu hỏi : 0 = câu 1
        private int answerLength = 0; //kiểm tra người dùng trả lời xong câu hỏi chưa
        private int diem = 0;
        private int soMang; //mạng của người chơi
        private int space = 0;  //biến đếm số khoảng trắng trong cau trả lời
        private int countselecttrue = 0; //đém ký tự khi người dùng chọn, vd : Lai van sam , chứ A có 3 chữ
        private ArrayList selected; // mảng chứa kí tự đúng của ng dùng
        public static int select { get; set; }//lựa chọn ô may mắn của người dùng
        private Connectsql cn = null;
        private Import ip = null;
        Formluudiem dangnhap = new Formluudiem();
        public FormPlaygame()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string cnstr = ConfigurationManager.ConnectionStrings["cnstr"].ConnectionString;
            cn = new Connectsql(cnstr);
            ip = new Import();
            cn.Connect();
            selected = new ArrayList();
            img = Image.FromFile(Application.StartupPath + @"\chiecnon.png");
            txtMang.Text = "";
            groupBox2.Enabled = false;
        }

        //////////////////////////////////////////
        //Thêm kí tự chữ trong đáp án vào groupbox
        private void Addlabels()
        {
            gbdapan.Controls.Clear();
            char[] wordChars = Import.arrAnswer1[numQuest].ToString().ToCharArray(); //chuyển đáp án thành từng kí tự

            //đếm các khoảng trắng giữa các từ
            foreach (char c in wordChars)
            {
                if (c == ' ')
                    space++;
            }

            int len = wordChars.Length;
            int refer1 = gbdapan.Width / len / 2 + 7; // dùng để chia khoảng cách từng kí tự trong gourpbox
            int refer2 = gbdapan.Width / len;
            for (int i = 0; i < len; i++)
            {
                PictureBox pic = new PictureBox();
                if (wordChars[i] != ' ')
                    pic.Image = Image.FromFile(Application.StartupPath + @"\Picture\daugach.png");
                else
                    pic.Text = "";
                pic.Size = new Size(50, 50);
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                if (len <= 9)
                    pic.Location = new Point(10 * 15 + i * refer1, gbdapan.Height - 60);
                else if (len <= 12)
                    pic.Location = new Point(10 * 10 + i * (refer1 + 10), gbdapan.Height - 60);
                else if (len <= 15)
                    pic.Location = new Point(5 + i * refer2, gbdapan.Height - 60);
                else
                    pic.Location = new Point(i * refer2 + 3, gbdapan.Height - 60);
                pic.Parent = gbdapan;
                pic.BringToFront(); //mang pic ra trước groupbox, bảo đảm được nhìn thấy
                picture.Add(pic);
            }
        }

        ///////////////////////////////////////////
        //Chọn câu trả lời
        private void button1_Click(object sender, EventArgs e)
        {

            if (flag == true)
            {
                Button b = (Button)sender;
                //char charClicked = b.Text.ToCharArray()[0];
                SelectQuestion(numQuest, b.Text.ToCharArray()[0]);

                b.Enabled = false;
                if (answerLength == Import.arrAnswer1[numQuest].ToString().Length - space)
                {
                    NextQuestion();
                }
                flag = false;
            }
            else
            {
                lbthongbao.Text = "Bạn chưa quay chiếc nón kỳ diệu!";
                // MessageBox.Show("Bạn chưa quay chiếc nón kỳ diệu","Cảnh Báo",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void NextQuestion() //chuyển sang câu hỏi mới
        {

            FormChienthang frm1 = new FormChienthang();
            frm1.ShowDialog();
            diem+=500;
            soMang++;
            txtdiem.Text = diem.ToString();
            txtMang.Text = soMang.ToString();
            FormYnghiacautraloi frm2 = new FormYnghiacautraloi(numQuest);
            frm2.ShowDialog();
            picture.Clear();
            numQuest++;
            space = 0;
            answerLength = 0; //reset lại biến space và answerLength
            Addlabels();
            lbchoi.Text = "Câu " + (numQuest + 1) + " :" + Import.arrQuestion[numQuest].ToString();
            lbthongbao.Text = "";
            EnableTrue();
            selected.Clear();
        }
        ///////////////////////////////////////////
        //Xử lý nút "Chơi"
        private void btchoi_Click(object sender, EventArgs e)
        {
            numQuest = 0;
            string cnstr = ConfigurationManager.ConnectionStrings["cnstr"].ConnectionString;
            cn = new Connectsql(cnstr);
            cn.Connect();
            ip.ImportQA(cn.mysql, "SELECT *FROM Question");
            soMang = 5;
            txtMang.Text = soMang.ToString();
            txtdiem.Text = diem.ToString();
            groupBox2.Enabled = true;
            EnableTrue();
            lbchoi.Text = "Câu " + (numQuest + 1) + " :" + Import.arrQuestion[numQuest].ToString();
            Addlabels();
            pictureBox1.Enabled = true;
            btchoi.Enabled = false;
        }

        ///////////////////////////////////////////
        //Chọn câu hỏi
        private void SelectQuestion(int question, char charClicked)
        {
            bool flagmess = true; //kiểm soát chỉ cho hiện 1 lần  MessageBox.Show("Bạn đã trả lời đúng!"); Hàm selectQuestion
            Import.arrAnswer1[question] = Import.arrAnswer1[question].ToString().ToUpper();

            //Người chơi chọn đúng kí tự trong câu trả lời
            if (Import.arrAnswer1[question].ToString().Contains(charClicked))
            {
                lbstatus.Parent = gbdapan;
                char[] wordchar = Import.arrAnswer1[question].ToString().ToCharArray(); // chuyển chuỗi kết quả thành mảng kí tự

                for (int i = 0; i < wordchar.Length; i++)
                {
                    if (charClicked == wordchar[i])
                    {
                        selected.Add(wordchar[i]);
                        answerLength++;
                        if (flagmess == true)
                        {
                            for (int j = 0; j < wordchar.Length; j++) //khi người dùng chọn 1 chữ cái thì hàm sẽ kiểm tra xem                              
                            {                                         //chữ đó có bao nhiêu chữ trong kết quả
                                if (charClicked == wordchar[j])
                                    countselecttrue++;
                            }
                            lbthongbao.Text = "Bạn đã trả lời đúng, có " + countselecttrue + " chữ " + charClicked;

                            flagmess = false;
                        }
                        picture[i].Text = charClicked.ToString();
                        picture[i].Image = Image.FromFile(Application.StartupPath + @"\Picture\" + charClicked.ToString() + ".png");
                        Addpoint(ketqua);
                        txtdiem.Text = diem.ToString();
                    }
                }
                //MessageBox.Show("Có " + countselecttrue + " chữ " + charClicked);
                countselecttrue = 0;
                EnableFalse(charClicked);
            }
            else
            {
                lbthongbao.Text = "Bạn đã trả lời sai";
                if (soMang > 1)
                    soMang--;
                else
                {
                    soMang--;
                    txtMang.Text = soMang.ToString();
                    MessageBox.Show("Ban da thua\nDiem cua ban: " + diem.ToString());
                }
                txtMang.Text = soMang.ToString();
            }
        }

        ///////////////////////////////////////////
        //thêm dữ liệu vào database
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
                case 'Q': btq.Enabled = false; break;
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

        ////////////////////////////////////////////
        /// Việc quay nón
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TranslateTransform(pictureBox1.Width / 2, pictureBox1.Height / 2);
            g.RotateTransform(angle);
            g.TranslateTransform(-pictureBox1.Width / 2, -pictureBox1.Height / 2);
            g.DrawImage(img, 0, 0);
        }

        /////////////////////////////////
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
                
            }
            else
            {
                lbthongbao.Text = "Bạn chưa chọn chữ cái trả lời!";
                //MessageBox.Show("Bạn chưa chọn chữ cái","Cảnh báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                flag = true;
            }

        }

        //////////////////////////////////////////////////////////
        //Hiển thị kết quả quay được và xử lý trường hợp đặc biệt
        public void Showpoint(int x)
        {
            switch (x)
            {
                case 0:
                    {

                        MessageBox.Show("Bạn đã quay vào ô Nhân đôi điểm","Chúc Mừng",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        break;
                    }
                case 15:
                    {
                        lbthongbao.Text = "Bạn đã quay vào ô 200 điểm";
                        break;
                    }
                case 30:
                    {
                        lbthongbao.Text = "Bạn đã quay vào ô 700 điểm";
                        break;
                    }
                case 45:
                    {
                        lbthongbao.Text = "Bạn đã quay vào ô 1000 điểm";
                        break;
                    }
                case 60:
                    {
                        lbthongbao.Text = "Bạn đã quay vào ô 400 điểm";
                        break;
                    }
                case 75:
                    {
                        MessageBox.Show("Bạn đã quay vào ô Mất Mạng","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        soMang--;
                        txtMang.Text = soMang.ToString();
                        break;
                    }
                case 90:
                    {
                        lbthongbao.Text = "Bạn đã quay vào ô 900 điểm";
                        break;
                    }
                case 105:
                    {
                        MessageBox.Show("Bạn đã quay vào ô May mắn\n" +
                        "Bạn được chọn 1 ký tự", "Chúc Mừng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Formmayman frm = new Formmayman(numQuest);
                        frm.ShowDialog();
                        if (select != -1)
                        {
                            string strPattern = @"[\s]+";
                            Regex rgx = new Regex(strPattern);
                            string output = rgx.Replace(Import.arrAnswer1[numQuest].ToString().ToUpper(), "");//loại bỏ khoảng trắng (space)
                            char[] wordchar = output.ToCharArray();//chuyển mảng thành kí tự sau khi đã loại bỏ các ký tự space
                            char select2;
                            for (int i = 0; i < selected.Count; i++)
                            {
                                select2 = Convert.ToChar(selected[i]);
                                if (select2 == wordchar[select])
                                {
                                    MessageBox.Show("Từ Đã lật", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            SelectQuestion(numQuest, wordchar[select]);
                            if (answerLength == Import.arrAnswer1[numQuest].ToString().Length - space)
                            {
                                NextQuestion();
                                flag = false;
                            }
                           


                        }
                        else
                        {
                            MessageBox.Show("Hêt thời gian", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    break;
                case 120:
                    {
                        lbthongbao.Text = "Bạn đã quay vào ô 300 điểm";
                        break;
                    }
                case 135:
                    {
                        lbthongbao.Text = "Bạn đã quay vào ô 800 điểm";
                        break;
                    }
                case 150:
                    {
                        MessageBox.Show("Bạn đã quay vào ô Mất Điểm", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        diem = 0;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 165:
                    {
                        lbthongbao.Text = "Bạn đã quay vào ô 1000 điểm";
                        break;
                    }
                case 180:
                    {
                        lbthongbao.Text = "Bạn đã quay vào ô 400 điểm";
                        break;
                    }
                case 195:
                    {
                        lbthongbao.Text = "Bạn đã quay vào ô 600 điểm";
                        break;
                    }
                case 210:
                    {
                        lbthongbao.Text = "Bạn đã quay vào ô 300 điểm";
                        break;
                    }
                case 225:
                    {
                        MessageBox.Show("Bạn đã quay vào ô Thêm Mạng","Chúc Mừng",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        soMang++;
                        txtMang.Text = soMang.ToString();
                        break;
                    }
                case 240:
                    {
                        lbthongbao.Text = "Bạn đã quay vào ô 200 điểm";
                        break;
                    }
                case 255:
                    {
                        lbthongbao.Text = "Bạn đã quay vào ô 900 điểm";
                        break;
                    }
                case 270:
                    {
                        lbthongbao.Text = "Bạn đã quay vào ô 700 điểm";
                        break;
                    }
                case 285:
                    {
                        MessageBox.Show("Bạn đã quay vào ô Chia đôi điểm","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        txtdiem.Text =(diem/2).ToString();
                        break;
                    }
                case 300:
                    {
                        lbthongbao.Text = "Bạn đã quay vào ô 300 điểm";
                        break;
                    }
                case 315:
                    {
                        lbthongbao.Text = "Bạn đã quay vào ô 2000 điểm";
                        break;
                    }
                case 330:
                    {
                        lbthongbao.Text = "Bạn đã quay vào ô 100 điểm";
                        break;
                    }
                case 345:
                    {
                        lbthongbao.Text = "Bạn đã quay vào ô 500 điểm";
                        break;
                    }
                case 360:
                    {
                        MessageBox.Show("Bạn đã quay vào ô nhân đôi điểm", "Chúc Mừng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
            }
        }

        //////////////////////////////////////
        //Tính và hiện điểm cho người chơi 
        public void Addpoint(int x)
        {
            switch (x)
            {
                case 0:
                    {
                        diem = diem * 2;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 15:
                    {
                        diem += 200;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 30:
                    {
                        diem += 700;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 45:
                    {
                        diem += 1000;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 60:
                    {
                        diem += 400;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 75:
                    {
                        break;
                    }
                case 90:
                    {
                        diem += 900;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 105:
                    {

                        break;
                    }
                case 120:
                    {
                        diem += 300;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 135:
                    {
                        diem += 800;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 150:
                    {

                        break;
                    }
                case 165:
                    {
                        diem += 1000;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 180:
                    {
                        diem += 400;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 195:
                    {
                        diem += 600;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 210:
                    {
                        diem += 300;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 225:
                    {
                        soMang++;
                        txtMang.Text = soMang.ToString();
                        break;
                    }
                case 240:
                    {
                        diem += 200;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 255:
                    {
                        diem += 900;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 270:
                    {
                        diem += 700;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 285:
                    {
                        break;
                    }
                case 300:
                    {
                        diem += 300;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 315:
                    {
                        diem += 2000;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 330:
                    {
                        diem += 100;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 345:
                    {
                        diem += 500;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
                case 360:
                    {
                        diem = diem * 2;
                        txtdiem.Text = diem.ToString();
                        break;
                    }
            }
        }

        //kết thúc trò chơi
        private void Endgame()
        {
            Formluudiem frm = new Formluudiem();
            frm.ShowDialog();
            this.Hide();

        }

        /////////////////////////////////////
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
                Showpoint(ketqua);
                flag = true;
            }
        }

        ////////////////////////////////////////////////////
        //Sử dụng làm chiếc nón quay một thời gian rồi ngừng
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