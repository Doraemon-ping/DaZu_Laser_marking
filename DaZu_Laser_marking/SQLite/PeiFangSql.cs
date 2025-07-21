using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DaZu_Laser_marking.SQLite
{
    internal class PeiFangSql
    {
        private string connectionString;

        public PeiFangSql()
        {
            DateTime dateTime = DateTime.Now;
            string riqi = dateTime.Year.ToString() + "_" + dateTime.Month.ToString();
            string absoluteDbFilePath = "SqliteDb\\" + "生产数据.db";
            string directoryPath = "SqliteDb\\";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            connectionString = $"Data Source={absoluteDbFilePath};Version=3;";
            Console.WriteLine(connectionString);
            //load();
        }

        public void load()
        {
           
        
        }

       

        public List<object> getById(int id)
        {
            List<object> result = new List<object>();
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    string selectQuery = "SELECT  * FROM  PF WHERE ID = @ID;";
                    connection.Open();

                    using (var command = new SQLiteCommand(selectQuery, connection))
                    {
                        // 添加查询参数
                        command.Parameters.AddWithValue("@ID", id);
                       // command.Parameters.AddWithValue("@NAME", name);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // 读取一行
                            {

                                try
                                {
                                    result.Add(reader[0]);
                                    result.Add(reader[1]);
                                    result.Add(reader[2]);
                                    result.Add(reader[3]);
                                    result.Add(reader[4]);
                                    result.Add(reader[5]);
                                    result.Add(reader[6]);
                                    result.Add(reader[7]);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }




                                // Console.WriteLine($"Column1: {column1Value}, Column2: {column2Value}");
                            }


                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("查询失败！" + e.Message);

            }
            return result;
        }


        public void updateByID(string name , string th , int num , int str , string strs , int end , string ends , int id) {

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE PF SET NAME=@name,TH=@th,NUMBER=@number,STR=@str,STRs=@strs,[END]=@end,ENDs=@ends WHERE ID=@id";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@th", th);
                        command.Parameters.AddWithValue("@number", num);
                        command.Parameters.AddWithValue("@str", str);
                        command.Parameters.AddWithValue("@strs", strs);
                        command.Parameters.AddWithValue("@end", end);
                        command.Parameters.AddWithValue("@ends", ends);
                        command.Parameters.AddWithValue("@id", id);
                        int rowsAffected = command.ExecuteNonQuery(); // 执行更新
                        Console.WriteLine($"{rowsAffected} rows updated.");
                        MessageBox.Show("保存成功!");
                    }
                }
            }
            catch (Exception ex){
                Program.Logger.Info(ex.Message);
            }


        }
    }
}
