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
    public partial class PeifangForm : Form
    {
        PeiFangSql PL;
        PeiFangSql PR;

        public PeifangForm()
        {
            InitializeComponent();
            PL = new PeiFangSql("L");
            PR = new PeiFangSql("R");

            load();
          

        }

        void load() {

            List<object> res1 = PL.getById();
            List<object> res2 = PR.getById();


            try
            {
                textBox1.Text = res1[1].ToString();
                textBox2.Text = res1[2].ToString();
                textBox3.Text = res1[3].ToString();
                textBox4.Text = res1[5].ToString();
                textBox5.Text = res1[4].ToString();
                textBox7.Text = res1[7].ToString();
                textBox6.Text = res1[6].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                textBox14.Text = res2[1].ToString();
                textBox13.Text = res2[2].ToString();
                textBox12.Text = res2[3].ToString();
                textBox11.Text = res2[5].ToString();
                textBox10.Text = res2[4].ToString();
                textBox9.Text = res2[7].ToString();
                textBox8.Text = res2[6].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string name_L = textBox1.Text;
                string TH_l = textBox2.Text;
                int NUM_L = int.Parse(textBox3.Text);
                string STR_L = textBox4.Text;
                int STR_N_L = int.Parse(textBox5.Text);
                string END_L = textBox7.Text;
                int END_N_L = int.Parse(textBox6.Text);
                PL.updateByID(name_L, TH_l, NUM_L, STR_N_L, STR_L, END_N_L, END_L);
                load();

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
                string name_R = textBox14.Text;
                string TH_R = textBox13.Text;
                int NUM_R = int.Parse(textBox12.Text);
                string STR_R = textBox11.Text;
                int STR_N_R = int.Parse(textBox10.Text);
                string END_R = textBox9.Text;
                int END_N_R = int.Parse(textBox8.Text);
                PR.updateByID(name_R, TH_R, NUM_R, STR_N_R, STR_R, END_N_R, END_R);
                load();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
