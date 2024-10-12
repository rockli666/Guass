using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Gauss
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private DATA Obs;
        bool xy = false;
        string name = "";
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void 打开文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "(txt文件)|*txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    name = Path.GetFileName(openFileDialog1.FileName);
                    Obs = FileHelper.ReadFile(openFileDialog1.FileName,name);
                    UpdateViews();
                }
                catch (Exception)
                {
                    throw new Exception("数据导入失败！");
                }
            }
        }


        #region 高斯正反算
        int index = 0;
        private void 正算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((name == "反算" || name == "反算.txt") && index == 0) || Obs == null)
            {
                MessageBox.Show("请先进行反算");
            }
            else
            {
                Calacute pos = new Calacute(Obs.Datum, Obs.L0);
                for (int i = 0; i < Obs.Data.Count; i++)
                {
                    double x, y;
                    double B = Obs.Data[i].B;
                    double L = Obs.Data[i].L;
                    pos.BL2xy(B, L, out x, out y);
                    Obs.Data[i].x = x;
                    Obs.Data[i].y = y;
                }
                index++;
                xy = true;
                UpdateViews();
            }
        }

        private void 反算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((name == "正算" || name == "正算.txt")&& index == 0) || Obs== null)
            {
                MessageBox.Show("请先进行正算");
            }
            else
            {
                Calacute pos = new Calacute(Obs.Datum, Obs.L0);
                double B, L;
                for (int i = 0; i < Obs.Data.Count; i++)
                {
                    double x = Obs.Data[i].x;
                    double y = Obs.Data[i].y;
                    pos.xy2BL(x, y, out B, out L);
                    Obs.Data[i].B = B; Obs.Data[i].L = L;
                }
                index++;
                xy = false;
                UpdateViews();
            }
        }
        #endregion

        #region 数据显示
        /// <summary>
        /// 更新表格
        /// </summary>
        void UpdateViews()
        {
            richTextBox1.Text = Report();
            dataGridView1.DataSource = Obs.ToDataTable();
        }
        /// <summary>
        /// 更新报告
        /// </summary>
        /// <returns></returns>
        string Report()
        {
            string res = Obs.ToString();

            if (xy)
            {
                res += string.Format("\r\n 正算 （BL-->xy）\r\n");
            }
            else
            {
                res += string.Format("\r\n  反算 （xy-->BL）\r\n");
            }
            res += "--------------------------------------\r\n";
                res += $"{"点名",-5} {"B",10} {"L",10}";
                res += $" {"x",10:f4} {"y",10:f4} \r\n";
                foreach (var d in Obs.Data)
                {
                    res += $"{d.Name,-5} {GeoPro.Rad2Dms(d.B).ToString("F6"),10} {GeoPro.Rad2Dms(d.L).ToString("F6"),10}";
                    res += $" {d.x,10:f4}  {d.y,10:f4}\r\n";
                }
            return res;
        }
        #endregion

        #region 选择椭球
        private void cGCS2000ToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }
        #endregion
        
        private void 注意ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("椭球选择和中间数据代码(即高斯正反算作业需要填写的)没写" +
                "\r\n" +
                "\r\n" +
                "因为我不希望敌人捡到我的代码(屎)可以直接食用" +
                "\r\n" +
                "让我们一起说中文~一起码代码~" );
        }
    }
}