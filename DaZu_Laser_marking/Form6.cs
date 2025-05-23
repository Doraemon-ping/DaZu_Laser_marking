using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
//using NLog;
using System.Diagnostics;
using DaZu_Laser_marking.Model;
using DaZu_Laser_marking.SQLite;
using static System.Data.Entity.Infrastructure.Design.Executor;
using System.Data.SqlClient;
//using Aqua.EnumerableExtensions;

namespace DaZu_Laser_marking
{
    public partial class Form6 : Form
    {
        bool _isHandlingTextChanged = false;

        int b_bt1 = 0;//button1状态
        int b_bk = 0;//标刻状态9
        int b_bt2 = 0;//客户吗状态
        Thread fresh;//控件刷新线程
        Peifang QZcode;//前转配方
        Model.AddEquipInfo mesMod;//报工实体类
        dataSql dsq;
        bool isNotCm = false;//是否不重码
        bool isOK = false;//是否可标刻
        int ms = 1; //模式，1正常 ， 2 返工
        

        public Form6()
        {
            InitializeComponent();
            chushihau();
          

        }

        public void chushihau() {

           
            getPF();

            //控件更新线程
            fresh = new Thread(flash);
            fresh.Start();

            


            //初始化报工实体实体
            getAdMod();

            dsq = new dataSql();





        }


        public void getPF() {

            PeiFangSql peiFangSql = new PeiFangSql();
            List<object> QZ = peiFangSql.getById(1);

            //右件配方实体
            QZcode = new Peifang(QZ[1].ToString(), QZ[2].ToString(), int.Parse(QZ[3].ToString()));

            // System.Console.WriteLine("test:"+LMOD.TH_R);

        }

        public void getAdMod() { 
            mesMod = new Model.AddEquipInfo();
            mesMod.FactoryCode = "8111";
            mesMod.ProductLineNo = "KHM01";
            mesMod.EquipNo = "8111khm02";
            mesMod.stationCodeNo = "KHMGW01";
            mesMod.Operator = "tp2022062112";
            mesMod.Result = "OK";
        
        }

        public void update() {
            if (b_bt1 != 0 && b_bt2 != 0)
            {
                isOK = true;
            }
            else
            {
                isOK = false;
            }


        }


       



