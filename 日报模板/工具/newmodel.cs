using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace 日报模板
{
    public partial class newmodel : Form
    {
        public newmodel()
        {
            InitializeComponent();
            lookup();

        }

        public void lookup()
        {
            dataGridView1.DataSource = null;
            SqlConnection conn = Class1.GetSqleverconn();
            string sql = "select * from model";
            SqlDataAdapter data = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            data.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            Class1.CloseSqlServerConn(conn);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("设备型号不能为空");
            }
            else
            {
                SqlConnection conn = Class1.GetSqleverconn();
                string sql = "insert into model(model) values( '" + textBox1.Text + "') ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                Class1.CloseSqlServerConn(conn);
                textBox1.Text = "";
            }
            lookup();
        }
    }
}
