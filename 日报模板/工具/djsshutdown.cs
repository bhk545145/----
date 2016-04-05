using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics; 


namespace 日报模板
{
    public partial class djsshutdown : Form
    {
        public djsshutdown()
        {
            InitializeComponent();
        }

        private void djsshutdown_Load(object sender, EventArgs e)
        {
            readSysLog();
            //自动滚动到最后
            this.listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
        }
        public void readSysLog()
        {
            EventLog myNewLog = new EventLog();

            myNewLog.Log = "System";
            EventLogEntryCollection myCollection = myNewLog.Entries;

            //掉电不会记录关机时间
            foreach (EventLogEntry test in myCollection)
            {
                if (test.EventID.ToString() == "6005")
                {
                    listBox1.Items.Add("开机时间：" + test.TimeGenerated);
                }
                if (test.EventID.ToString() == "6006")
                {
                    listBox1.Items.Add("关机时间：" + test.TimeGenerated);
                    listBox1.Items.Add("-------------------------------------------");

                }
            }
        }
    }
}
