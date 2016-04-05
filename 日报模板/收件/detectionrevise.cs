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
    public partial class detectionrevise : Form
    {
        public detectionrevise()
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
            Class1.CloseSqlServerConn(conn);
        }

        public detectionrevise(DataRowView dv): this() //从detectionlookup传来的数据
        {
            dateTimePicker1.Text = dv["time"].ToString();
            textBox8.Text = dv["ID"].ToString();
            textBox1.Text = dv["name"].ToString();
            comboBox1.Text = dv["model"].ToString();
            textBox2.Text = dv["number"].ToString();
            textBox3.Text = dv["holder"].ToString();
            textBox6.Text = dv["mac"].ToString();
            textBox7.Text = dv["firmware"].ToString();
            textBox4.Text = dv["problemdescription"].ToString();
            textBox5.Text = dv["remarks"].ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = Class1.GetSqleverconn();
            string sql = "update detection set time ='" + dateTimePicker1.Value + "' ,name = '" + textBox1.Text + "',model ='" + comboBox1.Text + "',number = '" + textBox2.Text + "', holder ='" + textBox3.Text + "',mac = '" + textBox6.Text + "',firmware = '" + textBox7.Text + "',problemdescription = '" + textBox4.Text + "',remarks = '" + textBox5.Text + "'where ID ='" + textBox8.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            Class1.CloseSqlServerConn(conn);
            MessageBox.Show("修改成功！");
        }
        
    }
}
