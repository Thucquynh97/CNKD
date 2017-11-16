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

        Connectsql cn = null;
        List<object> list;
        public Formcauhoi()
        {
            InitializeComponent();
        }
        private void Formcauhoi_Load(object sender, EventArgs e)
        {
            string cnstr = ConfigurationManager.ConnectionStrings["cnstr"].ConnectionString;
            cn = new Connectsql();
            GetData();
        }
        public void GetData()
        {
            list = cn.ExecuteReader("SELECT * FROM Question ORDER BY ID");
            dgv.DataSource = list;
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgv.Columns[e.ColumnIndex] is DataGridViewButtonColumn && dgv.Columns[e.ColumnIndex].Name == "Xoa")
            {
                int row = e.RowIndex;
                int id = int.Parse(dgv.Rows[row].Cells["ID"].Value.ToString());
                int NumOfDelete = cn.Delete(id);
                if(NumOfDelete > 0)
                {
                    MessageBox.Show("Đã Xóa");
                }
                GetData();
            }

        }

        private void btThem_Click(object sender, EventArgs e)
        {
            string cauhoi, cautraloi, giaithich;
            cauhoi = txtcauhoi.Text.Trim();
            cautraloi = txtcautraloi.Text.Trim();
            giaithich = txtgiaithich.Text.Trim();
            cn.AddQuestion(cauhoi,cautraloi,giaithich);
            GetData();
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Formcauhoi_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dlg = MessageBox.Show("Bạn có chắc chắn muốn thoát không ?", "Cảnh Báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dlg == DialogResult.OK)
            {
                Formmain frm = new Formmain();
                frm.Show();
            }
            else
                e.Cancel = true;

        }

        private void btThem_KeyDown(object sender, KeyEventArgs e)
        {
          
        }
    }
}
