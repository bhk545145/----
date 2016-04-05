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
    public partial class customerrevise : Form
    {
        public customerrevise()
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
            textBox1.Text = logon.name;
            textBox14.Text = logon.name;
            comboBox2.Items.Add("维修");
            comboBox2.Items.Add("换新");
            dateTimePicker2.Text = DateTime.Today.ToString(); 
        }

        public customerrevise(DataRowView dv): this() //从Form2传来的数据
        {
            dateTimePicker1.Text = dv["repairtime"].ToString();
            dateTimePicker2.Text = dv["sendbacktime"].ToString();
            textBox1.Text = dv["name"].ToString();
            textBox2.Text = dv["store"].ToString();
            textBox3.Text = dv["who"].ToString();
            textBox4.Text = dv["callnumber"].ToString();
            textBox5.Text = dv["totalnumber"].ToString();
            textBox6.Text = dv["objectinformation"].ToString();
            textBox7.Text = dv["phenomenon"].ToString();
            textBox8.Text = dv["address"].ToString();
            textBox9.Text = dv["logisticsinformation"].ToString();
            textBox10.Text = dv["remarks"].ToString();
            textBox11.Text = dv["secondphenomenon"].ToString();
            textBox12.Text = dv["dailyID"].ToString();
            textBox13.Text = dv["ID"].ToString();
            textBox15.Text = dv["sendbacklogisticsinformation"].ToString();
            textBox16.Text = dv["macaddress"].ToString();
            comboBox1.Text = dv["model"].ToString();
            comboBox2.Text = dv["repairornew"].ToString();
            string radio = dv["sendback"].ToString();
            if (radio == "True")
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton2.Checked = false;
            }
            
        }
        private void button2_Click(object sender, EventArgs e)  //修改
        {
            SqlConnection conn = Class1.GetSqleverconn();
            string sql = "update receipt set sendbacktime = '" + dateTimePicker2.Value + "' ,name = '" + textBox1.Text + "',store ='" + textBox2.Text + "',who = '" + textBox3.Text + "',callnumber ='" + textBox4.Text + "',totalnumber ='" + textBox5.Text + "',objectinformation='" + textBox6.Text + "',phenomenon ='" + textBox7.Text + "',address ='" + textBox8.Text + "',logisticsinformation ='" + textBox9.Text + "',remarks ='" + textBox10.Text + "',secondphenomenon ='" + textBox11.Text + "',model ='" + comboBox1.Text + "',repairornew ='" + comboBox2.Text + "',macaddress ='"+ textBox16.Text+"',sendback ='" + radioButton2.Checked + "',sendbackname ='" + textBox14.Text + "',sendbacklogisticsinformation ='" + textBox15.Text + "' where ID ='" + textBox13.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            Class1.CloseSqlServerConn(conn);
            MessageBox.Show("修改成功！");
        }
    }
}
