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
    class Connectsql
    {
        public SqlConnection mysql { get; set; }
        public Connectsql(string cnstr)
        {
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
            catch (InvalidOperationException ex)
            {
                throw ex;
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

    }
}
