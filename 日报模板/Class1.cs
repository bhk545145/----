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
    public static class Class1
    {
        public static SqlConnection GetSqleverconn()
        {
            string ConnectionString = logon.ConnectionString;
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            return conn;
        }
        public static void CloseSqlServerConn(SqlConnection Conn)
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }

        }
     }
}




