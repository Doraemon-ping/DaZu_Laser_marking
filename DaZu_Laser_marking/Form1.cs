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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 网络配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            MyTool.showForm(f3,this.panel2);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void 主页ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 f2 = new Form6();
            MyTool.showForm(f2,this.panel2);
        }

        private void 网络配置MESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            MyTool.showForm(f4,this.panel2);
        }
        
        private void 图号管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            MyTool.showForm(f5,this.panel2);

        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
