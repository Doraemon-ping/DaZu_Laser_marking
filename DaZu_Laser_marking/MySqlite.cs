using DaZu_Laser_marking;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace DaZu_Laser_marking
{
    internal class MySqlite
    {

        private string connectionString;

        public MySqlite()
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
            // Batteries.Init();
            try
            {
                // Create a new database connection
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Create a table
                    string createTableQuery = @"
                        CREATE TABLE IF NOT EXISTS 生产数据 (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            BARCODE TEXT NOT NULL,
                            KEHUBARCODE REAL NOT NULL,
                            CREATEE TEXT NOT NULL
                        );";

                    using (var command = new SQLiteCommand(createTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }
            catch (Exception e) {Console.WriteLine(e.Message); }
        }

        public void insert(string bar, string newbar, string create)
        {
            Console.WriteLine("开始保存！");
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open(); // 打开数据库连接

                    // 插入数据的 SQL 语句
                    string insertQuery = "INSERT INTO 生产数据 (BARCODE, NIUJU, CREATEE) VALUES (@barcode, @niu, @date);";

                    // 创建 SQL 命令
                    using (var command = new SQLiteCommand(insertQuery, connection))
                    {
                        // 添加参数并赋值
                        command.Parameters.AddWithValue("@barcode", bar);
                        command.Parameters.AddWithValue("@niu", newbar);
                        command.Parameters.AddWithValue("@date", create);

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


        public DataTable getTest()
        {

            //string connectionString = "Data Source=MyDatabase.db;Version=3;";

            // SQL query to retrieve data from the table
            string selectQuery = "SELECT * FROM 生产数据;";
            DataTable dataTable = new DataTable();

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
        //

        public DataTable getBydate(string start, string end)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    string selectQuery = "SELECT Id , BARCODE AS 二维码,KEHUBARCODE AS 客户码,CREATEE AS 测试时间 FROM NIJUDATA WHERE CREATEE >= @startDate AND CREATEE <= @endDate;";
                    connection.Open();

                    using (var command = new SQLiteCommand(selectQuery, connection))
                    {
                        // 添加查询参数
                        command.Parameters.AddWithValue("@startDate", start);
                        command.Parameters.AddWithValue("@endDate", end);

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
    }
}
