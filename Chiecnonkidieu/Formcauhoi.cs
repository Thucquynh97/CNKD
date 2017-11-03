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
            GetData();
        }
        public void GetData()
        {
            list = ExecuteReader("SELECT * FROM Question ORDER BY ID");
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
            return cmd.ExecuteNonQuery();
            Disconnect();
        }
        public void Add()
        {
            string cauhoi, cautraloi, giaithich;
            cauhoi = txtcauhoi.Text.Trim();
            cautraloi = txtcautraloi.Text.Trim();
            giaithich = txtgiaithich.Text.Trim();
            Connect();
            SqlCommand cmd = new SqlCommand("uspThem", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("cauhoi", cauhoi));
            cmd.Parameters.Add(new SqlParameter("cautraloi", cautraloi));
            cmd.Parameters.Add(new SqlParameter("giaithich", giaithich));
            cmd.ExecuteNonQuery();
            Disconnect();
        }
        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgv.Columns[e.ColumnIndex] is DataGridViewButtonColumn && dgv.Columns[e.ColumnIndex].Name == "Xoa")
            {
                int row = e.RowIndex;
                int id = int.Parse(dgv.Rows[row].Cells["ID"].Value.ToString());
                int NumOfDelete = Delete(id);
                if(NumOfDelete > 0)
                {
                    MessageBox.Show("Đã Xóa");
                }
                GetData();
            }

        }

        private void btThem_Click(object sender, EventArgs e)
        {
            Add();
            GetData();
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            Formmain frm = new Formmain();
            frm.Show();
            this.Hide();
        }
    }
}
