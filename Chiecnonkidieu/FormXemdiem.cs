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
        public FormXemdiem()
        {
            InitializeComponent();
        }

        private void FormXemdiem_Load(object sender, EventArgs e)
        {
            string cnstr = ConfigurationManager.ConnectionStrings["cnstr"].ConnectionString;
            Connectsql cn = new Connectsql(cnstr);
            cn.Connect();
            SqlDataAdapter da = new SqlDataAdapter("SELECT TOP 10 * FROM Charts ORDER BY point DESC", cn.mysql);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgv.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
