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
    public partial class cunstomer : Form
    {
        public cunstomer()
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
            comboBox2.Items.Add("维修");
            comboBox2.Items.Add("换新");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox7.Text == "")
            {
                MessageBox.Show("故障现象不能为空");
                return;
            }
            SqlConnection conn = Class1.GetSqleverconn();
            dateTimePicker1.Text = DateTime.Now.ToString();
            string sql = "insert into receipt(dailyID,repairtime,name,store,who,callnumber,model,totalnumber,objectinformation,phenomenon,repairornew,macaddress,address,logisticsinformation,remarks,secondphenomenon,sendback) values('" + textBox12.Text + "','" + dateTimePicker1.Value + "','" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + comboBox1.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + comboBox2.Text + "','" + textBox13.Text + "','" + textBox8.Text.Replace("\r", "").Replace("\n", "") + "','" + textBox9.Text.Replace("\r", "").Replace("\n", "") + "','" + textBox10.Text.Replace("\r", "").Replace("\n", "") + "','" + textBox11.Text.Replace("\r", "").Replace("\n", "") + "', '" + radioButton2.Checked + "' ) ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            Class1.CloseSqlServerConn(conn);
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
        }
        private customerlookup c1 = null;
        private void button1_Click(object sender, EventArgs e)
        {
            if (c1 == null || c1.IsDisposed)
            {
                c1 = new customerlookup();
                c1.Show();
            }
            else
            {
                c1.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            }
        }
        public cunstomer(DataRowView dv): this() //从Form2传来的数据
        {
            textBox12.Text = dv["ID"].ToString();
            textBox4.Text = dv["callnumber"].ToString();
            comboBox1.Text = dv["model"].ToString();
        }
    }
}
