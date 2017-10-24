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
    public partial class Formtuychinh : Form
    {
        private SqlDataAdapter da = null;
        private DataSet ds = null;
        private string cnstr;
        SqlConnection cn = null;
        DataTable dt;
        bool flag = true;
        public Formtuychinh()
        {
            InitializeComponent();
        }

        private void Formtuychinh_Load(object sender, EventArgs e)
        {

            cnstr = ConfigurationManager.ConnectionStrings["cnstr"].ConnectionString;
            dt = UpdateDGV();
           // txtcauhoi.DataBindings.Add("Text", dt, "Question");
           // txttraloi.DataBindings.Add("Text", dt, "answer1");
        }
        private DataTable UpdateDGV()
        {
            DataTable dt = new DataTable();
            dt = GetData(cnstr).Tables[0];
            dgv.DataSource = dt;
            return dt;
        }
        public DataSet GetData(string cnstr)
        {
            da = new SqlDataAdapter("SELECT * FROM Question", cnstr);
            ds = new DataSet();
            da.Fill(ds);
            return ds;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            txtcauhoi.DataBindings.Clear();
            txttraloi.DataBindings.Clear();
            dr["Question"] = txtcauhoi.Text;
            dr["answer1"] = txttraloi.Text;
            ds.Tables[0].Rows.Add(dr);
            flag = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommandBuilder cb = new SqlCommandBuilder(da); // *** Bảng đơn      
            da.Update(ds.Tables[0]);
            dgv.DataSource = dt;
            flag = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
          
            if (flag == true)
            {
                txtcauhoi.Text = "";
                txttraloi.Text = "";
                MessageBox.Show("Mời bạn chọn câu để xóa");
                flag = false;
            }
            else
                MessageBox.Show("Đã xóa");
            cn = new SqlConnection(cnstr);
            cn.Open();
            string sql = "DELETE FROM[dbo].[Question] WHERE[dbo].[Question].Question = N'" + txtcauhoi.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.ExecuteNonQuery();
            dt = UpdateDGV();
            dgv.DataSource = dt;
            txtcauhoi.DataBindings.Clear();
            txttraloi.DataBindings.Clear();
            txtcauhoi.DataBindings.Add("Text", dt, "Question");
            txttraloi.DataBindings.Add("Text", dt, "answer1");
            dgv.DataSource = dt;

        }

        private void btthoat_Click(object sender, EventArgs e)
        {
            Formmain frm = new Formmain();
            frm.Show();
            this.Close();
        }

        private void txttraloi_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar)) //nếu là kí tự chữ
            {
                MessageBox.Show("Chỉ cho phép nhập chữ", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Handled = true;

            }
        }
    }
}
