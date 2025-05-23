using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DaZu_Laser_marking.NET
{
    public class MyNet
    {

        public static async Task<string> myPost(string url, string jsonData)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Request failed with status code: {response.StatusCode}");
                }

                return await response.Content.ReadAsStringAsync();
            }

            //return response;


        }

        public static bool isConnettionToIp(string ip)
        {
            try
            {
                using (Ping ping = new Ping())
                {
                    PingReply reply = ping.Send(ip);
                    return reply.Status == IPStatus.Success;
                }
            }


            catch (Exception ex)
            {
                // 处理可能的异常，例如 Ping 请求超时、地址无效等
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }

        }
    }
}
