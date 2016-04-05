using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;


namespace 日报模板
{
    public partial class djssetup : Form
    {
        public djssetup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string commsql = "update festival set MidAutumn = '" + dateTimePicker1.Text + "' where ID = 1";
            string commsql1 = "update festival set [National] = '" + dateTimePicker2.Text + "' where ID = 1";
            string commsql2 = "update festival set NewYearsDay = '" + dateTimePicker3.Text + "' where ID = 1";
            string commsql3 = "update festival set Newyear = '" + dateTimePicker4.Text + "' where ID = 1";
            string commsql4 = "update festival set ClearBright = '" + dateTimePicker5.Text + "' where ID = 1";
            string commsql5 = "update festival set InternationalLabour = '" + dateTimePicker6.Text + "' where ID = 1";
            string commsql6 = "update festival set DragonBoat = '" + dateTimePicker7.Text + "' where ID = 1";
            string commsql7 = "update festival set customize = '" + dateTimePicker8.Text + "' where ID = 1";
            string commsql8 = "update festival set customizes = '" + textBox1.Text + "' where ID = 1";
            Update(commsql);
            Update(commsql1);
            Update(commsql2);
            Update(commsql3);
            Update(commsql4);
            Update(commsql5);
            Update(commsql6);
            Update(commsql7);
            Update(commsql8);
        }
        private void Update(string commsql)
        {
            SqlConnection conn = Class1.GetSqleverconn();
            SqlCommand comm = new SqlCommand (commsql,conn);
            comm.ExecuteNonQuery();
            Class1.CloseSqlServerConn(conn);
        }

        private void djssetup_Load(object sender, EventArgs e)
        {
            SqlConnection conn = Class1.GetSqleverconn();
            SqlCommand cmd = new SqlCommand("select * from festival", conn);
            SqlDataReader read1 = cmd.ExecuteReader();
            read1.Read();
            dateTimePicker1.Text = read1.GetString(1);
            dateTimePicker2.Text = read1.GetString(2);
            dateTimePicker3.Text = read1.GetString(3);
            dateTimePicker4.Text = read1.GetString(4);
            dateTimePicker5.Text = read1.GetString(5);
            dateTimePicker6.Text = read1.GetString(6);
            dateTimePicker7.Text = read1.GetString(7);
            dateTimePicker8.Text = read1.GetString(8);
            textBox1.Text = read1.GetString(9);
            Class1.CloseSqlServerConn(conn);
        }
    }
}
