using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Data.OleDb;
using Microsoft.Win32;
using System.Threading;

namespace 日报模板
{
    public partial class daojishi : Form
    {
        public daojishi()
        {
            InitializeComponent();
            webBrowser1.Navigate("http://m.weather.com.cn/data/");
        }
        public bool flag = true;
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct TokPriv1Luid
        {
            public int Count;
            public long Luid;
            public int Attr;
        }

        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GetCurrentProcess();
        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);
        [DllImport("advapi32.dll", SetLastError = true)]
        internal static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);
        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall,
           ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);
        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool ExitWindowsEx(int uFlags, int dwReserved);
        internal const int SE_PRIVILEGE_ENABLED = 0x00000002;
        internal const int TOKEN_QUERY = 0x00000008;
        internal const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
        internal const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
        internal const int EWX_LOGOFF = 0x00000000;
        internal const int EWX_SHUTDOWN = 0x00000001;
        internal const int EWX_REBOOT = 0x00000002;
        internal const int EWX_FORCE = 0x00000004;
        internal const int EWX_POWEROFF = 0x00000008;
        internal const int EWX_FORCEIFHUNG = 0x00000010;
        private void Shutdown()//关机
        {

            System.Diagnostics.Process myProcess = new System.Diagnostics.Process(); 
            myProcess.StartInfo.FileName = "cmd.exe"; 
            myProcess.StartInfo.UseShellExecute = false; 
            myProcess.StartInfo.RedirectStandardInput = true; 
            myProcess.StartInfo.RedirectStandardOutput = true; 
            myProcess.StartInfo.RedirectStandardError = true; 
            myProcess.StartInfo.CreateNoWindow = true; 
            myProcess.Start(); 
            myProcess.StandardInput.WriteLine("shutdown -s -t 0"); 
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "打开")
            {
                timer1.Enabled = false;
                button1.Text = "关闭";
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
            }
            else
            {
                timer1.Enabled = true;
                button1.Text = "打开";
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            djssetup d1 = new djssetup();
            d1.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            djsshutdown d2 = new djsshutdown();
            d2.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void daojishi_Load(object sender, EventArgs e)
        {
            bool ok;
            TokPriv1Luid tp;
            IntPtr hproc = GetCurrentProcess();
            IntPtr htok = IntPtr.Zero;
            ok = OpenProcessToken(hproc, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref htok);
            tp.Count = 1;
            tp.Luid = 0;
            tp.Attr = SE_PRIVILEGE_ENABLED;
            ok = LookupPrivilegeValue(null, SE_SHUTDOWN_NAME, ref tp.Luid);
            ok = AdjustTokenPrivileges(htok, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero); 
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            int a = Convert.ToInt32(textBox1.Text);
            int b = Convert.ToInt32(textBox2.Text);
            int c = Convert.ToInt32(textBox3.Text);
            TimeSpan ts1 = new TimeSpan(a, b, c);
            TimeSpan ts2 = DateTime.Now.TimeOfDay;
            TimeSpan ts = ts1 - ts2;
            label1.Text = "现在时间: " + DateTime.Now.ToString() + "  " + DateTime.Now.ToString("dddd");
            label2.Text = "距离下班: " + ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟" + ts.Seconds.ToString() + "秒";
            label3.Text = Convert.ToInt32(ts.TotalSeconds) + "秒";
            /*周末*/
            string week = DateTime.Now.ToString("dddd");
            int days = dater(week);
            if (days < 0)
            {
                label4.Text = "今天是周末^_^";
            }
            else
            {
                label4.Text = "距离周末:" + days + "天";
            }
            SqlConnection conn = Class1.GetSqleverconn();
            SqlCommand cmd = new SqlCommand("select * from festival", conn);
            SqlDataReader read1 = cmd.ExecuteReader();
            read1.Read();
            /*中秋*/
            DateTime MidAutumn = Convert.ToDateTime(read1.GetString(1));
            DateTime tNow = DateTime.Now;
            TimeSpan tsMidAutumn = MidAutumn - tNow;
            label5.Text = "距离中秋:" + tsMidAutumn.Days.ToString() + "天";
            /*国庆*/
            DateTime National = Convert.ToDateTime(read1.GetString(2));
            TimeSpan tsNational = National - tNow;
            label6.Text = "距离国庆:" + tsNational.Days.ToString() + "天";
            /*元旦*/
            DateTime NewYearsDay = Convert.ToDateTime(read1.GetString(3));
            TimeSpan tsNewYearsDay = NewYearsDay - tNow;
            label9.Text = "距离元旦:" + tsNewYearsDay.Days.ToString() + "天";
            /*除夕*/
            DateTime Newyear = Convert.ToDateTime(read1.GetString(4));
            TimeSpan tsNewyear = Newyear - tNow;
            label7.Text = "距离除夕:" + tsNewyear.Days.ToString() + "天";
            /*清明节*/
            DateTime ClearBright = Convert.ToDateTime(read1.GetString(5));
            TimeSpan tsClearBright = ClearBright - tNow;
            label8.Text = "距离清明节:" + tsClearBright.Days.ToString() + "天";
            /*劳动节*/
            DateTime InternationalLabour = Convert.ToDateTime(read1.GetString(6));
            TimeSpan tsInternationalLabour = InternationalLabour - tNow;
            label10.Text = "距离劳动节:" + tsInternationalLabour.Days.ToString() + "天";
            /*端午节*/
            DateTime DragonBoat = Convert.ToDateTime(read1.GetString(7));
            TimeSpan tsDragonBoat = DragonBoat - tNow;
            label11.Text = "距离端午节:" + tsDragonBoat.Days.ToString() + "天";
            /*自定义*/
            DateTime customize = Convert.ToDateTime(read1.GetString(8));
            TimeSpan tcustomize = customize - tNow;
            label12.Text = "距离" + read1.GetString(9) + ":" + tcustomize.Days.ToString() + "天";
            Class1.CloseSqlServerConn(conn);
            if (checkBox1.Checked == true)
            {
                if (Convert.ToInt32(ts.TotalSeconds) == 0)
                {
                    Shutdown();
                }
            }
            else
            {
                
            }
        }
        private int dater(string week)
        {
            int days = 1;
            if (week == "星期一")
            {
                days = 4;
            }
            else if (week == "星期二")
            {
                days = 3;
            }
            else if (week == "星期三")
            {
                days = 2;
            }
            else if (week == "星期四")
            {
                days = 1;
            }
            else if (week == "星期五")
            {
                days = 0;
            }
            else if (week == "星期六")
            {
                days = -1;
            }
            else if (week == "星期日")
            {
                days = -1;
            }
            return days;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://m.weather.com.cn/data/");

        }



    }
}
