﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DaZu_Laser_marking.Model;
using Newtonsoft.Json;
using NLog;
using static System.Net.Mime.MediaTypeNames;

namespace DaZu_Laser_marking
{
    //打标协议的实现
    internal class DaBiao
    {
        private string IP;
        private string PORT;
        TcpClient tcpClient;
        NetworkStream stream;
        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);


        public string IP1 { get => IP; set => IP = value; }
        public string PORT1 { get => PORT; set => PORT = value; }

        private async Task SendMessageAsync(string message)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                await stream.WriteAsync(buffer, 0, buffer.Length);
            }
            catch(Exception ex)
            { 
                Program.Logger.Info(ex.Message);
            }
        }

        private async Task<string> ReceiveMessageAsync()
        {
            try
            {
                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                return Encoding.UTF8.GetString(buffer, 0, bytesRead);

            }
            catch (Exception ex)
            {
                Program.Logger.Info(ex.Message);
                return null;
            }
           
        }


        public DaBiao(int id, string name)
        {

            try
            {
                MySqlite msl = new MySqlite();
                List<object> result = msl.getByIdName(id, name);
                IP1 = result[0].ToString();
                PORT1 = result[1].ToString();
                Console.WriteLine(IP + ":" + PORT);
                tcpClient = new TcpClient(IP1, int.Parse(PORT1));
                stream = tcpClient.GetStream();

            }
            catch (Exception ex) {
                Program.Logger.Info(ex.Message);
            
            }

          

        }
        //登录
        public async Task<string>  Login()
        {
            // 保证发送+接收的原子性
            await semaphore.WaitAsync();
            try
            {
                var requestData = new
                {
                    F = "Login_C2S",
                    password = "123456"
                };
                var requestJson = JsonConvert.SerializeObject(requestData) + "#";

                Program.Logger.Info("登录请求：" + requestJson);

                await SendMessageAsync(requestJson);
                
                string result = await ReceiveMessageAsync();
                Program.Logger.Info("登录反馈：" + result);
                return result;
            }
            finally
            {
                semaphore.Release();
            }

        }

        //获取所有设备
        public async Task<string> GetAllEqupments()
        {
            await semaphore.WaitAsync();
            try
            {
                var requestData = new
                {
                    F = "GetAllDevices_C2S"
                };
                var requestJson = JsonConvert.SerializeObject(requestData) + "#";
                Program.Logger.Info("获取设备："+requestJson);
                await SendMessageAsync(requestJson);
                string result = await ReceiveMessageAsync();
                Program.Logger.Info("反馈：" + result);
                return result;
            }
            finally
            {
                semaphore.Release();
            }

        }

        //获取所有文档
        public async Task<string> GetAllWords()
        {
            // 构建请求
           
            await semaphore.WaitAsync();
            try
            {
                var requestData = new
                {
                    F = "GetAllDocs_C2S"
                };
                var requestJson = JsonConvert.SerializeObject(requestData) + "#";

                await SendMessageAsync(requestJson);
                return await ReceiveMessageAsync();
            }
            finally
            {
                semaphore.Release();
            }
        }

        //打开文件
        public async Task<string> OpenFileDialog(string path)
        {
            // 保证发送+接收的原子性
            await semaphore.WaitAsync();
            try
            {
                // path = MyTool.get_filePath();
                var requestData = new
                {
                    F = "OpenDocPath_C2S",
                    docPath = path
                };
                var requestJson = JsonConvert.SerializeObject(requestData) + "#";
                await SendMessageAsync(requestJson);
                return await ReceiveMessageAsync();
            }
            finally
            {
                semaphore.Release();
            }

        }


        //开始标刻
        public async Task<string> StartMarking(List<string> EQ)
        {
            // 保证发送+接收的原子性
            await semaphore.WaitAsync();
            try
            {
                // path = MyTool.get_filePath();
                var requestData = new
                {
                    F = "DevsMark_C2S",
                    devs = EQ.ToArray()
                };
                var requestJson = JsonConvert.SerializeObject(requestData) + "#";
                await SendMessageAsync(requestJson);
                Program.Logger.Info("开始打标：" + requestJson);
                return await ReceiveMessageAsync();
            }
            finally
            {
                semaphore.Release();
            }
        }


        public async Task<string> StartMarkingByWord(string words, List<string> equpment)
        {
            // 保证发送+接收的原子性
            await semaphore.WaitAsync();
            try
            {
                // path = MyTool.get_filePath();
                var requestData = new
                {
                    F = "ME",
                    DC = words,
                    DS = equpment
                };
                var requestJson = JsonConvert.SerializeObject(requestData) + "#";
                await SendMessageAsync(requestJson);
                return await ReceiveMessageAsync();
            }
            finally
            {
                semaphore.Release();
            }
        }

        public async Task<string> getTextValue(string TEXT)
        {
            // 保证发送+接收的原子性
            await semaphore.WaitAsync();
            try
            {
                // path = MyTool.get_filePath();
                var requestData = new
                {
                    F = "GetContentsByTextName_C2S",
                    doc = "1.orzx",
                    shapeName = TEXT
                };
                var requestJson = JsonConvert.SerializeObject(requestData) + "#";
                await SendMessageAsync(requestJson);
                return await ReceiveMessageAsync();
            }
            finally
            {
                semaphore.Release();
            }
           
        }


        public async Task<string> upDateText(int p,string t,string docName)
        {
            // 保证发送+接收的原子性
            await semaphore.WaitAsync();
            try
            {
                // path = MyTool.get_filePath();
                var requestData = new
                {
                    F = "SetTextInfo_C2S",
                    doc = docName,
                    PosIndex = p,
                    Text = t
                };
                var requestJson = JsonConvert.SerializeObject(requestData) + "#";
                await SendMessageAsync(requestJson);
                Program.Logger.Info("替换文本：" + requestJson);
                var result =  await ReceiveMessageAsync();
                Program.Logger.Info("替换反馈：" + result);
                return result;

            }
            finally
            {
                semaphore.Release();
            }

        }



    }
}

