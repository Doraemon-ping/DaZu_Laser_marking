
using DaZu_Laser_marking.Model;
using DaZu_Laser_marking.NET;
using DaZu_Laser_marking.SQLite;
using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DaZu_Laser_marking
{
    public partial class mainform : Form
    {
        //选择配方
        private readonly int Lint = 3;
        private readonly int Rint = 4;


        MainFormStatus status;
        NetStatus netStatus;
        Thread freshThread;
        Thread netCheekThread;
        Peifang Litem;
        Peifang Ritem;
        dataSql data;
        FreshStatus FreshStatus;//更新左右码状态

        //打标机NET
        string IP_L;
        string Port_L;
        string IP_R;
        string Port_R;
        string IP_mes;
        string Port_mes;
        string URL;
        private readonly String APIadd = "/api/AddEquipInfo";

        bool _isHandlingTextChanged = false;

        //dabiao
        DaBiao LD;//左打标机
        DaBiao RD;//右打标机
        List<string> Lequpment;
        List<string> Requpment;
        string FineName_L;
        string FineName_R;
        int pox1;
        int pox2;
        int pox3;
        int Lid;
        int Rid;

        //MesMod
        Model.AddEquipInfo mesMod;


        public mainform()
        {

            InitializeComponent();

            mainform_LoadAsync();
            LocalNet();

            netCheekThread = new Thread(cheekConnection);
            netCheekThread.Start();


            freshThread = new Thread(fresh);
            freshThread.Start();

            //获取报工实体
            getAdMod();


        }

        public void getAdMod()
        {
            mesMod = new Model.AddEquipInfo();
            mesMod.FactoryCode = "8111";
            mesMod.ProductLineNo = "KHM01";
            mesMod.EquipNo = "8111khm02";
            mesMod.stationCodeNo = "KHMGW01";
            mesMod.Operator = "tp2022062112";
            mesMod.Result = "OK";
        }

        // 用来确保 UI 线程更新控件
        private void UpdateLabelBackColor(System.Windows.Forms.Label label, Color color)
        {
            if (label.InvokeRequired)
            {
                label.Invoke(new System.Action(() => label.ForeColor = color));
            }
            else
            {
                label.BackColor = color;
            }
        }

        //刷新UI
        void fresh()
        {

            while (true)
            {
                Thread.Sleep(500); // 控制刷新频率，避免过高的 CPU 占用
                // 铸造码
                switch (status.ZhuzaomaLab)
                {
                    case 0:
                        UpdateLabelBackColor(zhuzaoma_lab, Color.Red);
                        break;
                    case 1:
                        UpdateLabelBackColor(zhuzaoma_lab, Color.Yellow);
                        break;
                    case 2:
                        UpdateLabelBackColor(zhuzaoma_lab, Color.Green);
                        break;
                }

                // 客户码
                switch (status.KehumaLab)
                {
                    case 0:
                        UpdateLabelBackColor(kehuma_lab, Color.Red);
                        break;
                    case 1:
                        UpdateLabelBackColor(kehuma_lab, Color.Yellow);
                        break;
                    case 2:
                        UpdateLabelBackColor(kehuma_lab, Color.Green);
                        break;
                }

                // 打标机Login
                switch (status.EqupmentLoginLab)
                {
                    case 0:
                        UpdateLabelBackColor(dabiaoLogin_lab, Color.Red);
                        break;
                    case 1:
                        UpdateLabelBackColor(dabiaoLogin_lab, Color.Yellow);
                        break;
                    case 2:
                        UpdateLabelBackColor(dabiaoLogin_lab, Color.Green);
                        break;
                }

                // 打标机更新条码
                switch (status.QrcodeUpdate1)
                {
                    case 0:
                        UpdateLabelBackColor(codeUpdate_lab, Color.Red);
                        break;
                    case 1:
                        UpdateLabelBackColor(codeUpdate_lab, Color.Yellow);
                        break;
                    case 2:
                        UpdateLabelBackColor(codeUpdate_lab, Color.Green);
                        break;
                }

                // 本地保存
                switch (status.Save1)
                {
                    case 0:
                        UpdateLabelBackColor(save_lab, Color.Red);
                        break;
                    case 1:
                        UpdateLabelBackColor(save_lab, Color.Yellow);
                        break;
                    case 2:
                        UpdateLabelBackColor(save_lab, Color.Green);
                        break;
                }

                // 本地保存
                switch (status.MesSave)
                {
                    case 0:
                        UpdateLabelBackColor(mesSave_lab, Color.Red);
                        break;
                    case 1:
                        UpdateLabelBackColor(mesSave_lab, Color.Yellow);
                        break;
                    case 2:
                        UpdateLabelBackColor(mesSave_lab, Color.Green);
                        break;
                }

                // 本地保存
                switch (status.Mark)
                {
                    case 0:
                        UpdateLabelBackColor(mark_lab, Color.Red);
                        break;
                    case 1:
                        UpdateLabelBackColor(mark_lab, Color.Yellow);
                        break;
                    case 2:
                        UpdateLabelBackColor(mark_lab, Color.Green);
                        break;
                }

                //网络连接
                switch (netStatus.IpL)
                {
                    case 0:
                        UpdateLabelBackColor(equpLConnect_lab, Color.Red);
                        break;
                    case 1:
                        UpdateLabelBackColor(equpLConnect_lab, Color.Green);
                        break;
                }
                switch (netStatus.IpR)
                {
                    case 0:
                        UpdateLabelBackColor(equpRConnect_lab, Color.Red);
                        break;
                    case 1:
                        UpdateLabelBackColor(equpRConnect_lab, Color.Green);
                        break;
                }
                switch (netStatus.IpMes)
                {
                    case 0:
                        UpdateLabelBackColor(mesConnect_lab, Color.Red);
                        break;
                    case 1:
                        UpdateLabelBackColor(mesConnect_lab, Color.Green);
                        break;
                }

                if (richTextBox1.InvokeRequired)
                {
                    richTextBox1.Invoke(new System.Action(() => {
                        richTextBox1.Text = status.Res;
                    }
                    ));
                }
                else
                {
                    richTextBox1.Text = status.Res;
                }


            }
        }

        //开始标刻
        

        async void cheekConnection()
        {

            while (true)
            {
                Thread.Sleep(500);
                if (await MyNet.isConnettionToIp(IP_L)) { netStatus.IpL = 1; } else { netStatus.IpL = 0; }
                if (await MyNet.isConnettionToIp(IP_R)) { netStatus.IpR = 1; } else { netStatus.IpR = 0; }
                if (await MyNet.isConnettionToIp(IP_mes)) { netStatus.IpMes = 1; } else { netStatus.IpMes = 0; }
            }

        }

        private async void mainform_LoadAsync()
        {
            //数据栏状态
            status = new MainFormStatus();

            netStatus = new NetStatus();

            PeiFangSql peiFangSql = new PeiFangSql("L");
            List<object> res1 = peiFangSql.getById();
            PeiFangSql peiFangSql2 = new PeiFangSql("R");
            List<object> res2 = peiFangSql2.getById();

            //左右件配方
            Litem = new Peifang(res1[1].ToString(),
                                res1[2].ToString(),
                                int.Parse(res1[3].ToString()),
                                int.Parse(res1[4].ToString()),
                                res1[5].ToString(),
                                int.Parse(res1[6].ToString()),
                                res1[7].ToString());

            Ritem = new Peifang(res2[1].ToString(),
                               res2[2].ToString(),
                               int.Parse(res2[3].ToString()),
                               int.Parse(res2[4].ToString()),
                               res2[5].ToString(),
                               int.Parse(res2[6].ToString()),
                               res2[7].ToString());

            //初始化数据增删对象
            data = new dataSql();


            textBox1.Multiline = true;
            textBox1.AcceptsReturn = true; // 允许按Enter键输入换行

            this.KeyPreview = true; // 允许窗体接收按键事件
            this.KeyDown += Form_KeyDown;

            try
            {
                FineName_L = ConfigurationManager.ConnectionStrings["W04L"].ConnectionString;
                FineName_R = ConfigurationManager.ConnectionStrings["W04R"].ConnectionString;
                //pox1
                pox1 = int.Parse(ConfigurationManager.ConnectionStrings["pox1"].ConnectionString);
                pox2 = int.Parse(ConfigurationManager.ConnectionStrings["pox2"].ConnectionString);
                pox3 = int.Parse(ConfigurationManager.ConnectionStrings["pox3"].ConnectionString);

                Lid = int.Parse(ConfigurationManager.ConnectionStrings["PFLID"].ConnectionString);
                Rid = int.Parse(ConfigurationManager.ConnectionStrings["PFRID"].ConnectionString);

                Program.Logger.Info("左件文件：" + FineName_L);
                Program.Logger.Info("右件文件：" + FineName_R);
                Program.Logger.Info("左配方id：" + Lid);

            }
            catch (Exception ex)
            {
                Program.Logger.Info(ex.Message);
            }
            
           

            try
            {
                LD = new DaBiao(1, "打标机1");
                await LD.Login();
                string resL = await LD.GetAllEqupments();
                resL = resL.Replace("#", "");
                var jsonObj1 = JObject.Parse(resL);
                if (jsonObj1["status"].ToString() == "success")
                {
                    Program.Logger.Info("左件获取成功!");
                }
                else {
                    Program.Logger.Info("左件获取失败!");
                }

                getEqupments result1 = System.Text.Json.JsonSerializer.Deserialize<getEqupments>(resL);
                Lequpment = result1.Devices;
                Program.Logger.Info("l_eqp:" + Lequpment.ToString());

            }
            catch (Exception ex)
            {
                Program.Logger.Info(ex.Message);
            }

            try
            {
                RD = new DaBiao(2, "打标机2");
                await RD.Login();
                string resR = await RD.GetAllEqupments();
                resR = resR.Replace("#", "");
                var jsonObj2 = JObject.Parse(resR);
                if (jsonObj2["status"].ToString() == "success")
                {
                    Program.Logger.Info("右件获取成功!");
                }
                else
                {
                    Program.Logger.Info("右件获取失败!");
                }

                getEqupments result2 = System.Text.Json.JsonSerializer.Deserialize<getEqupments>(resR);
                Requpment = result2.Devices;
                Program.Logger.Info("r_eqp:" + Requpment.ToString());

            }
            catch (Exception ex)
            {
                Program.Logger.Info(ex.Message);
            }

        }
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    ShowHelp(); // 执行你的函数
                    e.Handled = true; // 标记为已处理
                    break;
               
            }
        }

        private async void ShowHelp()
        {

            //开始执行写入打标
            String oldCode = textBox2.Text;
            string newCode = textBox3.Text;

            mesMod.barCode = oldCode;
            mesMod.mainPartCode = newCode;
            mesMod.childPartCode = oldCode;
            mesMod.startTime = DateTime.Now;
            mesMod.endTime = DateTime.Now;

            if (string.IsNullOrEmpty(mesMod.barCode))
            {
                MessageBox.Show("请先扫二维码！");
                return;
            
            }
           

            if (status.Lr == 1)
            {
                MessageBox.Show("左件生产");
                MessageBox.Show(mesMod.mainPartCode);
                MessageBox.Show(Lid.ToString());


                switch (Lid)
                {

                    case 1:
                        break;
                    case 3:
                        break;
                    case 5:
                        break;
                    case 7:
                        await MarkAndUpload.MarkW04Async(mesMod,LD,data,URL,status,pox1,pox2,FineName_L,Lequpment);
                        break;
                }

                

            }


            if (status.Lr == 2)
            {
                MessageBox.Show("右件生产");

                switch (Rid)
                {

                    case 2:
                        break;
                    case 4:
                        break;
                    case 6:
                        break;
                    case 8:
                        await MarkAndUpload.MarkW04Async(mesMod, RD, data, URL, status, pox1, pox2, FineName_R, Requpment);
                        break;
                }

            }


        }


        private void LocalNet()
        {

            try
            {
                MySqlite mySqlite = new MySqlite();
                List<object> res1 = new List<object>();
                res1 = mySqlite.getByIdName(1, "打标机1");
                IP_L = res1[0].ToString();
                Port_L = res1[1].ToString();
                List<object> res2 = new List<object>();
                res2 = mySqlite.getByIdName(2, "打标机2");
                IP_R = res2[0].ToString();
                Port_R = res2[1].ToString();
                List<object> res3 = new List<object>();
                res3 = mySqlite.getByIdName(3, "数据库");
                List<object> res4 = new List<object>();
                res4 = mySqlite.getByIdName(4, "WEB");
                IP_mes = res4[0].ToString();
                Port_mes = res4[1].ToString();
                URL = "http://" + IP_mes + ":" + Port_mes + APIadd;

                Program.Logger.Info("打标机L，  IP:" + IP_L + ",端口：" + Port_L);
                Program.Logger.Info("打标机R，  IP:" + IP_R + ",端口：" + Port_R);
                Program.Logger.Info("MES，  IP:" + IP_mes + ",端口：" + Port_mes);

            }
            catch (Exception ex)
            {
                Program.Logger.Info(ex.ToString());
            }

        }

        //输入铸造码，开始工作
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
                    FreshStatus = new FreshStatus();
                    mesMod.clean();
                    if (FreshStatus.Lcode1 || FreshStatus.Rcode1)
                    {
                        status.ZhuzaomaLab = 2;
                    }
                    else
                    {
                        status.clean();

                    }

                    if (FreshStatus.LNewCode1 || FreshStatus.RNewCode1)
                    {
                        status.KehumaLab = 2;
                    }
                    else
                    {
                        status.clean();
                    }


                    string code = textBox1.Text.Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Replace(" ",""); //删除换行

                    code = code.ToUpper(); // 示例：将文本转换为大写
                                           // richTextBox1.SelectionStart = richTextBox1.Text.Length; // 保持光标位置
                                           //判断二维码左右件

                    int lr = MyTool.isLorR(code, Litem, Ritem);

                    if (lr == 1)
                    {
                        FreshStatus.Lcode1 = true;
                        status.ZhuzaomaLab = 2;
                        textBox1.Text = string.Empty;
                        textBox2.Text = code;

                        status.IsRecode = !MyTool.isNotCm(code);

                        if (!status.IsRecode)
                        {
                            //不重码
                            List<string> list = UpdateCode("L");
                            textBox3.Text = list[0];
                            status.KehumaLab = 2;
                            status.Lr = 1;
                        }
                        else {
                            //重码
                            textBox3.Text =  data.getKHM(code);
                            status.KehumaLab = 2;
                            status.Lr = 1;
                            status.Res = "铸造重码" + "\n" + status.Res;

                        }




                    }
                    if (lr == 2)
                    {
                        FreshStatus.Rcode1 = true;
                        status.ZhuzaomaLab = 2;
                        textBox1.Text = string.Empty;
                        textBox2.Text = code;

                        status.IsRecode = !MyTool.isNotCm(code);

                        if (!status.IsRecode)
                        {
                            //不重码
                            List<string> list = UpdateCode("R");
                            textBox3.Text = list[0];
                            status.KehumaLab = 2;
                            status.Lr = 2;
                        }
                        else
                        {
                            //重码
                            textBox3.Text = data.getKHM(code);
                            status.KehumaLab = 2;
                            status.Lr = 2;
                            status.Res = "铸造重码" + "\n" + status.Res;
                        }
                    }
                    if (lr == 0)
                    {
                        MessageBox.Show("输入二维码不正确！");
                        textBox1.Text = string.Empty;
                        textBox2.Text = string.Empty;
                        //LEOFKLHM1250609181119C2#
                        textBox3.Text = string.Empty;
                    }
                    // ScanRead = richTextBox1.Text.IsNotNullOrEmpty();
                }
            }
            finally
            {
                // 重置标志位，允许再次处理事件
                _isHandlingTextChanged = false;
            }
        }

        //实现此方法用户根据规则获取客户码以及替换字符串
        public List<string> UpdateCode(string s)
        {
            string res = s.ToUpper();
          
            List<string> result = new List<string>();


            if (res == "L")
            {
                //生成左件客户码
                switch (Lid) {

                    case 1:
                        //LEO左前
                        break;
                    case 3:
                        //E3S6左前
                        result.Add(Code.GetNewCode.GetNewCodeE3S6("FKLH",Litem));
                        break;
                    case 5:
                        //E3S6左后
                        break;
                    case 7:
                        //W04L
                        result.Add(Code.GetNewCode.GetNewCodeW04("L",Litem));
                        break;
                }
            }

            if (res == "R")
            {
                //生成右件客户码
                switch (Rid)
                {

                    case 2:
                        break;
                    case 4:
                        result.Add(Code.GetNewCode.GetNewCodeE3S6("FKRH", Ritem));
                        break;
                    case 6:
                        break;
                    case 8:
                        result.Add(Code.GetNewCode.GetNewCodeW04("R", Ritem));
                        break;
                }

            }
           
            //result[0]:客户码
            //result[1],[2]替换字符

            return result;
        }
    }
}
    
