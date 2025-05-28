using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DaZu_Laser_marking
{
    internal static class Program
    {
        public static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public static string logPath;


        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            // 简单的配置日志记录到一个文件
            var config = new NLog.Config.LoggingConfiguration();

            DateTime dateTime = DateTime.Now;

            string riqi = dateTime.Year.ToString() + "_" + dateTime.Month.ToString();

            // 定义日志文件路径和日志格式
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "log" + "\\" + riqi + "\\" + riqi + dateTime.Day.ToString() + "_logfile.txt" };
            logPath = logfile.FileName.ToString();
            Console.WriteLine(logPath);

            // 将日志级别设置为 Debug 及以上的所有日志信息写入文件
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);

            // 应用配置
            LogManager.Configuration = config;

            // 记录日志
            Logger.Info("Application started.");


            //时间限制
            DateTime expiryDate = new DateTime(2026, 12, 30); // 设置到期时间

            bool cheek =  Success.cheek();

            if (DateTime.Now > expiryDate)
            {
                MessageBox.Show("本程序已过期，无法使用。"+"\n"+"请联系邮箱:lisong.zhao@tuopu.com！！！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(0); // 退出程序
            }

            if (cheek)
            {
                MessageBox.Show("验证成功 ， 进入 ！");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form2());
            }
            else {
                MessageBox.Show("验证失败 ， 退出 ！");
                Environment.Exit(0); // 退出程序
            }



           
        }
    }
}
