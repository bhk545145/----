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
    public partial class revise : Form
    {
        public revise()
        {
            InitializeComponent();
            SqlConnection conn = Class1.GetSqleverconn();
            string sql = "select * from model";
            string sql1 = "select * from question";
            string sql2 = "select * from purchaseway";
            string sql3 = "select * from usefrequency";
            SqlDataAdapter data = new SqlDataAdapter(sql, conn);
            SqlDataAdapter data1 = new SqlDataAdapter(sql1, conn);
            SqlDataAdapter data2 = new SqlDataAdapter(sql2, conn);
            SqlDataAdapter data3 = new SqlDataAdapter(sql3, conn);
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();
            DataSet ds2 = new DataSet();
            DataSet ds3 = new DataSet();
            data.Fill(ds);
            data1.Fill(ds1);
            data2.Fill(ds2);
            data3.Fill(ds3);
            Class1.CloseSqlServerConn(conn);
            comboBox1.DataSource = ds.Tables[0];
            comboBox1.DisplayMember = "model";
            comboBox2.DataSource = ds1.Tables[0];
            comboBox2.DisplayMember = "question";
            comboBox4.DataSource = ds2.Tables[0];
            comboBox4.DisplayMember = "purchaseway";
            comboBox5.DataSource = ds3.Tables[0];
            comboBox5.DisplayMember = "usefrequency";
            textBox1.Text = logon.name;
            comboBox3.Items.Add("电话");
            comboBox3.Items.Add("QQ");
            comboBox3.Items.Add("京东");
            comboBox3.Items.Add("邮件");
            comboBox3.Items.Add("旺旺");
            comboBox3.Items.Add("论坛");
            comboBox3.Items.Add("微信");
            textBox1.Text = logon.name;
            textBox25.Text = logon.name;
        }

        public revise(DataRowView dv): this() //从Form2传来的数据
        {
            dateTimePicker1.Text = dv["time"].ToString();
            textBox13.Text = dv["ID"].ToString();
            textBox1.Text = dv["name"].ToString();
            textBox2.Text = dv["who"].ToString();
            comboBox3.Text = dv["contactway"].ToString();
            textBox14.Text = dv["callnumber"].ToString();
            textBox28.Text = dv["APPnumber"].ToString();
            textBox27.Text = dv["mobilenumber"].ToString();
            textBox6.Text = dv["mobilesice"].ToString();
            textBox7.Text = dv["network"].ToString();
            textBox8.Text = dv["problemdescription"].ToString();
            textBox9.Text = dv["firstscheme"].ToString();
            textBox10.Text = dv["firstresult"].ToString();
            textBox11.Text = dv["secondscheme"].ToString();
            textBox12.Text = dv["secondresult"].ToString();
            comboBox1.Text = dv["model"].ToString();
            comboBox2.Text = dv["question"].ToString();
            textBox26.Text = dv["area"].ToString();
            textBox4.Text = dv["timebuying"].ToString();
            comboBox4.Text = dv["purchaseway"].ToString();
            comboBox5.Text = dv["usefrequency"].ToString();
            textBox5.Text = dv["wheretouse"].ToString();
        }
        private void button2_Click(object sender, EventArgs e)  //修改
        {
            SqlConnection conn = Class1.GetSqleverconn();
            string sql = "update daily set who ='" + textBox2.Text + "',contactway ='" + comboBox3.Text + "',callnumber = '" + textBox14.Text + "', model ='" + comboBox1.Text + "',question = '" + comboBox2.Text + "',APPnumber = '" + textBox28.Text + "',mobilenumber = '" + textBox27.Text + "',mobilesice = '" + textBox6.Text + "',network = '" + textBox7.Text + "',problemdescription = '" + textBox8.Text.Replace("\r", "").Replace("\n", "") + "',firstscheme = '" + textBox9.Text.Replace("\r", "").Replace("\n", "") + "',firstresult = '" + textBox10.Text.Replace("\r", "").Replace("\n", "") + "',secondscheme = '" + textBox11.Text.Replace("\r", "").Replace("\n", "") + "',secondresult = '" + textBox12.Text.Replace("\r", "").Replace("\n", "") + "',radio = '" + radioButton1.Checked + "', endname = '" + textBox25.Text + "',area = '" + textBox26.Text +"',timebuying = '" + textBox4.Text + "',purchaseway = '" + comboBox4.Text + "', usefrequency = '" + comboBox5.Text + "',wheretouse = '" + textBox5.Text + "' where ID ='" + textBox13.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            Class1.CloseSqlServerConn(conn);
            MessageBox.Show("修改成功！");
        }


    }
}

