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
    public partial class changepassword : Form
    {
        public changepassword()
        {
            InitializeComponent();
            textBox2.Text = logon.name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("密码不能为空！");
            }
            else
            {
                SqlConnection conn = Class1.GetSqleverconn();
                string sql = "update login set password = '" + textBox1.Text + "'where name = '" + logon.name + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                Class1.CloseSqlServerConn(conn);
                MessageBox.Show("修改成功！");
                this.Close();
            }
        }
    }
}
