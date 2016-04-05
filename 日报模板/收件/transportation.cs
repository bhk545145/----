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
    public partial class transportation : Form
    {
        public transportation()
        {
            InitializeComponent();
            SqlConnection conn = Class1.GetSqleverconn();
            string sql = "select * from model";
            SqlDataAdapter data = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            data.Fill(ds);
            Class1.CloseSqlServerConn(conn);
            comboBox1.DataSource = ds.Tables[0];
            comboBox1.DisplayMember = "model";
            textBox6.Text = logon.name;
            comboBox3.Items.Add("电话");
            comboBox3.Items.Add("QQ");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox13.Text == "")
            {
                MessageBox.Show("号码不能为空");
                return;
            }
            dateTimePicker1.Text = DateTime.Now.ToString();
            SqlConnection conn = Class1.GetSqleverconn();
            string sql = "insert into transportation(time,name,who,contactway,callnumber,model,paynumber,callwho,address,Refundamount,Reasonforrefund) values( '" + dateTimePicker1.Value + "','" + textBox6.Text + "','" + textBox2.Text + "','" + comboBox3.Text + "','" + textBox13.Text + "','" + comboBox1.Text + "','" + textBox1.Text + "','" + textBox5.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox8.Text + "') ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            Class1.CloseSqlServerConn(conn);
            textBox2.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox1.Text = "";
            textBox3.Text = "";
            textBox8.Text = "";
            textBox13.Text = "";
            comboBox1.Text = "";
            comboBox3.Text = "";
        }
        private transportationlookup f2;
        private void button1_Click(object sender, EventArgs e)
        {
            if (f2 == null || f2.IsDisposed)
            {
                f2 = new transportationlookup();
                f2.Show();
            }
            else
            {
                //f2.Activate();
                f2.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            }
        }

       

    }
}
