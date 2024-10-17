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
        public Form2()
        {
            InitializeComponent();
            MySqlite sqlite = new MySqlite();
            sqlite.load();
        }

        //刷新UI
        public void chushihua() { 
        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
