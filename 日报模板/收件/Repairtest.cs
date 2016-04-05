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
    public partial class Repairtest : Form
    {
        public Repairtest()
        {
            InitializeComponent();
            dateTimePicker1.Text = DateTime.Now.ToString();
            SqlConnection conn = Class1.GetSqleverconn();
            string sql = "select * from model";
            SqlDataAdapter data = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            data.Fill(ds);
            Class1.CloseSqlServerConn(conn);
            comboBox1.DataSource = ds.Tables[0];
            comboBox1.DisplayMember = "model";
            textBox1.Text = logon.name;
            //textBox2.Text = DateTime.Now.ToString("yyyyMMdd");
            Repairtestlookup();
        }

        public void Repairtestlookup()
        {
            dataGridView1.DataSource = null;
            SqlConnection conn = Class1.GetSqleverconn();
            string sql = "select * from detectionresult";
            SqlDataAdapter data = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            data.Fill(ds, "zidingyi");
            Class1.CloseSqlServerConn(conn);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                MessageBox.Show("现象描述不能为空");
                return;
            }
            dateTimePicker1.Text = DateTime.Now.ToString();
            SqlConnection conn = Class1.GetSqleverconn();
            string sql = "insert into repairtest(time,name,model,number,holder,mac,firmware,problemdescription,remarks) values( '" + dateTimePicker1.Value + "','" + textBox1.Text + "','" + comboBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox4.Text + "','" + textBox5.Text + "') ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            Class1.CloseSqlServerConn(conn);
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            comboBox1.Text = "";
        }
        int i = 0;
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            i++;
            if (i == 1)
            {
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            else
            {
                textBox4.Text = textBox4.Text + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
        }
        private Repairtestlookup d3;
        private void button2_Click(object sender, EventArgs e)
        {
            if (d3 == null || d3.IsDisposed)
            {
                d3 = new Repairtestlookup();
                d3.Show();
            }
            else
            {
                d3.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            }
        }
    }
}
