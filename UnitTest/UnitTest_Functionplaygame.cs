using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
using Chiecnonkidieu;
namespace UnitTest
{
    [TestClass]
    public class UnitTest_Functionplaygame
    {
        private Functionplaygame Func;
        private Connectsql cn;
        [TestInitialize]
        public void Setup()
        {
            Func = new Functionplaygame();
            cn = new Connectsql();
            cn.Connect();
            cn.ImportQA(cn.mysql, "SELECT * FROM Question");
            Func.numQuest = 14;
            Func.gbdapan = new GroupBox();
        }
        //Test Function CheckCharClicked
        [TestMethod]
        //TH1: char đúng
        public void Kiemtra_CheckCharClicked_Testcase1()
        {
            Assert.IsTrue(Func.CheckCharClicked('N'));
        }
        //TH2: Char sai , soMang >= 1 
        [TestMethod]
        public void Kiemtra_CheckCharClicked_Testcase2()
        {
            Func.soMang = 2;
            Assert.IsFalse(Func.CheckCharClicked('Z'));
            Assert.IsTrue(Func.soMang == 1);
        }
        //TH3: Char sai , soMang < 1 
        [TestMethod]
        public void Kiemtra_CheckCharClicked_Testcase3()
        {
       
            Func.soMang = 0;        
            Assert.IsFalse(Func.CheckCharClicked('Z'));
            Assert.IsTrue(Func.soMang == 0);

        }
        //Test Function SelectQuestion
        [TestMethod]
        //TH1 : Char sai
        public void Kiemtra_SelectQuestion_Testcase1()
        {
            int ketqua = 0;
            char charclicked = 'V';
            Assert.IsFalse(Func.SelectQuestion(charclicked, ketqua));
        }
        [TestMethod]
        //TH2 : Char Đúng , nhưng chưa trả lời đúng hết câu hỏi
        public void Kiemtra_SelectQuestion_Testcase2()
        {
            int ketqua = 0;
            char charclicked = 'N';
            Func.AddPicturebox(Func.numQuest);
            Assert.IsFalse(Func.SelectQuestion(charclicked, ketqua));
        }

        [TestMethod]
        //TH3 : Char Đúng , Trả lời hết và đúng câu hỏi
        public void Kiemtra_SelectQuestion_Testcase3()
        {
            int ketqua = 0;
            Func.AddPicturebox(Func.numQuest);
            Func.SelectQuestion('N', ketqua);
            Func.SelectQuestion('I', ketqua);
            Func.SelectQuestion('L', ketqua);
            Assert.IsTrue(Func.SelectQuestion('E', ketqua));
        }
        //Test Func OMayMan
        [TestMethod]
        //TH1 : Không Nhập dữ liệu ô may mắn 
        public void Kiemtra_OMayMan_Testcase1()
        {
            Assert.AreEqual(Func.OMayMan(105), "");
        }
        [TestMethod]
        //TH2 : Nhập dữ liệu ô May Mắn, từ chưa lật
        public void Kiemtra_OMayMan_Testcase4()
        {
            Func.AddPicturebox(Func.numQuest);
            Assert.AreEqual(Func.OMayMan(105), "N");
        }
        [TestMethod]
        //TH3 : Nhập dữ liệu ô May Mắn nhưng từ đã lật
        public void Kiemtra_OMayMan_Testcase2()
        {
            Func.AddPicturebox(Func.numQuest);
            Func.SelectQuestion('N', 0);
            Assert.AreEqual(Func.OMayMan(105), "");
        }

        [TestMethod]
        //TH4: Nhập dữ liệu ô May Mắn và chuyển câu hỏi
        public void Kiemtra_OMayMan_Testcase3()
        {
            Func.AddPicturebox(Func.numQuest);
            Func.SelectQuestion('N', 0);
            Func.SelectQuestion('I', 0);
            Func.SelectQuestion('L', 0);
            Assert.AreEqual(Func.OMayMan(105), null);
        }
        [TestMethod]
        //Test Function DangNhap
        //TH1 :Nhập tk+mk đúng
        public void Kiemtra_DangNhap_Testcase1()
        {
            string tk = "nhom8";
            string mk = "12345";
            Assert.IsTrue(Func.DangNhap(tk, mk));
        }
        [TestMethod]
        //TH2 :Nhập tk đúng , mk sai
        public void Kiemtra_DangNhap_Testcase2()
        {
            string tk = "nhom8";
            string mk = "123456";
            Assert.IsFalse(Func.DangNhap(tk, mk));
        }
        [TestMethod]
        //TH3 :Nhập tk sai , mk đúng
        public void Kiemtra_DangNhap_Testcase3()
        {
            string tk = "nhom88";
            string mk = "12345";
            Assert.IsFalse(Func.DangNhap(tk, mk));
        }
        [TestMethod]
        //TH3 :để trống tk + mk;
        public void Kiemtra_DangNhap_Testcase4()
        {
            string tk = "";
            string mk = "";
            Assert.IsFalse(Func.DangNhap(tk, mk));
        }

        [TestMethod]
        public void Kiemtra_Addpoint()
        {
            int diem = Func.Addpoint(15);
            Assert.AreEqual(diem, 200);
        }

        [TestMethod]
        public void Kiemtra_Showpoint()
        {
            Func.diem = 0;
            Func.soMang = 0;
            Assert.AreSame(Func.Showpoint(15), "Bạn đã quay vào ô 200 điểm");
        }
        [TestMethod]
        public void Kiemtra_RandQuestion()
        {
            Func.numQuest = 4;
            Func.RandQuestion();
            Assert.IsTrue(Func.numQuest != 4);
        }
        [TestMethod]
        public void Kiemtra_NextQuestion()
        {
            Func.diem = 0;
            Func.soMang = 3;
            Func.NextQuestion();
            Assert.AreEqual(Func.diem, 500);
            Assert.AreEqual(Func.soMang, 4);
        }

    }
}
