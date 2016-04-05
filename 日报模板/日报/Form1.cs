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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            
        }
        

        private Form2 f2 = null;
        private void button1_Click(object sender, EventArgs e)    //查看
        {
            if (f2 == null || f2.IsDisposed)
            {
                f2 = new Form2();
                f2.Show();
            }
            else
            {
                //f2.Activate();
                f2.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            }

        }
        private void button2_Click(object sender, EventArgs e)      //提交
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("登记人不能为空");
                return;
            }
            if (textBox13.Text == "")
            {
                MessageBox.Show("号码不能为空");
                return;
            }
            if (textBox8.Text == "")
            {
                MessageBox.Show("具体现象不能为空");
                return;
            }
            if (comboBox3.Text == "")
            {
                MessageBox.Show("问题归类不能为空");
                return;
            }
            dateTimePicker1.Text = DateTime.Now.ToString();
            SqlConnection conn = Class1.GetSqleverconn();
            string sql = "insert into daily(time,name,who,contactway,callnumber,model,question,APPnumber,mobilenumber,mobilesice,network,problemdescription,firstscheme,firstresult,secondscheme,secondresult,radio,area,timebuying,purchaseway,usefrequency,wheretouse) values( '" + dateTimePicker1.Value + "','" + textBox1.Text + "','" + textBox2.Text + "','" + comboBox3.Text + "','" + textBox13.Text + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text.Replace("\r", "").Replace("\n", "") + "','" + textBox9.Text.Replace("\r", "").Replace("\n", "") + "','" + textBox10.Text.Replace("\r", "").Replace("\n", "") + "','" + textBox11.Text.Replace("\r", "").Replace("\n", "") + "','" + textBox12.Text.Replace("\r", "").Replace("\n", "") + "','" + radioButton1.Checked + "','" + textBox26.Text + "','" + textBox24.Text + "','" + comboBox4.Text + "','" + comboBox5.Text + "','" + textBox25.Text + "') ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            Class1.CloseSqlServerConn(conn);
            textBox2.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            radioButton1.Checked = true;
        }
        private void button3_Click(object sender, EventArgs e)      //退出
        {
            this.Close();           
        }
        private bool windowCreate = true;
        private void notifyIcon1_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            if (this.Visible == true)
            {
                this.Hide();
            }
            else
            {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
                this.BringToFront();
                windowCreate = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Text = DateTime.Now.ToString();
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
            comboBox1.Text = "";
            comboBox4.Text = " ";
            comboBox5.Text = " ";
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

      




    }
}
