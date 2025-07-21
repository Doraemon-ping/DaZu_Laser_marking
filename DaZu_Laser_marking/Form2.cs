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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DaZu_Laser_marking
{
    public partial class Form2 : Form
    {
        Form3 form3;
        Form4 form4;
        Form7 form7;
        Form1 form1;
        mainform mainform;
        PeifangForm peifangForm;
        public Form2()
        {
            InitializeComponent();
        }

        private void 主页ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mainform == null || mainform.IsDisposed)
                mainform = new mainform();
            MyTool.showForm(mainform, this.panel1);
        }

        private void 打标机配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (form3 == null || form3.IsDisposed)
                form3 = new Form3();
            MyTool.showForm(form3, this.panel1);
        }

        private void mes配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (form4 == null || form4.IsDisposed)
                form4 = new Form4();
            MyTool.showForm(form4, this.panel1);
        }

        private void 测试页ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (form7 == null || form7.IsDisposed)
                form7 = new Form7();
            MyTool.showForm(form7, this.panel1);
        }

        private void 数据导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (form1 == null || form1.IsDisposed)
                form1 = new Form1();
            MyTool.showForm(form1, this.panel1);
        }

        private void 日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DateTime dateTime = DateTime.Now;
            string riqi = dateTime.Year.ToString() + "_" + dateTime.Month.ToString();
            // 定义日志文件路径和日志格式
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "log" + "\\" + riqi + "\\" + riqi + dateTime.Day.ToString() + "_logfile.txt" };
            string logPath = logfile.FileName.ToString();

           MyTool.OpenFolder(logPath);
        }

        private void 配方管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (peifangForm == null || peifangForm.IsDisposed)
                peifangForm = new PeifangForm();
            MyTool.showForm(peifangForm, this.panel1);

        }
    }
}
