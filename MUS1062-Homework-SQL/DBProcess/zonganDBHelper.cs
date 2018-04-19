using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MUS1062_Homework_SQL
{
    class zonganDBHelper : DBHelper<Career>
    {
        static int count = 0;
        SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=" + System.IO.Directory.GetParent(System.Environment.CurrentDirectory).Parent.FullName + @"\AppData\HomeWorkDB.mdf;Integrated Security = True");
        public void InsertData(Career car)
        {
            count++;
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = string.Format($"INSERT INTO LiverBurst (Id,資料集名稱,服務分類) " +
                                                        $"values ('{count}',N'{car.資料集名稱}',N'{car.服務分類}')");
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        public List<Career> ReadData(String col_name, String name)
        {
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = string.Format($"SELECT * FROM LiverBurst WHERE {col_name}=N'{name}'");
            SqlDataReader reader = cmd.ExecuteReader();
            List<Career> car = new List<Career>();
            while (reader.Read())
            {
                Career ca = new Career
                {
                    資料集名稱 = reader[1].ToString(),
                    服務分類 = reader[2].ToString(),

                };
                car.Add(ca);
            }
            connection.Close();
            return car;
        }
        public List<Career> Xml_Load()
        {
            XElement xml = XElement.Load(@".\..\..\AppData\zongan.xml");
            List<Career> car = new List<Career>();
            xml.Descendants("node").ToList().ForEach(row => {
                Career ca = new Career
                {
                    資料集名稱 = row.Element("資料集名稱").Value,
                    服務分類 = row.Element("服務分類").Value
                };
                car.Add(ca);
            });
            return car;
        }
        public void ShowData(List<Career> list)
        {
            list.ForEach(ca => {
                Console.WriteLine("資料集名稱:{0}\n服務分類:{1}\n", ca.資料集名稱, ca.服務分類);
            });
        }

        public void UpdateData(int id, Career item)
        {
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = string.Format($"UPDATE LiverBurst SET 資料集名稱 = N'{item.資料集名稱}',服務分類 = N'{item.服務分類}' WHERE Id={id}");
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void DeleteData(int id)
        {
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = string.Format($"DELETE FROM LiverBurst WHERE Id={id}");
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}