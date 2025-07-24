using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DaZu_Laser_marking.SQLite
{
    public class dataSql
    {
        private string connectionString;

        public dataSql()
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
            //
        }

        public void insert(string ZZM, string KHM, DateTime DT)
        {

            Console.WriteLine("开始保存！");
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open(); // 打开数据库连接

                    // 插入数据的 SQL 语句
                    string insertQuery = "INSERT INTO \"生产数据\" (\"BARCODE\",\"KEHUBARCODE\",\"CREATEE\") VALUES (@zzm,@khm,@dt);";

                    // 创建 SQL 命令
                    using (var command = new SQLiteCommand(insertQuery, connection))
                    {
                        // 添加参数并赋值
                        command.Parameters.AddWithValue("@zzm", ZZM);
                        command.Parameters.AddWithValue("@khm", KHM);
                        command.Parameters.AddWithValue("@dt", DT.ToString("yyyy-MM-dd HH:mm:ss"));

                        // 执行插入操作
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"成功插入了 {rowsAffected} 行数据。");
                    }
                }
                Console.WriteLine("保存成功！");

            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("失败！");

            }


        }


        public System.Data.DataTable getData()
        {
            //获取所有数据
            //string connectionString = "Data Source=MyDatabase.db;Version=3;";

            // SQL query to retrieve data from the table
            string selectQuery = "SELECT \"生产数据\".Id AS ID,\"生产数据\".BARCODE AS 铸造码 ,\"生产数据\".KEHUBARCODE AS 客户码 ,\"生产数据\".CREATEE AS 保存时间 ,IS_UPLOAD AS 保存成功 FROM \"生产数据\"";
            System.Data.DataTable dataTable = new System.Data.DataTable();

            // Create a new SQLite connection
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Create a new data adapter
                    using (var adapter = new SQLiteDataAdapter(selectQuery, connection))
                    {
                        // Create a new DataTable to hold the query results


                        // Fill the DataTable with data from the database
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        // dataGridView1.DataSource = dataTable;
                    }
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("查询失败！" + e.Message);
            }
            return dataTable;

        }

        public System.Data.DataTable getByCode(string barcode) {

            System.Data.DataTable dataTable = new System.Data.DataTable();


            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    string selectQuery = "SELECT \"生产数据\".Id AS ID,\"生产数据\".BARCODE AS 铸造码 ,\"生产数据\".KEHUBARCODE AS 客户码 ,\"生产数据\".CREATEE AS 保存时间 ,IS_UPLOAD AS 保存成功 FROM \"生产数据\" WHERE BARCODE=@bar";
                    connection.Open();

                    using (var command = new SQLiteCommand(selectQuery, connection))
                    {
                        // 添加查询参数
                        command.Parameters.AddWithValue("@bar", barcode);


                        // 使用 SQLiteDataAdapter 将数据填充到 DataTable 中
                        using (var adapter = new SQLiteDataAdapter(command))
                        {
                            adapter.Fill(dataTable);  // 将查询结果填充到 DataTable
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("查询失败！" + e.Message);

            }
            return dataTable;
        }


        //判断重码
        public int is_Only(string barcode)
        {
            int id = 0;
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    string selectQuery = "SELECT COUNT(*) FROM \"生产数据\" WHERE BARCODE=@bar";
                    connection.Open();

                    using (var command = new SQLiteCommand(selectQuery, connection))
                    {
                        // 添加查询参数
                        command.Parameters.AddWithValue("@bar", barcode);

                        using (var reader = command.ExecuteReader()) {

                            reader.Read();
                            id = reader.GetInt32(0);   // ✅ 只有在 Read() 返回 true 时才能访问数据
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("查询失败！" + e.Message);

            }
            return id;


        }

        //获取生产数量
        public int getNumner() {

            DateTime todayStart = DateTime.Today;
            string today = todayStart.ToString("yyyy-MM-dd HH:mm:ss");
            int id = 0;
            System.Console.WriteLine(today);

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    string selectQuery = "SELECT COUNT(*) FROM \"生产数据\" WHERE CREATEE>= @bar";
                    connection.Open();

                    using (var command = new SQLiteCommand(selectQuery, connection))
                    {
                        // 添加查询参数
                        command.Parameters.AddWithValue("@bar", today);

                        using (var reader = command.ExecuteReader())
                        {

                            reader.Read();
                            id = reader.GetInt32(0);   // ✅ 只有在 Read() 返回 true 时才能访问数据


                        }

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("查询失败！" + e.Message);

            }
            return id;





        }


        public string getKHM(string code) {

            string KHM = null;
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    string selectQuery = "SELECT KEHUBARCODE FROM \"生产数据\" WHERE BARCODE=@bar";
                    connection.Open();

                    using (var command = new SQLiteCommand(selectQuery, connection))
                    {
                        // 添加查询参数
                        command.Parameters.AddWithValue("@bar", code);

                        using (var reader = command.ExecuteReader()){

                            reader.Read();
                            KHM = reader.GetString(0); // ✅ 只有在 Read() 返回 true 时才能访问数据

                        }
                           


                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("查询失败！" + e.Message);

            }
            return KHM;


        }

        public bool updateUp(string barcode , int isUopLoad) {
            Console.WriteLine("开始保存！");
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open(); // 打开数据库连接

                    // 插入数据的 SQL 语句
                    string insertQuery = "UPDATE \"生产数据\"\r\nSET IS_UPLOAD=@up WHERE BARCODE=@code";

                    // 创建 SQL 命令
                    using (var command = new SQLiteCommand(insertQuery, connection))
                    {
                        // 添加参数并赋值
                        command.Parameters.AddWithValue("@up", isUopLoad);
                        command.Parameters.AddWithValue("@code", barcode);
                        // command.Parameters.AddWithValue("@dt", DT.ToString());

                        // 执行插入操作
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"成功插入了 {rowsAffected} 行数据。");
                    }
                }
                Console.WriteLine("保存成功！");
                return true;

            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("失败！");
                return false;

            }



        }


        public bool isUpLoad(string barcode) {

            bool up = false;
            try
            {

                using (var connection = new SQLiteConnection(connectionString))
                {
                    string selectQuery = "SELECT IS_UPLOAD FROM \"生产数据\" WHERE BARCODE=@code";
                    connection.Open();

                    using (var command = new SQLiteCommand(selectQuery, connection))
                    {
                        // 添加查询参数
                        command.Parameters.AddWithValue("@code",barcode);

                        using (var reader = command.ExecuteReader())
                        {

                            reader.Read();
                            bool res = reader.Read(); // ✅ 只有在 Read() 返回 true 时才能访问数据
                            up = res;


                        }



                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("查询失败！" + e.Message);

            }
            return up;

        }
    }
}
