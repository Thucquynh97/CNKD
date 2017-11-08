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
        public void AddPicturebox(GroupBox gbdapan,int numQuest,int space, List<PictureBox> picture)
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
        }

    }
}
