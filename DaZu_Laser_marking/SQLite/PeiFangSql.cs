using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                                result.Add(reader[0]);
                                result.Add(reader[1]);
                                result.Add(reader[2]);
                                result.Add(reader[3]);
                                result.Add(reader[4]);



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
    }
}
