using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chiecnonkidieu;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace UnitTest
{
    [TestClass]
    public class UTConnectsql
    {
        private Connectsql cn = null;
        [TestInitialize]
        public void Setup()
        {
            cn = new Connectsql();
        }
        [TestMethod]
        public void Kiemtra_Phuongthucconnect()
        {
            cn.Connect();
            Assert.IsTrue(cn.mysql.State == ConnectionState.Open);
        }
        [TestMethod, Ignore]
        [ExpectedException(typeof(SqlException))]
        public void Kiemtra_Phuongthucconnect_Nhapsaichuoiketnoi()
        {
            string cnstr = ConfigurationManager.ConnectionStrings["cnstr"].ConnectionString;
            cn.mysql = new SqlConnection(cnstr);
            cn.Connect();
        }
        [TestMethod]
        public void Kiemtra_PhuongthucDisconnect()
        {
            cn.Connect();
            cn.Disconnect();
            Assert.IsTrue(cn.mysql.State == ConnectionState.Closed);
        }
        [TestMethod, Ignore]

        public void Kiemtra_PhuongthucDelect_IDdung()
        {
            int id = 10;
            cn.Connect();
            Assert.AreEqual(cn.Delete(id), 1);

        }
        [TestMethod]
        public void Kiemtra_PhuongthucDelect_IDsai()
        {
            int id = 1;
            cn.Connect();
            Assert.AreEqual(cn.Delete(id), 0);

        }
        [TestMethod]
        public void Kiemtra_PhuongthucDelect_IDrong()
        {
            int id = -1;
            cn.Connect();
            Assert.AreEqual(cn.Delete(id), -1);

        }

        [TestMethod,Ignore]
        public void Kiemtra_PhuongthucAddquestion_nhapdaydu()
        {
            string cauhoi = "câu hỏi";
            string cautraloi = "Câu trả lời";
            string giaithich = "Giải thích";
            Assert.AreEqual(cn.AddQuestion(cauhoi, cautraloi, giaithich), 1);

        }
        [TestMethod]
        public void Kiemtra_PhuongthucAddquestion_khongnhapcauhoi()
        {
            string cauhoi = "";
            string cautraloi = "Câu trả lời";
            string giaithich = "Giải thích";
            Assert.AreEqual(cn.AddQuestion(cauhoi, cautraloi, giaithich), -1);

        }
        [TestMethod]
        public void Kiemtra_PhuongthucAddquestion_khongcautraloi()
        {
            string cauhoi = "Câu hỏi";
            string cautraloi = "";
            string giaithich = "Giải thích";
            Assert.AreEqual(cn.AddQuestion(cauhoi, cautraloi, giaithich), -1);

        }
        [TestMethod]
        public void Kiemtra_PhuongthucAddquestion_khongnhapgiaithich()
        {
            string cauhoi = "Câu hỏi";
            string cautraloi = "Câu trả lời";
            string giaithich = "";
            Assert.AreEqual(cn.AddQuestion(cauhoi, cautraloi, giaithich), -1);

        }
        [TestMethod]
        public void Kiemtra_PhuongthucAddquestion_khongnhapgihet()
        {
            string cauhoi = "";
            string cautraloi = "";
            string giaithich = "";
            Assert.AreEqual(cn.AddQuestion(cauhoi, cautraloi, giaithich), -1);

        }

        [TestMethod]
        public void Kiemtra_PhuongthucExcuteReader_Rong()
        {
            string sql = "";
            Assert.IsNull(cn.ExecuteReader(sql));
        }
        [TestMethod]
        public void Kiemtra_PhuongthucExcuteReader_Nhapsqldung()
        {
            string sql = "SELECT * FROM Question";
            Assert.IsNotNull(cn.ExecuteReader(sql));
        }
        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void Kiemtra_PhuongthucExcuteReader_Nhapsqlsai()
        {
            string sql = "SELECT * FROM Questions";
            Assert.IsNotNull(cn.ExecuteReader(sql));
        }

        [TestMethod]
        public void Kiemtra_PhuongthucImportQA_Nhapdung()
        {
            string str = "SELECT *FROM Question";
            string cnstr = ConfigurationManager.ConnectionStrings["cnstr"].ConnectionString;
            SqlConnection sqlcn = new SqlConnection(cnstr);
            Assert.AreEqual(cn.ImportQA(sqlcn, str), 1);
        }
        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void Kiemtra_PhuongthucImportQA_Nhapsai()
        {
            string str = "SELECT * FROM Questions";
            string cnstr = ConfigurationManager.ConnectionStrings["cnstr"].ConnectionString;
            SqlConnection sqlcn = new SqlConnection(cnstr);
            cn.ImportQA(sqlcn, str);
        }
        [TestMethod]
        public void Kiemtra_PhuongthucImportQA_Rong()
        {
            string str = "";
            string cnstr = ConfigurationManager.ConnectionStrings["cnstr"].ConnectionString;
            SqlConnection sqlcn = new SqlConnection(cnstr);
            Assert.AreEqual(cn.ImportQA(sqlcn, str), -1);
        }


    }
}
