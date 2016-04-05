using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;


delegate void ShowProgressDelegate(int totalStep, int currentStep);
delegate void RunTaskDelegate(int seconds);
namespace 日报模板
{
    public partial class mainforms : Form
    {
        public mainforms()
        {
            InitializeComponent();
            
        }
        private Form1 f1 = null;
        private Form2 f2 = null;
        private detection d3 = null;
        private detectionlookup d4 = null;
        public static cunstomer c5 = null;
        private customerlookup c6 = null;
        private newmodel m7 = null;
        private daojishi d8 = null;
        private changepassword c9 = null;
        private Repairtest r10 = null;
        private Repairtestlookup r11 = null;
        private workrest w12 = null;
        private transportation t13 = null;
        private void ShowNewForm(object sender, EventArgs e)
        {
            if (f1 == null || f1.IsDisposed)
            {
                f1 = new Form1();
                f1.MdiParent = this;
                f1.Show();
            }
            else
            {
                f1.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            }
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }


        private void lookup_Click(object sender, EventArgs e)
        {
            if (f2 == null || f2.IsDisposed)
            {
                f2 = new Form2();
                f2.Show();
            }
            else
            {
                f2.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            }
        }


        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (c5 == null || c5.IsDisposed)
            {
                c5 = new cunstomer();
                c5.MdiParent = this;
                c5.Show();
            }
            else
            {
                c5.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            }
        }

