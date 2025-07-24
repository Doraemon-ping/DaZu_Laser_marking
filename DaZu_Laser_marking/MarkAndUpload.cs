using DaZu_Laser_marking.Model;
using DaZu_Laser_marking.NET;
using DaZu_Laser_marking.SQLite;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DaZu_Laser_marking
{
    public class MarkAndUpload
    {
        //这里实现替换文本和打标逻辑

        public static async Task MarkW04Async(Model.AddEquipInfo mes , 
                                              DaBiao DB , 
                                              dataSql dsq , 
                                              String url , 
                                              MainFormStatus status,
                                              int pox1,
                                              int pox2,
                                              string FileName,
                                              List<string> Equpments
            ){

            MessageBox.Show("打标");

            if (status.IsRecode == false)
            {
                //不重码
                MessageBox.Show("不重码正常生产！");
                //本地保存
                try
                {
                    dsq.insert(mes.BarCode, mes.mainPartCode, DateTime.Now);
                    status.Save1 = 2;
                    status.Res = "本地保存成功!" + "\n" + status.Res;
                }
                catch (Exception ex) 
                {
                    status.Res = "本地保存失败!" + "\n" + status.Res;
                }

                //上传MES
                string json = System.Text.Json.JsonSerializer.Serialize(mes);
                try
                {
                    Program.Logger.Info("请求数据；" + json);
                    var response = await MyNet.myPost(url, json);
                    Program.Logger.Info(response);
                    serverResponse serverresponse = JsonConvert.DeserializeObject<serverResponse>(response);
                    int code = serverresponse.Code;
                    if (code == 201)
                    {
                        Program.Logger.Error("失败！错误信息：" + response);
                        status.MesSave = 0;
                        status.Res = "MES上传失败!" + "\n" + status.Res;
                    }
                    else if (code == 0)
                    {
                        status.MesSave = 2;
                        Program.Logger.Info("保存成功！" + response);
                        status.Res = "MES上传成功!" + "\n" + status.Res;
                    }
                }
                catch (Exception ex)
                {
                    status.Res = "MES上传失败!" + "\n" + status.Res;
                    status.MesSave = 0;

                }
                
                //打标
                status.Res = "左件开始打标\n" + status.Res ;
                Program.Logger.Info("正常模式，铸造码：" + mes.barCode + "客户码：" + mes.mainPartCode);

                try
                {
                    string re = await DB.Login();
                    var jsonObjres = JObject.Parse(re.Replace("#", ""));
                    if (jsonObjres["status"].ToString() == "success")
                    {
                        status.Res = "登录成功!" + "\n" + status.Res;
                        status.EqupmentLoginLab = 2;
                    }
                    else
                    {
                        status.Res = "登录失败!" + "\n" + status.Res;
                    }
                }
                catch (Exception ex) {
                    status.Res = "登录失败!" + "\n" + status.Res;
                }


                string ymd = mes.mainPartCode.Substring(17, 10);
                string res1 = await DB.upDateText(pox1, mes.mainPartCode, FileName);//替换二维码
                string res2 = await DB.upDateText(pox2, ymd, FileName);//替换序列号
                try
                {
                    var jsonObj1 = JObject.Parse(res1.Replace("#", ""));
                    var jsonObj2 = JObject.Parse(res2.Replace("#", ""));
                    if (jsonObj1["status"].ToString() == "success" && jsonObj2["status"].ToString() == "success")
                    {
                        status.Res = "写入成功!" + "\n" + status.Res;
                        status.QrcodeUpdate1 = 2;

                        string res = await DB.StartMarking(Equpments);
                        var jsonObj3 = JObject.Parse(res.Replace("#", ""));
                        if (jsonObj3["status"].ToString() == "success")
                        {
                            status.Res = "打标成功!" + "\n" + status.Res;
                            status.Mark = 2;
                        }
                        else
                        {
                            status.Res = "打标失败!" + "\n" + status.Res;
                        }
                    }
                    else
                    {
                        status.Res = "写入失败!" + "\n" + status.Res;
                    }
                }
                catch (Exception ex)
                {
                    status.Res = "写入失败!" + "\n" + status.Res;
                }
            }
            else
            {
                //重码
                MessageBox.Show("重码正常生产！");

                status.Res = "左件开始打标\n" + status.Res;
                Program.Logger.Info("正常模式，铸造码：" + mes.barCode + "客户码：" + mes.mainPartCode);

                try
                {
                    string re = await DB.Login();
                    var jsonObjres = JObject.Parse(re.Replace("#", ""));

                    string re1 = jsonObjres["status"].ToString();

                    if (re1 == "success")
                    {
                        status.Res = "登录成功!" + "\n" + status.Res;
                        status.EqupmentLoginLab = 2;
                    }
                    else
                    {
                        status.Res = "登录失败!" + "\n" + status.Res;
                    }
                }
                catch (Exception ex)
                {
                    status.Res = "登录失败!" + "\n" + status.Res;
                }
                string ymd = mes.mainPartCode.Substring(17, 10);
                string res1 = await DB.upDateText(pox1, mes.mainPartCode, FileName);//替换二维码
                string res2 = await DB.upDateText(pox2, ymd, FileName);//替换序列号

                try
                {
                    var jsonObj1 = JObject.Parse(res1.Replace("#", ""));
                    var jsonObj2 = JObject.Parse(res2.Replace("#", ""));
                    if (jsonObj1["status"].ToString() == "success" && jsonObj2["status"].ToString() == "success")
                    {
                        status.Res = "写入成功!" + "\n" + status.Res;
                        status.QrcodeUpdate1 = 2;

                        string res = await DB.StartMarking(Equpments);
                        var jsonObj3 = JObject.Parse(res.Replace("#", ""));
                        if (jsonObj3["status"].ToString() == "success")
                        {
                            status.Res = "打标成功!" + "\n" + status.Res;
                            status.Mark = 2;
                        }
                        else
                        {
                            status.Res = "打标失败!" + "\n" + status.Res;
                        }
                    }
                    else
                    {
                        status.Res = "写入失败!" + "\n" + status.Res;
                    }
                }
                catch (Exception ex)
                {
                    status.Res = "写入失败!" + "\n" + status.Res;
                }
            }
        }

        public void MarkLeo() { }


        public void MarkE3S6() { }



    }







}
