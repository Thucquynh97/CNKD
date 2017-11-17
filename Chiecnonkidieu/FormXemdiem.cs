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
    public partial class FormXemdiem : Form
    {
        private Connectsql cn;
        private DataTable dt;
        private SqlDataAdapter da;
        public FormXemdiem()
        {
            InitializeComponent();
        }

        private void FormXemdiem_Load(object sender, EventArgs e)
        {
            cn = new Connectsql();
            cn.Connect();
            dt = GetData();
            dgv.DataSource = dt;
            cn.Disconnect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private DataTable GetData()
        {

            da = new SqlDataAdapter("SELECT TOP 10 * FROM Charts ORDER BY point DESC", cn.mysql);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            cn.Connect();
            cn.ResetDiem();
            dt = GetData();
            dgv.DataSource = dt;
            cn.Disconnect();
        }

        private void FormXemdiem_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
