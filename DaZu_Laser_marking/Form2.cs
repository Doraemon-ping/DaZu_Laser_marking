using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DaZu_Laser_marking
{
    public partial class Form2 : Form
    {
        Form6 form6;
        Form3 form3;
        Form4 form4;
        Form7 form7;
        Form1 form1;
        mainform mainform;
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
            if (form4 == null || form6.IsDisposed)
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
            if (form1 == null || form6.IsDisposed)
                form1 = new Form1();
            MyTool.showForm(form1, this.panel1);
        }
    }
}
