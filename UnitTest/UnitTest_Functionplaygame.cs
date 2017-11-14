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
        }
        //Test Function CheckCharClicked
        [TestMethod]
        //TH1: char đúng
        public void Kiemtra_CheckCharClicked_Testcase1()
        {
            Func.numQuest = 0;
            Assert.IsTrue(Func.CheckCharClicked('B'));
        }
        //TH2: Char sai , soMang >= 1 
        [TestMethod]
        public void Kiemtra_CheckCharClicked_Testcase2()
        {
            Func.numQuest = 0;
            Func.soMang = 2;
            Assert.IsFalse(Func.CheckCharClicked('Q'));
            Assert.IsTrue(Func.soMang == 1);
        }
        //TH3: Char sai , soMang < 1 
        [TestMethod]
        public void Kiemtra_CheckCharClicked_Testcase3()
        {
       
            Func.numQuest = 0;
            Func.soMang = 0;        
            Assert.IsFalse(Func.CheckCharClicked('Q'));
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
            char charclicked = 'B';
            Func.gbdapan = new GroupBox();
            Func.AddPicturebox(Func.numQuest);
            Assert.IsFalse(Func.SelectQuestion(charclicked, ketqua));
        }

        [TestMethod]
        //TH3 : Char Đúng , Trả lời hết và đúng câu hỏi
        public void Kiemtra_SelectQuestion_Testcase3()
        {
            int ketqua = 0;
            Func.gbdapan = new GroupBox();
            Func.AddPicturebox(Func.numQuest);
            Func.SelectQuestion('A', ketqua);
            Assert.IsTrue(Func.SelectQuestion('B', ketqua));
        }
        //Test Func OMayMan
        [TestMethod]
        //TH1 : Không Nhập dữ liệu ô may mắn 
        public void Kiemtra_OMayMan_Testcase1()
        {
            Func.numQuest = 0;
            Assert.AreEqual(Func.OMayMan(105), "");
        }
        [TestMethod]
        //TH2 : Nhập dữ liệu ô May Mắn nhưng từ đã lật
        public void Kiemtra_OMayMan_Testcase2()
        {
            Func.numQuest = 0;
            Func.gbdapan = new GroupBox();
            Func.AddPicturebox(Func.numQuest);
            Func.SelectQuestion('A', 0);
            Assert.AreEqual(Func.OMayMan(105), "");
        }
        [TestMethod]
        //TH2 : Nhập dữ liệu ô May Mắn và chuyển câu hỏi
        public void Kiemtra_OMayMan_Testcase3()
        {
            Func.numQuest = 0;
            Func.gbdapan = new GroupBox();
            Func.AddPicturebox(Func.numQuest);
            Func.SelectQuestion('A', 0);
            Assert.AreEqual(Func.OMayMan(105), null);
        }
        [TestMethod]
        //TH2 : Nhập dữ liệu ô May Mắn, từ chưa lật
        public void Kiemtra_OMayMan_Testcase4()
        {
            Func.numQuest = 0;
            Func.gbdapan = new GroupBox();
            Func.AddPicturebox(Func.numQuest);
            Assert.AreEqual(Func.OMayMan(105), "A");
        }



    }
}
