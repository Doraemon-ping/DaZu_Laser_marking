using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace DaZu_Laser_marking
{
    //打标协议的实现
    internal class DaBiao
    {
        private string IP;
        private string PORT;

        public DaBiao(int id, string name)
        {

            MySqlite msl = new MySqlite();
            List<object> result = msl.getByIdName(id, name);
            IP = result[0].ToString();
            PORT = result.ToString();

        }
        //登录
        public string Login()
        {
            using (var client = new TcpClient(IP, int.Parse(PORT)))
            {
                using (var stream = client.GetStream())
                {
                    // 构建请求
                    var requestData = new
                    {
                        F = "L",
                        P = "123456"
                    };
                    var requestJson = JsonConvert.SerializeObject(requestData) + "#";

                    // 发送请求
                    byte[] data = Encoding.UTF8.GetBytes(requestJson);
                    stream.Write(data, 0, data.Length);

                    // 接收响应
                    byte[] responseData = new byte[256];
                    int bytes = stream.Read(responseData, 0, responseData.Length);
                    return Encoding.UTF8.GetString(responseData, 0, bytes);
                }
            }

        }

        //获取所有设备
        public string GetAllEqupments() {

            using (var client = new TcpClient(IP, int.Parse(PORT)))
            {
                using (var stream = client.GetStream())
                {
                    // 构建请求
                    var requestData = new
                    {
                        F = "GADS"
                    };
                    var requestJson = JsonConvert.SerializeObject(requestData) + "#";

                    // 发送请求
                    byte[] data = Encoding.UTF8.GetBytes(requestJson);
                    stream.Write(data, 0, data.Length);

                    // 接收响应
                    byte[] responseData = new byte[256];
                    int bytes = stream.Read(responseData, 0, responseData.Length);
                    return Encoding.UTF8.GetString(responseData, 0, bytes);
                }
            }
        }

        //获取所有文档
        public string GetAllWords() {
            using (var client = new TcpClient(IP, int.Parse(PORT)))
            {
                using (var stream = client.GetStream())
                {
                    // 构建请求
                    var requestData = new
                    {
                        F = "GADC"
                    };
                    var requestJson = JsonConvert.SerializeObject(requestData) + "#";

                    // 发送请求
                    byte[] data = Encoding.UTF8.GetBytes(requestJson);
                    stream.Write(data, 0, data.Length);

                    // 接收响应
                    byte[] responseData = new byte[256];
                    int bytes = stream.Read(responseData, 0, responseData.Length);
                    return Encoding.UTF8.GetString(responseData, 0, bytes);
                }
            }

        }

        //打开文件
        public string OpenFileDialog() {
            string path = MyTool.get_filePath();
            using (var client = new TcpClient(IP, int.Parse(PORT)))
            {
                using (var stream = client.GetStream())
                {
                    // 构建请求
                    var requestData = new
                    {
                        F = "DOC",
                        DP = path
                    };
                    var requestJson = JsonConvert.SerializeObject(requestData) + "#";

                    // 发送请求
                    byte[] data = Encoding.UTF8.GetBytes(requestJson);
                    stream.Write(data, 0, data.Length);

                    // 接收响应
                    byte[] responseData = new byte[256];
                    int bytes = stream.Read(responseData, 0, responseData.Length);
                    return Encoding.UTF8.GetString(responseData, 0, bytes);
                }
            }
        }

        //保存文档
        public string SaveAllFileDialog() {
            using (var client = new TcpClient(IP, int.Parse(PORT)))
            {
                using (var stream = client.GetStream())
                {
                    // 构建请求
                    var requestData = new
                    {
                        F = "SDC",
                        DC = ""
                    };
                    var requestJson = JsonConvert.SerializeObject(requestData) + "#";

                    // 发送请求
                    byte[] data = Encoding.UTF8.GetBytes(requestJson);
                    stream.Write(data, 0, data.Length);

                    // 接收响应
                    byte[] responseData = new byte[256];
                    int bytes = stream.Read(responseData, 0, responseData.Length);
                    return Encoding.UTF8.GetString(responseData, 0, bytes);
                }
            }



        }

        //关闭所有文档
        public string CloseAllFileDialog() {
            using (var client = new TcpClient(IP, int.Parse(PORT)))
            {
                using (var stream = client.GetStream())
                {
                    // 构建请求
                    var requestData = new
                    {
                        F = "CDC",
                        DC = ""
                    };
                    var requestJson = JsonConvert.SerializeObject(requestData) + "#";

                    // 发送请求
                    byte[] data = Encoding.UTF8.GetBytes(requestJson);
                    stream.Write(data, 0, data.Length);

                    // 接收响应
                    byte[] responseData = new byte[256];
                    int bytes = stream.Read(responseData, 0, responseData.Length);
                    return Encoding.UTF8.GetString(responseData, 0, bytes);
                }
            }


        }

        //开始标刻
        public string StartMarking(string word ,List<string> equpment ) {
            using (var client = new TcpClient(IP, int.Parse(PORT)))
            {
                using (var stream = client.GetStream())
                {
                    // 构建请求
                    var requestData = new
                    {
                        F = "M",
                        DS = equpment
                    };
                    var requestJson = JsonConvert.SerializeObject(requestData) + "#";

                    // 发送请求
                    byte[] data = Encoding.UTF8.GetBytes(requestJson);
                    stream.Write(data, 0, data.Length);

                    // 接收响应
                    byte[] responseData = new byte[256];
                    int bytes = stream.Read(responseData, 0, responseData.Length);
                    return Encoding.UTF8.GetString(responseData, 0, bytes);
                }
            }
        }


        public string StartMarkingByWord(string words , List<string> equpment) {
            using (var client = new TcpClient(IP, int.Parse(PORT)))
            {
                using (var stream = client.GetStream())
                {
                    // 构建请求
                    var requestData = new
                    {
                        F = "ME",
                        DC= words,
                        DS = equpment
                    };
                    var requestJson = JsonConvert.SerializeObject(requestData) + "#";

                    // 发送请求
                    byte[] data = Encoding.UTF8.GetBytes(requestJson);
                    stream.Write(data, 0, data.Length);

                    // 接收响应
                    byte[] responseData = new byte[256];
                    int bytes = stream.Read(responseData, 0, responseData.Length);
                    return Encoding.UTF8.GetString(responseData, 0, bytes);
                }
            }


        }

    }
}
