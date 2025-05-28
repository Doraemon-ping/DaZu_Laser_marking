using DaZu_Laser_marking.Model;
using DaZu_Laser_marking.NET;
using DaZu_Laser_marking.SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Portable.Licensing;
using Portable.Licensing.Security.Cryptography;
using System.IO;
using License = Portable.Licensing.License;


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

            DaBiao daBiao = new DaBiao(1, "打标机1");
            await daBiao.Login();


        }

        private async void button2_Click(object sender, EventArgs e)
        {
            AddEquipInfo mesMod = new AddEquipInfo();
            mesMod = new Model.AddEquipInfo();
            mesMod.FactoryCode = "8111";
            mesMod.ProductLineNo = "KHM01";
            mesMod.EquipNo = "8111khm02";
            mesMod.stationCodeNo = "KHMGW01";
            mesMod.Operator = "tp2022062112";
            mesMod.Result = "OK";
            mesMod.barCode = "111";
            mesMod.mainPartCode = "111";

            string URL = "http://10.25.206.7:8861/api/AddEquipInfo";
            string JSON = System.Text.Json.JsonSerializer.Serialize(mesMod);
            var response = await MyNet.myPost(URL,JSON);


            richTextBox1.Text = response;



        }
    }
}
