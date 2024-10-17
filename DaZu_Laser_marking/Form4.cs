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
    public partial class Form4 : Form
    {
        MySqlite sql = new MySqlite();

        public Form4()
        {
            InitializeComponent();
            chushihua();
        }

        public void chushihua()
        {
            List<object> res1 = new List<object>();
            res1 = sql.getByIdName(3, "数据库");
            this.textBox1.Text = res1[0].ToString();
            this.textBox2.Text = res1[1].ToString();

            List<object> res2 = new List<object>();
            res2 = sql.getByIdName(4, "WEB");
            this.textBox3.Text = res2[0].ToString();
            this.textBox4.Text = res2[1].ToString();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string ip = this.textBox1.Text;
                string port = this.textBox2.Text;
                sql.update_ip(ip, port, 3, "数据库");
                chushihua();
                MessageBox.Show("保存成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string ip = this.textBox3.Text;
                string port = this.textBox4.Text;
                sql.update_ip(ip, port, 4, "WEB");
                chushihua();
                MessageBox.Show("保存成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
