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
    public partial class transportationrevise : Form
    {
        public transportationrevise()
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
        public transportationrevise(DataRowView dv): this() //从transportationrevise传来的数据
        {
            dateTimePicker1.Text = dv["time"].ToString();
            textBox7.Text = dv["ID"].ToString();
            textBox6.Text = dv["name"].ToString();
            comboBox1.Text = dv["model"].ToString();
            textBox2.Text = dv["who"].ToString();
            comboBox3.Text = dv["contactway"].ToString();
            textBox13.Text = dv["callnumber"].ToString();
            textBox1.Text = dv["paynumber"].ToString();
            textBox5.Text = dv["callwho"].ToString();
            textBox3.Text = dv["address"].ToString();
            textBox4.Text = dv["Refundamount"].ToString();
            textBox8.Text = dv["Reasonforrefund"].ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
             SqlConnection conn = Class1.GetSqleverconn();
            string sql = "update transportation set time ='" + dateTimePicker1.Value + "', name = '" + textBox6.Text + "',model ='" + comboBox1.Text + "',who = '" + textBox2.Text + "', contactway ='" + comboBox3.Text + "',callnumber = '" + textBox13.Text + "',paynumber = '" + textBox1.Text + "',callwho = '" + textBox5.Text + "',address = '" + textBox3.Text + "',Refundamount = '" + textBox4.Text + "',Reasonforrefund = '" + textBox8.Text + "'where ID ='" + textBox7.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            Class1.CloseSqlServerConn(conn);
            MessageBox.Show("修改成功！");
        }
        
    }
}
