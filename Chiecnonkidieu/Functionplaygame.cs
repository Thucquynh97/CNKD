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

namespace Chiecnonkidieu
{
    public class Functionplaygame
    {
        public GroupBox gbdapan { get; set; }
        public string lbstatus { get; set; }
        public string lbthongbao { get; set; }
        public ArrayList selected { get; set; }
        public int answerLength { get; set; }
        public int soMang { get; set; }
        public int diem { get; set; }
        public int countselecttrue { get; set; }
        public List<PictureBox> picture { get; set; }
        public Functionplaygame()
        {
            picture = new List<PictureBox>();
            selected = new ArrayList();
        }
        public List<PictureBox> AddPicturebox(int numQuest, ref int space)
        {

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
        
        public int CheckCharClicked(char charClicked, int numquestion) //Kiểm tra ký tự nhập đúng của người dùng
        {
            answerLength = 0; 
            bool flagmess = true; //kiểm soát chỉ cho hiện 1 lần  MessageBox.Show("Bạn đã trả lời đúng!"); Hàm selectQuestion
            Connectsql.arrAnswer1[numquestion] = Connectsql.arrAnswer1[numquestion].ToString().ToUpper();
            if (Connectsql.arrAnswer1[numquestion].ToString().Contains(charClicked))
            {
                char[] wordchar = Connectsql.arrAnswer1[numquestion].ToString().ToCharArray(); // chuyển chuỗi kết quả thành mảng kí tự

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
                            lbthongbao = "Bạn đã trả lời đúng, có " + countselecttrue + " chữ " + charClicked;
                            flagmess = false;
                        }
                        picture[i].Text = charClicked.ToString();
                        picture[i].Image = Image.FromFile(Application.StartupPath + @"\Picture\" + charClicked.ToString() + ".png");
                    }
                }
                return 1;

            }
            else
            {
                return 0;
            }
        }
    }
}
