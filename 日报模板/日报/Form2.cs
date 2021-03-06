﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace 日报模板
{
    public partial class Form2 : Form
    {
        int pageSize = 0;     //每页显示行数
        int nMax = 0;         //总记录数
        int pageCount = 0;    //页数＝总记录数/每页显示行数
        int pageCurrent = 0;   //当前页号
        int nCurrent = 0;      //当前记录行
        DataTable dtInfo = new DataTable();
        public Form2()
        {
            InitializeComponent();
            comboBox1.Items.Add("name");
            comboBox1.Items.Add("contactway");
            comboBox1.Items.Add("callnumber");
            comboBox1.Items.Add("radio");
            lookup();
            dateTimePicker1.Text = DateTime.Today.ToString();
            dateTimePicker2.Text = DateTime.Today.ToString(); 
        }

        private void button1_Click(object sender, EventArgs e)          //查看
        {
            this.dataGridView1.Size = new System.Drawing.Size(1335, 577);
            bdnInfo.Visible = true;
            button4.Visible = true;
            button5.Visible = true;
            txtCurrentPage.Visible = true;
            lblPageCount.Visible = true;
            lookup();
        }
        private void button2_Click(object sender, EventArgs e)      //查找
        {
            dataGridView1.DataSource = null;
            SqlConnection conn = Class1.GetSqleverconn();
            string sql;
            if (dateTimePicker1.Text  == dateTimePicker2.Text)
            {
                if (textBox1.Text == "")
                {
                    sql = "select * from daily order by time desc";
                }
                else
                {
                    sql = "select * from daily where " + comboBox1.Text + " ='" + textBox1.Text + "'";
                }
            }
            else if (textBox1.Text != "")
            {
                sql = "select * from daily where " + comboBox1.Text + " ='" + textBox1.Text + "' and time > ='" + dateTimePicker1.Value + "' and time < '" + dateTimePicker2.Value + "'order by time";
            }

            else 
            {
                sql = "select * from daily where  time > ='" + dateTimePicker1.Value + "' and time < '" + dateTimePicker2.Value + "'order by time";
            }
            SqlDataAdapter data = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            data.Fill(ds,"zidingyi");
            Class1.CloseSqlServerConn(conn);
            dataGridView1.DataSource = ds.Tables[0];
            this.dataGridView1.Size = new System.Drawing.Size(1335, 677);
            bdnInfo.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            txtCurrentPage.Visible = false;
            lblPageCount.Visible = false;
            look();

        }
        private void button3_Click(object sender, EventArgs e)         //导出excel
        {
            progressBar1.Visible = true;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = dataGridView1.Rows.Count;
            progressBar1.BackColor = Color.Green;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Execl files (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "Export Excel File";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName == "")
                return;
            Stream myStream;
            myStream = saveFileDialog.OpenFile();
            StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));

            string str = "";
            try
            {
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    if (i > 0)
                    {
                        str += "\t";
                    }
                    str += dataGridView1.Columns[i].HeaderText;
                }
                sw.WriteLine(str);
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    string tempStr = "";
                    for (int k = 0; k < dataGridView1.Columns.Count; k++)
                    {
                        if (k > 0)
                        {
                            tempStr += "\t";
                        }
                        tempStr += dataGridView1.Rows[j].Cells[k].Value.ToString();
                    }
                    sw.WriteLine(tempStr);
                    progressBar1.Value++;
                    Application.DoEvents();
                }
                sw.Close();
                myStream.Close();

                MessageBox.Show("导出成功");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sw.Close();
                myStream.Close();
            }


        }
        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView obj = (DataRowView)dataGridView1.Rows[dataGridView1.CurrentRow.Index].DataBoundItem;
            revise re = new revise(obj);
            re.Show();
        }
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlConnection conn = Class1.GetSqleverconn();
            if (logon.name == "Administrator")
            {
                try
                {
                    string select_id = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    string delete_by_id = "delete from daily where ID=" + select_id + "";
                    SqlCommand cmd = new SqlCommand(delete_by_id, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("删除成功！");
                }
                catch
                {
                    MessageBox.Show("请正确选择行!");
                }
                finally
                {
                    Class1.CloseSqlServerConn(conn);
                }
                try
                {
                    DataRowView drv = dataGridView1.CurrentRow.DataBoundItem as DataRowView;
                    drv.Delete();
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
            else
            {
                MessageBox.Show("您没有权限!");
            }

        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.修改ToolStripMenuItem_Click(sender, e);
        }
        public void lookup()
        {
            dataGridView1.DataSource = null;
            SqlConnection conn = Class1.GetSqleverconn();
            string sql = "select * from daily order by time desc";
            SqlDataAdapter data = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            data.Fill(ds);
            Class1.CloseSqlServerConn(conn);
            dtInfo = ds.Tables[0];
            InitDataSet();
            //dataGridView1.DataSource = ds.Tables[0];
            look();

        }
        public void look()
        {
            dataGridView1.Columns[1].HeaderCell.Value = "登记时间";
            dataGridView1.Columns[2].HeaderCell.Value = "登记人name";
            dataGridView1.Columns[3].HeaderCell.Value = "称呼";
            dataGridView1.Columns[4].HeaderCell.Value = "联系方式contactway";
            dataGridView1.Columns[5].HeaderCell.Value = "号码callnumber";
            dataGridView1.Columns[6].HeaderCell.Value = "设备型号";
            dataGridView1.Columns[7].HeaderCell.Value = "问题归类";
            dataGridView1.Columns[8].HeaderCell.Value = "APP版本号";
            dataGridView1.Columns[9].HeaderCell.Value = "手机系统版本号";
            dataGridView1.Columns[10].HeaderCell.Value = "手机型号";
            dataGridView1.Columns[11].HeaderCell.Value = "客户网络环境";
            dataGridView1.Columns[12].HeaderCell.Value = "具体现象";
            dataGridView1.Columns[13].HeaderCell.Value = "第一次联系时给的解决方案";
            dataGridView1.Columns[14].HeaderCell.Value = "反馈结果";
            dataGridView1.Columns[15].HeaderCell.Value = "第二次联系时给的解决方案";
            dataGridView1.Columns[16].HeaderCell.Value = "反馈结果";
            dataGridView1.Columns[17].HeaderCell.Value = "是否解决radio";
            dataGridView1.Columns[18].HeaderCell.Value = "最后一次修改人";
            dataGridView1.Columns[19].HeaderCell.Value = "地区";
            dataGridView1.Columns[20].HeaderCell.Value = "大概购买时间";
            dataGridView1.Columns[21].HeaderCell.Value = "什么途径购买";
            dataGridView1.Columns[22].HeaderCell.Value = "使用频率";
            dataGridView1.Columns[23].HeaderCell.Value = "用于什么家电";
            dataGridView1.RowTemplate.Height = 51;
        }
        private void InitDataSet()
        {
            pageSize = 20;      //设置页面行数
            nMax = dtInfo.Rows.Count;
            pageCount = (nMax / pageSize);    //计算出总页数
            if ((nMax % pageSize) > 0) pageCount++;
            pageCurrent = 1;    //当前页数从1开始
            nCurrent = 0;       //当前记录数从0开始
            LoadData();
        }
        private void LoadData()
        {
            int nStartPos = 0;   //当前页面开始记录行
            int nEndPos = 0;     //当前页面结束记录行
            DataTable dtTemp = dtInfo.Clone();   //克隆DataTable结构框架

            if (pageCurrent == pageCount)
            {
                nEndPos = nMax;
            }
            else
            {
                nEndPos = pageSize * pageCurrent;
            }

            nStartPos = nCurrent;
            lblPageCount.Text = '/'+pageCount.ToString();
            txtCurrentPage.Text = Convert.ToString(pageCurrent);


            //从元数据源复制记录行
            try
            {
                for (int i = nStartPos; i < nEndPos; i++)
                {
                    dtTemp.ImportRow(dtInfo.Rows[i]);
                    nCurrent++;
                }
            }
            catch
            {
                dataGridView1.Columns.Clear();
            }
            bdsInfo.DataSource = dtTemp;
            bdnInfo.BindingSource = bdsInfo;
            dataGridView1.DataSource = bdsInfo;
        }
        private void Form2_Load(object sender, EventArgs e)
        {

        }
        private void button4_Click(object sender, EventArgs e)
        {
            pageCurrent--;
            if (pageCurrent <= 0)
            {
                MessageBox.Show("已经是第一页，请点击“下一页”查看！");
                return;
            }
            else
            {
                nCurrent = pageSize * (pageCurrent - 1);
            }
            LoadData();

        }
        private void button5_Click(object sender, EventArgs e)
        {
            pageCurrent++;
            if (pageCurrent > pageCount)
            {
                MessageBox.Show("已经是最后一页，请点击“上一页”查看！");
                return;
            }
            else
            {
                nCurrent = pageSize * (pageCurrent - 1);
            }
            LoadData();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            sqlserver s1 = new sqlserver();
            s1.Show();
            this.Close();
        }
        private void dataGridView1_RowPostPaint_1(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            label1.Text = this.dataGridView1.RowCount.ToString();
            using (SolidBrush b = new SolidBrush(dataGridView1.RowHeadersDefaultCellStyle.ForeColor))
            {
                string linenum = e.RowIndex.ToString();
                int linen = Convert.ToInt32(linenum) + 1;
                string line = linen.ToString();
                e.Graphics.DrawString(line, e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 20);
            }

        }
        
        private void 需寄回ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView obj = (DataRowView)dataGridView1.Rows[dataGridView1.CurrentRow.Index].DataBoundItem;                
            cunstomer c1 = new cunstomer(obj);
            c1.Show();
            c1.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.Close();
        }

    }
}

