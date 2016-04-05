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
using System.Net;

namespace 日报模板
{
    public partial class logon : Form
    {
        public static string ConnectionString;
        public static string name;
        int a = 1;
        public logon()
        {
            InitializeComponent();
            try
            {
                OleDbConnection con = this.GetOleDbconn();
                OleDbCommand cmd = new OleDbCommand("select * from admin", con);
                OleDbDataReader read = cmd.ExecuteReader();
                if (read.Read())
                {
                    textBox1.Text = read.GetString(0);
                    textBox2.Text = read.GetString(1);
                    textBox3.Text = read.GetString(2);
                    textBox4.Text = read.GetString(3);
                }
                CloseGetOleDbconn(con);
            }
            catch
            {
                a = 11;
                textBox1.Text = "192.168.1.3";
                textBox2.Text = "123456";
                textBox3.Text = "64位系统自己填";
                textBox4.Text = "123456";
                return;
            }
            
        }

        private OleDbConnection GetOleDbconn()
        {
            string str_CurrPath = Application.StartupPath + "\\bhk545145.accdb";
            string ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + str_CurrPath + ";Persist Security Info=False;";
            OleDbConnection con = new OleDbConnection(ConnectionString);
            con.Open();
            return con;
        }

        private void CloseGetOleDbconn(OleDbConnection Con)
        {
            if (Con.State == ConnectionState.Open)
            {
                Con.Close();
            }

        }

        private SqlConnection GetSqleverconn()
        {
            string ConnectionString = "Data Source='" + textBox1.Text + "';database=daily;uid=sa;pwd='" + textBox2.Text + "'";
            SqlConnection conn = new SqlConnection(ConnectionString);
            return conn;
        }
        private void CloseSqlServerConn(SqlConnection Conn)
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (a == 1)
            {
                OleDbConnection con = this.GetOleDbconn();
                OleDbCommand cmd2 = new OleDbCommand("update admin set IP = '" + textBox1.Text + "',sapassword = '" + textBox2.Text + "',name = '" + textBox3.Text + "',word ='" + textBox4.Text + "'", con);
                cmd2.ExecuteNonQuery();
            }
            else
            {

            }
            SqlConnection conn = this.GetSqleverconn();
            name = textBox3.Text;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
            string localaddr = Dns.GetHostName();
            IPHostEntry MyEntry = Dns.GetHostByName(localaddr); 
            IPAddress MyAddress = new IPAddress(MyEntry.AddressList[0].Address);
            string sql = "insert into admin (IP,sapassword,name,datetime,localaddr,localIP) values ('" + textBox1.Text + "', '" + textBox2.Text + "','" + textBox3.Text + "','" + DateTime.Now.ToString() + "','" + localaddr + "','" + MyAddress.ToString() + "') ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

            string sql1 = @"select * from admin";
            SqlCommand cmd1 = new SqlCommand(sql1, conn);
            SqlDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                string IP = dr[1].ToString();
                string sapassword = dr[2].ToString();
                ConnectionString = "Data Source='" + IP + "';database=daily;uid=sa;pwd='" + sapassword + "'";
            }
            CloseSqlServerConn(conn);

            SqlConnection conn1 = this.GetSqleverconn();
            conn1.Open();
            string sql2 = @"select * from login where name = '" + textBox3.Text + "' and password = '" + textBox4.Text + "'";
            SqlCommand cmd3 = new SqlCommand(sql2, conn1);
            SqlDataReader read = cmd3.ExecuteReader();
            if (read.HasRows)
            {
                Console.WriteLine("登陆成功");
                CloseSqlServerConn(conn1);
                this.Close();  
            }
            else
            {
                MessageBox.Show("登陆失败");
                ConnectionString = "";
                return;
            }
         
        }

        private void logon_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }


    }
}
