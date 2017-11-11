﻿using System;
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

namespace Chiecnonkidieu
{
    public class Functionplaygame
    {
        public GroupBox gbdapan { get; set; }
        public string lbchoi { get; set; } //Biến lưu câu hỏi lấy từ csdl
        public int soMang { get; set; }
        public int diem { get; set; }
        public ArrayList selected { get; set; } // mảng chứa kí tự đúng của ng dùng
        public int numQuest { get; set; }
        private int answerLength; //kiểm tra người dùng trả lời xong câu hỏi chưa
        private int countselecttrue; //Đêm câu trả lời đúng của ng dùng => người dùng đã trả lời xog câu hỏi chưa
        private List<PictureBox> picture; //Lưu từng ký tự của câu trả lời
        private int space; //Tính số khoảng trắng 
        private bool flag; //Biến cờ kiểm soát thao tác người dùng VD:Quay nón xong phải chọn chữ và ngược lại
        private int IQ = 0; //Biến đếm I câu hỏi
        private Connectsql cn = null;
        public Functionplaygame()
        {
            cn = new Connectsql();
            picture = new List<PictureBox>();
            selected = new ArrayList();
        }

        public bool CheckCharClicked(char charClicked) //Kiểm tra ký tự nhập đúng của người dùng
        {
            Connectsql.arrAnswer1[numQuest] = Connectsql.arrAnswer1[numQuest].ToString().ToUpper();

            //Người chơi chọn đúng kí tự trong câu trả lời
            if (Connectsql.arrAnswer1[numQuest].ToString().Contains(charClicked))
            {
                return true;
            }
            else
            {

                if (soMang > 1)
                    soMang--;
                else
                {
                    soMang--;
                }
                return false;
            }
        }
        public void StartGame()
        {
            cn.Connect();
            if (cn.ImportQA(cn.mysql, "SELECT *FROM Question") != -1)
            {
                soMang = 2;
                AddPic();
            }
            else
                MessageBox.Show("Kết nối bị lỗi", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public bool SelectQuestion(char charClicked, int ketqua)
        {

            char[] wordchar = Connectsql.arrAnswer1[numQuest].ToString().ToCharArray(); // chuyển chuỗi kết quả thành mảng kí tự

            for (int i = 0; i < wordchar.Length; i++)
            {
                if (charClicked == wordchar[i])
                {
                    selected.Add(wordchar[i]);
                    answerLength++;
                    picture[i].Text = charClicked.ToString();
                    picture[i].Image = Image.FromFile(Application.StartupPath + @"\Picture\" + charClicked.ToString() + ".png");

                }
            }
            for (int i = 0; i < wordchar.Length; i++) //khi người dùng chọn 1 chữ cái thì hàm sẽ kiểm tra xem                              
            {                                         //chữ đó có bao nhiêu chữ trong kết quả

                if (charClicked == wordchar[i])
                    countselecttrue++;
            }

            DialogResult dlg = MessageBox.Show("Có " + countselecttrue + " chữ " + charClicked, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            for (int i = 0; i < countselecttrue; i++)
                diem += Addpoint(ketqua);
            countselecttrue = 0;
            if (dlg == DialogResult.OK)
            {
                if (answerLength == Connectsql.arrAnswer1[numQuest].ToString().Length - space)
                {
                    NextQuestion();
                    return true;
                }
            }
            return false;



        }
        public int Addpoint(int x)
        {
            int diem = 0;
            switch (x)
            {
                case 0:
                    {

                        break;
                    }
                case 15:
                    {
                        diem += 200;
                        break;
                    }
                case 30:
                    {
                        diem += 700;
                        break;
                    }
                case 45:
                    {
                        diem += 1000;
                        break;
                    }
                case 60:
                    {
                        diem += 400;
                        break;
                    }
                case 75:
                    {
                        break;
                    }
                case 90:
                    {
                        diem += 900;
                        break;
                    }
                case 105:
                    {

                        break;
                    }
                case 120:
                    {
                        diem += 300;
                        break;
                    }
                case 135:
                    {
                        diem += 800;
                        break;
                    }
                case 150:
                    {

                        break;
                    }
                case 165:
                    {
                        diem += 1000;
                        break;
                    }
                case 180:
                    {
                        diem += 400;
                        break;
                    }
                case 195:
                    {
                        diem += 600;
                        break;
                    }
                case 210:
                    {
                        diem += 300;
                        break;
                    }
                case 225:
                    {
                        break;
                    }
                case 240:
                    {
                        diem += 200;
                        break;
                    }
                case 255:
                    {
                        diem += 900;
                        break;
                    }
                case 270:
                    {
                        diem += 700;
                        break;
                    }
                case 285:
                    {
                        break;
                    }
                case 300:
                    {
                        diem += 300;
                        break;
                    }
                case 315:
                    {
                        diem += 2000;
                        break;
                    }
                case 330:
                    {
                        diem += 100;
                        break;
                    }
                case 345:
                    {
                        diem += 500;
                        break;
                    }
                case 360:
                    {

                        break;
                    }
            }
            return diem;
        }
        public string Showpoint(int x)
        {
            string lbthongbao = "";
            switch (x)
            {
                case 0:
                    {
                        diem = diem * 2;
                        MessageBox.Show("Bạn đã quay vào ô Nhân đôi điểm", "Chúc Mừng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                case 15:
                    {
                        lbthongbao = "Bạn đã quay vào ô 200 điểm";
                        break;
                    }
                case 30:
                    {
                        lbthongbao = "Bạn đã quay vào ô 700 điểm";
                        break;
                    }
                case 45:
                    {
                        lbthongbao = "Bạn đã quay vào ô 1000 điểm";
                        break;
                    }
                case 60:
                    {
                        lbthongbao = "Bạn đã quay vào ô 400 điểm";
                        break;
                    }
                case 75:
                    {
                        MessageBox.Show("Bạn đã quay vào ô Mất Mạng", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        soMang--;
                        break;
                    }
                case 90:
                    {
                        lbthongbao = "Bạn đã quay vào ô 900 điểm";
                        break;
                    }
                case 105:
                    {
                        //MessageBox.Show("Bạn đã quay vào ô May mắn\n" +
                        //"Bạn được chọn 1 ký tự", "Chúc Mừng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Formmayman frm = new Formmayman(numQuest);
                        //frm.ShowDialog();
                        //if (select != -1)
                        //{
                        //    string strPattern = @"[\s]+";
                        //    Regex rgx = new Regex(strPattern);
                        //    string output = rgx.Replace(Connectsql.arrAnswer1[numQuest].ToString().ToUpper(), "");//loại bỏ khoảng trắng (space)
                        //    char[] wordchar = output.ToCharArray();//chuyển mảng thành kí tự sau khi đã loại bỏ các ký tự space
                        //    char select2;
                        //    for (int i = 0; i < selected.Count; i++)
                        //    {
                        //        select2 = Convert.ToChar(selected[i]);
                        //        if (select2 == wordchar[select])
                        //        {
                        //            MessageBox.Show("Từ Đã lật", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //            return;
                        //        }
                        //    }
                        //    SelectQuestion(numQuest, wordchar[select]);
                        //    if (answerLength == Connectsql.arrAnswer1[numQuest].ToString().Length - space)
                        //    {
                        //        Func.NextQuestion();
                        //        flag = false;
                        //    }



                        //}
                        //else
                        //{
                        //    MessageBox.Show("Hêt thời gian", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //}
                    }
                    break;
                case 120:
                    {
                        lbthongbao = "Bạn đã quay vào ô 300 điểm";
                        break;
                    }
                case 135:
                    {
                        lbthongbao = "Bạn đã quay vào ô 800 điểm";
                        break;
                    }
                case 150:
                    {
                        MessageBox.Show("Bạn đã quay vào ô Mất Điểm", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        diem = 0;
                        break;
                    }
                case 165:
                    {
                        lbthongbao = "Bạn đã quay vào ô 1000 điểm";
                        break;
                    }
                case 180:
                    {
                        lbthongbao = "Bạn đã quay vào ô 400 điểm";
                        break;
                    }
                case 195:
                    {
                        lbthongbao = "Bạn đã quay vào ô 600 điểm";
                        break;
                    }
                case 210:
                    {
                        lbthongbao = "Bạn đã quay vào ô 300 điểm";
                        break;
                    }
                case 225:
                    {
                        MessageBox.Show("Bạn đã quay vào ô Thêm Mạng", "Chúc Mừng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        soMang++;
                        break;
                    }
                case 240:
                    {
                        lbthongbao = "Bạn đã quay vào ô 200 điểm";
                        break;
                    }
                case 255:
                    {
                        lbthongbao = "Bạn đã quay vào ô 900 điểm";
                        break;
                    }
                case 270:
                    {
                        lbthongbao = "Bạn đã quay vào ô 700 điểm";
                        break;
                    }
                case 285:
                    {
                        MessageBox.Show("Bạn đã quay vào ô Chia đôi điểm", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        diem = diem / 2;
                        break;
                    }
                case 300:
                    {
                        lbthongbao = "Bạn đã quay vào ô 300 điểm";
                        break;
                    }
                case 315:
                    {
                        lbthongbao = "Bạn đã quay vào ô 2000 điểm";
                        break;
                    }
                case 330:
                    {
                        lbthongbao = "Bạn đã quay vào ô 100 điểm";
                        break;
                    }
                case 345:
                    {
                        lbthongbao = "Bạn đã quay vào ô 500 điểm";
                        break;
                    }
                case 360:
                    {
                        diem = diem * 2;
                        MessageBox.Show("Bạn đã quay vào ô nhân đôi điểm", "Chúc Mừng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
            }
            return lbthongbao;
        }
        public List<PictureBox> AddPicturebox(int numQuest)
        {
            space = 0;
            gbdapan.Controls.Clear();
            char[] wordChars = Connectsql.arrAnswer1[numQuest].ToString().ToCharArray(); //chuyển đáp án thành từng kí tự

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
            return picture;
        }
        public void AddPic()
        {
            RandQuestion();
            IQ++;
            lbchoi = "Câu " + IQ + " :" + Connectsql.arrQuestion[numQuest].ToString();
            picture = AddPicturebox(numQuest);
        }
        public void RandQuestion()
        {
            Random rand = new Random();
            numQuest = rand.Next(0, Connectsql.arrQuestion.Count);
        }
        public void NextQuestion() //chuyển sang câu hỏi mới
        {
            diem += 500;
            soMang++;
            picture.Clear();
            selected.Clear();
            space = 0;
            answerLength = 0; //reset lại biến space và answerLength
            AddPic();

        }
    }
}
