using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace 日报模板
{
    public partial class sqlserver : Form
    {
        public sqlserver()
        {
            InitializeComponent();
            textBox1.Text = "select name,count(*) from daily where time > '2015/4/01' and time < '2015/4/30'  group by name ";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = Class1.GetSqleverconn();
                string sql = textBox1.Text;
                SqlDataAdapter data = new SqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                data.Fill(ds);
                Class1.CloseSqlServerConn(conn);
                dataGridView1.DataSource = ds.Tables[0];
            }
            catch
            {
                MessageBox.Show("sql语句错误");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