        private void 查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (c6 == null || c6.IsDisposed)
            {
                c6 = new customerlookup();
                c6.Show();
            }
            else
            {
                c6.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            }
        }
        private bool windowCreate = true;
        private void 最小化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (windowCreate)
            {
                base.Visible = false;
                windowCreate = false;
            }
            this.Hide();
            base.OnActivated(e);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
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

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 最大最小化ToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void menuStrip_ItemAdded(object sender, ToolStripItemEventArgs e)
        {
            if (e.Item.Text.Length == 0 || e.Item.Text == "最小化(&N)" || e.Item.Text == "还原(&R)" || e.Item.Text == "关闭(&C)")
            {
                e.Item.Visible = false;
            }
        }


        private void 收件检测ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (d3 == null || d3.IsDisposed)
            {
                d3 = new detection();
                d3.MdiParent = this;
                d3.Show();
            }
            else
            {
                d3.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            }
        }

        private void 收件检测查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (d4 == null || d4.IsDisposed)
            {
                d4 = new detectionlookup();
                //d4.MdiParent = this;
                d4.Show();
            }
            else
            {
                d4.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            }
        }

        private void 添加设备型号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m7 == null || m7.IsDisposed)
            {
                m7 = new newmodel();
                m7.MdiParent = this;
                m7.Show();
            }
            else
            {
                m7.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            }
        }

        private void 倒计时ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (d8 == null || d8.IsDisposed)
            {
                d8 = new daojishi();
                d8.MdiParent = this;
                d8.Show();
            }
            else
            {
                d8.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (c9 == null || c9.IsDisposed)
            {
                c9 = new changepassword();
                c9.Show();
            }
            else
            {
                c9.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            }
        }

        private void mainforms_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.toolStripStatusLabel.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss MMMM dddd     ");
            ABworktest();
        }
        private void ABworktest()
        {
            TimeSpan morningstartt1 = new TimeSpan(09, 00, 00);
            TimeSpan morningcloset1 = new TimeSpan(11, 00, 00);
            TimeSpan afternoonstartt1 = new TimeSpan(12, 30, 00);
            TimeSpan afternooncloset1 = new TimeSpan(18, 00, 00);
            TimeSpan morningstartt2 = new TimeSpan(09, 00, 00);
            TimeSpan morningcloset2 = new TimeSpan(12, 30, 00);
            TimeSpan afternoonstartt2 = new TimeSpan(14, 00, 00);
            TimeSpan afternooncloset2 = new TimeSpan(18, 00, 00);
            TimeSpan saturdaystartt2 = new TimeSpan(09, 00, 00);
            TimeSpan saturdaycloset2 = new TimeSpan(17, 00, 00);
            TimeSpan ts2 = DateTime.Now.TimeOfDay;
            string week = DateTime.Now.ToString("dddd");
            int days = dater(week);
            //A组
            if (days == 1)
            {
                if (ts2 > morningstartt1)
                {
                    if (ts2 < morningcloset1)
                    {
                        toolStripStatusLabel3.Text = "企业QQ+销售电话+售后电话";
                    }
                    else
                    {
                        if (ts2 < afternoonstartt1)
                        {
                            toolStripStatusLabel3.Text = "休息时间";
                        }
                        else
                        {
                            if (ts2 < afternooncloset1)
                            {
                                toolStripStatusLabel3.Text = "技术支持电话";
                            }
                            else
                            {
                                toolStripStatusLabel3.Text = "下班时间";
                            }
                        }
                    }

                }
                else
                {
                    toolStripStatusLabel3.Text = "还没上班";
                }
            }
            else if(days ==2)
            {
                if (ts2 > morningstartt2)
                {
                    if (ts2 < morningcloset2)
                    {
                        toolStripStatusLabel3.Text = "技术支持电话";
                    }
                    else
                    {
                        if (ts2 < afternoonstartt2)
                        {
                            toolStripStatusLabel3.Text = "休息时间";
                        }
                        else
                        {
                            if (ts2 < afternooncloset2)
                            {
                                toolStripStatusLabel3.Text = "企业QQ+销售电话+售后电话";
                            }
                            else
                            {
                                toolStripStatusLabel3.Text = "下班时间";
                            }
                        }
                    }

                }
                else
                {
                    toolStripStatusLabel3.Text = "还没上班";
                }
            }
            //B组
            if (days == 1)
            {
                if (ts2 > morningstartt2)
                {
                    if (ts2 < morningcloset2)
                    {
                        toolStripStatusLabel4.Text = "技术支持电话";
                    }
                    else
                    {
                        if (ts2 < afternoonstartt2)
                        {
                            toolStripStatusLabel4.Text = "休息时间";
                        }
                        else
                        {
                            if (ts2 < afternooncloset2)
                            {
                                toolStripStatusLabel4.Text = "企业QQ+销售电话+售后电话";
                            }
                            else
                            {
                                toolStripStatusLabel4.Text = "下班时间";
                            }
                        }
                    }

                }
                else
                {
                    toolStripStatusLabel4.Text = "还没上班";
                }
            }
            else if(days == 2)
            {
                if (ts2 > morningstartt1)
                {
                    if (ts2 < morningcloset1)
                    {
                        toolStripStatusLabel4.Text = "企业QQ+销售电话+售后电话";
                    }
                    else
                    {
                        if (ts2 < afternoonstartt1)
                        {
                            toolStripStatusLabel4.Text = "休息时间";
                        }
                        else
                        {
                            if (ts2 < afternooncloset1)
                            {
                                toolStripStatusLabel4.Text = "技术支持电话";
                            }
                            else
                            {
                                toolStripStatusLabel4.Text = "下班时间";
                            }
                        }
                    }

                }
                else
                {
                    toolStripStatusLabel4.Text = "还没上班";
                }
            }
            if (days == -1)
            {
                toolStripStatusLabel3.Text = "周六周日自己看着办！！  ";
                toolStripStatusLabel4.Text = "周六周日自己看着办！！  ";
            }
        }
        private int dater(string week)
        {
            int days = 0;
            if (week == "星期一")
            {
                days = 1;
            }
            else if (week == "星期二")
            {
                days = 2;
            }
            else if (week == "星期三")
            {
                days = 1;
            }
            else if (week == "星期四")
            {
                days = 2;
            }
            else if (week == "星期五")
            {
                days = 1;
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

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (r10 == null || r10.IsDisposed)
            {
                r10 = new Repairtest();
                r10.MdiParent = this;
                r10.Show();
            }
            else
            {
                r10.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            }
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (r11 == null || r11.IsDisposed)
            {
                r11 = new Repairtestlookup();
                //r11.MdiParent = this;
                r11.Show();
            }
            else
            {
                r11.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            }
        }

        private void 作息时间表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (w12 == null || w12.IsDisposed)
            {
                w12 = new workrest();
                w12.MdiParent = this;
                w12.Show();
            }
            else
            {
                w12.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            }
        }

        private void 运费报销登记ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (t13 == null || t13.IsDisposed)
            {
                t13 = new transportation();
                t13.MdiParent = this;
                t13.Show();
            }
            else
            {
                t13.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            }
        }



    }
}
