using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TDRv
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;//设置form1的开始位置为屏幕的中央
        }

        private void tsb_DevConnect_Click(object sender, EventArgs e)
        {
            DevConnectSet devConnectSet = new DevConnectSet();
            devConnectSet.ChangeValue += new DevConnectSet.ChangeTsbHandler(Change_Tsb_Index);
            devConnectSet.Show();
        }

        //根据连接设置传过来的值，控制是否可以使用
        public void Change_Tsb_Index(bool flag)
        {
            tsb_GetTestIndex.Enabled = flag;
            tsb_StartTest.Enabled = flag;
        }


        private void tsb_DevOptSet_Click(object sender, EventArgs e)
        {
            DevOptSet devOptSet = new DevOptSet();
            devOptSet.Show();
        }

        private void tsb_DevParamSet_Click(object sender, EventArgs e)
        {
            DevParamSet devParamSet = new DevParamSet();
            devParamSet.ChangeDgv += new DevParamSet.ChangeDgvHandler(Change_DataGridView);
            devParamSet.Show();
        }

        public void Change_DataGridView(DataGridView dt)
        {
            dataGridView1.Visible = true;
            dataGridView1.Rows.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int index = dataGridView1.Rows.Add();

                dataGridView1.Rows[index].Cells[1].Value = dt.Rows[i].Cells[1].Value;
                dataGridView1.Rows[index].Cells[2].Value = dt.Rows[i].Cells[2].Value;
                dataGridView1.Rows[index].Cells[3].Value = dt.Rows[i].Cells[3].Value;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            initChart();
        }

        private void initChart()
        {
            //chart1.Series.Clear();
            chart1.Series[0].LegendText = "TDR Curve";
            chart1.Series[1].LegendText = "limit";
            chart1.Series[2].LegendText = "limit";
            chart1.Series[3].LegendText = "100";
            chart1.Series[0].ChartType = SeriesChartType.Spline;
            chart1.Series[0].BorderWidth = 2;
            chart1.Series[1].BorderWidth = 2;
            chart1.Series[2].BorderWidth = 2;
            chart1.Series[3].BorderWidth = 2;

            //背景黑色
            chart1.BackColor = Color.Black;
            chart1.ChartAreas[0].BackColor = Color.Black;

            //XY标签改为白色
            chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White;
            chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;

            //X，Y轴白色
            chart1.ChartAreas[0].AxisX.LineColor = Color.White;
            chart1.ChartAreas[0].AxisY.LineColor = Color.White;

            //差分线条黄色
            chart1.Series[0].Color = Color.Yellow; //线条颜色

            //有效区域线红色
            chart1.Series[1].Color = Color.Red; //线条颜色
            chart1.Series[2].Color = Color.Red; //线条颜色

            //单端线条蓝色
            chart1.Series[3].Color = Color.Blue; //线条颜色

            //网格线颜色白色
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;
        }

        //开路定义
        private void tsb_GetTestIndex_Click(object sender, EventArgs e)
        {

        }
    }
}
