using DaZu_Laser_marking.Model;
using DaZu_Laser_marking.NET;
using DaZu_Laser_marking.SQLite;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Threading;
using System.Windows.Forms;

namespace DaZu_Laser_marking
{
    public partial class mainform : Form
    {
        MainFormStatus status;
        NetStatus netStatus;
        Thread freshThread;
        Thread netCheekThread;
        Peifang Litem;
        Peifang Ritem;
        dataSql data;

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







        public mainform()
        {
            InitializeComponent();

            mainform_Load();
            LocalNet();

            netCheekThread = new Thread(cheekConnection);
            netCheekThread.Start();


            freshThread = new Thread(fresh);
            freshThread.Start();

           
        }

        // 用来确保 UI 线程更新控件
        private void UpdateLabelBackColor(System.Windows.Forms.Label label, Color color)
        {
            if (label.InvokeRequired)
            {
                label.Invoke(new System.Action (() => label.BackColor = color));
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
            }
        }

        async void cheekConnection() {

            while (true)
            {
                Thread.Sleep(500);
                if (await MyNet.isConnettionToIp(IP_L)) { netStatus.IpL = 1; } else { netStatus.IpL = 0; }
                if (await MyNet.isConnettionToIp(IP_R)) { netStatus.IpR = 1; } else { netStatus.IpR = 0; }
                if (await MyNet.isConnettionToIp(IP_mes)) { netStatus.IpMes = 1; } else { netStatus.IpMes = 0; }
            }
           
        }

        private void mainform_Load()
        {
            //数据栏状态
            status = new MainFormStatus();

            netStatus = new NetStatus();

            PeiFangSql peiFangSql = new PeiFangSql();
            List<object> res1 = peiFangSql.getById(1);
            List<object> res2 = peiFangSql.getById(2);

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



        }


        private void LocalNet() {

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

                Program.Logger.Info("打标机L，  IP:"+IP_L+",端口："+Port_L);
                Program.Logger.Info("打标机R，  IP:" + IP_R + ",端口：" + Port_R);
                Program.Logger.Info("MES，  IP:" + IP_mes + ",端口：" + Port_mes);

            }
            catch (Exception ex)
            {
                Program.Logger.Info(ex.ToString());
            }

        }
    }
}
