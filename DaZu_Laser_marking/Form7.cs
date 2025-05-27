using DaZu_Laser_marking.Model;
using DaZu_Laser_marking.SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DaZu_Laser_marking
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            DaBiao equpment1 = new DaBiao(1,"打标机1");

            // Console.WriteLine("登录："+ equpment1.Login());

             string s1 = await equpment1.Login();
             System.Console.WriteLine(s1);

            string res = await equpment1.GetAllEqupments();
            res.Replace("#", "");
            getEqupments result = JsonSerializer.Deserialize<getEqupments>(res);

            richTextBox1.Text = result.Devices[0];
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            DaBiao d = new DaBiao(1, "打标机1");
            string T = textBox1.Text;

           string S2 =  await d.Login();

            System.Console.WriteLine(S2);

         

            System.Console.WriteLine(S);

            
        }
    }
}
