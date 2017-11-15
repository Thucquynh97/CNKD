using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Chiecnonkidieu
{
    public class Connectsql
    {
        public static ArrayList arrQuestion { get; set; }
        public static ArrayList arrAnswer1 { get; set; }
        public static ArrayList arrAnswer2 { get; set; }
        public SqlConnection mysql { get; set; }
        public Connectsql()
        {
            string cnstr = ConfigurationManager.ConnectionStrings["cnstr"].ConnectionString;
            mysql = new SqlConnection(cnstr);
        }
        public void Connect()
        {
            try
            {
                if (mysql != null && mysql.State == ConnectionState.Closed)
                {
                    mysql.Open();

                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

        }
        public void Disconnect()
        {
            if (mysql.State == ConnectionState.Open)
            {
                mysql.Close();

            }
        }
        public int Delete(int id)
        {
            if (id == -1)
                return -1;
            Connect();
            SqlCommand cmd = new SqlCommand("uspDelete", mysql);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("id", id));
            int numOfdelete = cmd.ExecuteNonQuery();
            return numOfdelete;
            Disconnect();
        }
        public int AddQuestion(string cauhoi, string cautraloi, string giaithich)
        {
            if (cauhoi == "" || cautraloi == "" || giaithich == "")
                return -1;
            Connect();
            SqlCommand cmd = new SqlCommand("uspThem", mysql);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("cauhoi", cauhoi));
            cmd.Parameters.Add(new SqlParameter("cautraloi", cautraloi));
            cmd.Parameters.Add(new SqlParameter("giaithich", giaithich));
            int numOfAdd = cmd.ExecuteNonQuery();
            return numOfAdd;
            Disconnect();
        }
        public void ResetDiem()
        {
            Connect();
            SqlCommand cm = new SqlCommand("uspReset", mysql);
            cm.CommandType = CommandType.StoredProcedure;
            cm.ExecuteNonQuery();
            Disconnect();
        }
        public List<object> ExecuteReader(string sql)
        {
            SqlDataReader dr;
            List<object> list;
            if (sql == "")
            {
                list = null;
            }
            else
            {
                Connect();
                SqlCommand cmd = new SqlCommand(sql, mysql);
                try
                {
                    dr = cmd.ExecuteReader();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                list = new List<object>();
                while (dr.Read())
                {
                    var prop = new
                    {
                        id = dr["ID"],
                        cauhoi = dr["Question"],
                        cautraloi = dr["answer1"],
                        giaithich = dr["answer2"]
                    };
                    list.Add(prop);
                }
                Disconnect();

            }
            return list;

        }
        public int ImportQA(SqlConnection cn, string str)
        {
            if (cn == null || str == "")
                return -1;
            arrQuestion = new ArrayList();
            arrAnswer1 = new ArrayList();
            arrAnswer2 = new ArrayList();
            SqlDataAdapter da = new SqlDataAdapter(str, cn);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                arrQuestion.Add(dt.Rows[j][1]);
                arrAnswer1.Add(dt.Rows[j][2]);
                arrAnswer2.Add(dt.Rows[j][3]);
            }
            return 1;
        }
        public int ImportPoint(string name, int point)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("ImportPoint", mysql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@name", name));
                cmd.Parameters.Add(new SqlParameter("@point", point));
                int countOfIP = cmd.ExecuteNonQuery();
                return countOfIP;

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return -1;

        }

    }
}
