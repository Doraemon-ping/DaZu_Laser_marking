using DaZu_Laser_marking.SQLite;
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
        dataSql ds;
        DataTable dt;
        public Form1()
        {
            InitializeComponent();
            ds = new dataSql();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;

            dt = ds.getData();

            this.dataGridView1.DataSource = dt;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dt == null) { MessageBox.Show("请先查询！"); }
            else
            {
                MyExcel.get_Excel_fromDateGridView(this.dataGridView1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;

            string barcode = this.textBox1.Text;

            dt = ds.getByCode(barcode);

            this.dataGridView1.DataSource = dt;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        
    }
}
