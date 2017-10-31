using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
namespace Chiecnonkidieu
{
    public partial class Formcauhoi : Form
    {

        private string cnstr;
        SqlConnection cn = null;
        List<object> list;
        public Formcauhoi()
        {
            InitializeComponent();
        }
        private void Formcauhoi_Load(object sender, EventArgs e)
        {
            cnstr = ConfigurationManager.ConnectionStrings["cnstr"].ConnectionString;
            cn = new SqlConnection(cnstr);
            list = ExecuteReader("SELECT * FROM Question");
            dgv.DataSource = list;
        }

        public List<object> ExecuteReader(string sql)
        {
            Connect();
            SqlCommand cmd = new SqlCommand(sql, cn);
            SqlDataReader dr = cmd.ExecuteReader();
            List<object> list = new List<object>();
            while(dr.Read())
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
            return list;
          
        }
        public void Connect()
        {
            if(cn != null && cn.State == ConnectionState.Closed)
            {
                try
                {
                    cn.Open();
                }
                catch (SqlException ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void Disconnect()
        {
            if (cn != null && cn.State == ConnectionState.Open)
            {
                cn.Close();
            }
        }
        public int Delete(int id)
        {
            Connect();
            SqlCommand cmd = new SqlCommand("uspDelete", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("id", id));
            Disconnect();
            return cmd.ExecuteNonQuery();
        }
        public void Add(int id)
        {
            Connect();
            SqlCommand cmd = new SqlCommand("uspSua", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("id", id));
            Disconnect();
        }
        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgv.Columns[e.ColumnIndex] is DataGridViewButtonColumn && dgv.Columns[e.ColumnIndex].Name == "Delete")
            {
                int row = e.RowIndex;
                int id = int.Parse(dgv.Rows[row].Cells["ID"].Value.ToString());
                int NumOfDelete = Delete(id);
                if(NumOfDelete > 0)
                {
                    MessageBox.Show("Đã Xóa");
                }
                list = ExecuteReader("SELECT * FROM Question");
                dgv.DataSource = list;
            }
            else if (dgv.Columns[e.ColumnIndex] is DataGridViewButtonColumn && dgv.Columns[e.ColumnIndex].Name == "Sua")
            {
                int row = e.RowIndex;
                int id = int.Parse(dgv.Rows[row].Cells["ID"].Value.ToString());
                int NumOfDelete = Delete(id);
                if (NumOfDelete > 0)
                {
                    MessageBox.Show("Đã Xóa");
                }
                list = ExecuteReader("SELECT * FROM Question");
                dgv.DataSource = list;
            }


        }
    }
}