        public void flash() {
            
            //刷新线程
            //Console.WriteLine("fresh线程开始");

            while (true) {

                if (b_bt1 == 0 ) {

                    if (button1.InvokeRequired)
                    {
                        button1.Invoke(new Action(() => button1.BackColor = Color.Red));
                    }
                    else
                    {
                        button1.BackColor = Color.Red;
                    }
                }

                if (b_bt1 == 1 || b_bt1 == 2) {
                    if (button1.InvokeRequired)
                    {
                        button1.Invoke(new Action(() => button1.BackColor = Color.Green));
                    }
                    else
                    {
                        button1.BackColor = Color.Green;
                    }
                }

                if (b_bt2 == 0)
                {
                    if (button2.InvokeRequired)
                    {
                        button2.Invoke(new Action(() => button2.BackColor = Color.Red));
                    }
                    else
                    {
                        button2.BackColor = Color.Green;
                    }
                }

                if (b_bt2 == 1) {
                    if (button2.InvokeRequired)
                    {
                        button2.Invoke(new Action(() => button2.BackColor = Color.Green));
                    }
                    else
                    {
                        button2.BackColor = Color.Green;
                    }
                }

                if (isOK == false) {
                    if (button3.InvokeRequired)
                    {
                        button3.Invoke(new Action(() => button3.BackColor = Color.Red));
                    }
                    else
                    {
                        button2.BackColor = Color.Red;
                    }
                }

                if (isOK == true) {

                    if (button3.InvokeRequired)
                    {
                        button3.Invoke(new Action(() => button3.BackColor = Color.Green));
                    }
                    else
                    {
                        button2.BackColor = Color.Green;
                    }
                }

                if (ms == 1)
                {
                    if (button4.InvokeRequired)
                    {
                        button4.Invoke(new Action(() => button4.BackColor = Color.Green));
                        button4.Invoke(new Action(() => button4.Text = "正常模式"));

                    }
                    else
                    {
                        button4.BackColor = Color.Green;
                        button4.Text = "正常模式";
                    }
                }

                if (ms == 2)
                {
                    if (button4.InvokeRequired)
                    {
                        button4.Invoke(new Action(() => button4.BackColor = Color.Yellow));
                        button4.Invoke(new Action(() => button4.Text = "返工模式"));

                    }
                    else
                    {
                        button4.BackColor = Color.Yellow;
                        button4.Text = "返工模式";
                    }
                }


                Thread.Sleep(1000);


            }

       

            

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        public void clean() {
            //更新
            richTextBox1.Text = string.Empty;
            richTextBox6.Text = string.Empty;
            richTextBox2.Text = string.Empty;
            richTextBox5.Text = string.Empty;
            b_bt1 = 0;
            b_bt2 = 0;
            richTextBox3.Text = "";
            richTextBox4.Text = "";
            isNotCm = false;
            isOK = false;
            richTextBox2.BackColor = Color.White;
            richTextBox5.BackColor = Color.White;

            mesMod.barCode = null;
            mesMod.mainPartCode = null;
            mesMod.childPartCode = null;
            mesMod.startTime = DateTime.Now;
            mesMod.endTime = DateTime.Now;


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // 检查是否已经在处理 TextChanged 事件
            if (_isHandlingTextChanged)
                return;

            try
            {
                Console.WriteLine("事件触发！");
                // 设置标志位，表示正在处理事件
                _isHandlingTextChanged = true;

                // 在这里执行你的逻辑，例如修改 TextBox 的内容
                // 注意：此时更改 TextBox.Text 不会再次触发 TextChanged 事件
                //
                if (textBox1.Text == "\n" || textBox1.Text == "\r\n" || textBox1.Text == "\r")
                {
                    textBox1.Text = string.Empty;
                    //只按下空格，将输入清空
                    //
                }

                //修改值
                bool isinput = !string.IsNullOrEmpty(textBox1.Text);//不为空，用户输入
                                                                //;//用户是否完成输入
                isinput = isinput && !(textBox1.Text == "\n" || textBox1.Text == "\r\n" || textBox1.Text == "\r");

                bool inputExit = false;//默认未完成

                bool endN = false; //以回车结尾
                bool includeN = false; //包含回车

                if (textBox1.Text.EndsWith("\r\n") || textBox1.Text.EndsWith("\n") || textBox1.Text.EndsWith("\r"))
                {
                    Console.WriteLine("字符串以换行符结尾");
                    endN = true;//用户完成输入 //换行了，输入完成
                }

                if (textBox1.Text.Contains("\r\n") || textBox1.Text.Contains("\n") || textBox1.Text.Contains("\r"))
                {
                    includeN = true;
                    Console.WriteLine("字符串中存在换行符");
                }
                inputExit = (endN || includeN);

                if (isinput && inputExit)
                {
                    //下一次扫码完成，初始化
                    clean();
                   
                   string code = textBox1.Text.Replace("\r\n", "").Replace("\n", "").Replace("\r", ""); //删除换行

                     code = code.ToUpper(); // 示例：将文本转换为大写
                                            // richTextBox1.SelectionStart = richTextBox1.Text.Length; // 保持光标位置
                   

                    int lr = MyTool.isLorR(code);
                    b_bt1 = lr;
                    if (lr == 1) { richTextBox1.Text = code;
                        richTextBox2.Text = MyTool.createKHM_L(QZcode);

                        b_bt2 = 1;
                        isNotCm = MyTool.isNotCm(code);

                        if (!isNotCm) { 
                            
                            richTextBox3.Text = "铸造重码";
                            richTextBox2.Text = dsq.getKHM(code);
                            richTextBox2.BackColor = Color.Yellow;
                        }
                    }

                    if (lr == 2) { richTextBox6.Text = code; 
                        richTextBox5.Text = MyTool.createKHM_R(QZcode);

                        b_bt2 = 1;
                        isNotCm = MyTool.isNotCm(code);
                        if (!isNotCm) { 
                           
                            richTextBox4.Text = "铸造重码";
                            richTextBox5.Text = dsq.getKHM(code);
                            richTextBox5.BackColor = Color.Yellow;


                        }
                    }
                    // ScanRead = richTextBox1.Text.IsNotNullOrEmpty();
                    textBox1.Text = string.Empty;
                    update();
                }

                ///
            }
            finally
            {
                // 重置标志位，允许再次处理事件
                _isHandlingTextChanged = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string okBarcode="";
            string okHbarcode="";
            if (isOK == true)
            {
                if (b_bt1 == 1)
                {
                    okBarcode = richTextBox1.Text;
                    okHbarcode = richTextBox2.Text;


                }
                if (b_bt1 == 2)
                {
                    okBarcode = richTextBox6.Text;
                    okHbarcode = richTextBox5.Text;
                    

                }

            }
            else {
                MessageBox.Show("二维码错误！");
                return;
            
            }

            //二维码正确
            if (isOK == true && ms == 1 ) {

                //不重码
                if (isNotCm)
                {
                   // MessageBox.Show("正常模式正常生产！");
                    dsq.insert(okBarcode, okHbarcode, DateTime.Now);
                    //上传

                    mesMod.barCode= okBarcode;
                    mesMod.mainPartCode = okBarcode;
                    mesMod.childPartCode = okHbarcode;
                    mesMod.startTime = DateTime.Now;
                    mesMod.endTime = DateTime.Now;

                    string json = System.Text.Json.JsonSerializer.Serialize(mesMod);
                    MessageBox.Show(json);




                    //打标

                    clean();
                    return;
                }
                else {
                    MessageBox.Show("请选择返工模式！");
                    return;


                }
            }

            //返工模式
            if (isOK == true &&  ms == 2) {

                if (!isNotCm) {
                    //重码
                    MessageBox.Show("返工模式正常生产！");

                    //上传


                    //打标
                    clean();
                    return;

                }
                else
                {
                    //不重码
                    MessageBox.Show("请选择正常模式！");
                    return;


                }


            }



        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (ms == 1) { 
                ms = 2;
                return;
            
            }
                
            if (ms == 2) { ms = 1;

                return;
               
            }

        }
    }
}
