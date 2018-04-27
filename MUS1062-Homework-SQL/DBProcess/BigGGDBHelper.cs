using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MUS1062_Homework_SQL
{
    class BigGGDBHelper : DBHelper<Airq>
    {
        static int count = 0;
        SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=" + System.IO.Directory.GetParent(System.Environment.CurrentDirectory).Parent.FullName + @"\AppData\HomeWorkDB.mdf;Integrated Security = True");

        public void DeleteData(int id)
        {
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = string.Format($"DELETE FROM Airq WHERE Id={id}");
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void InsertData(Airq item)
        {
            count++;
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = string.Format($"INSERT INTO Airq (Id,Air_area,Amp,AQI,Date) " +
                                            $"values ('{count}',N'{item.Air_area}',N'{item.Amp}',N'{item.AQI}',N'{item.Date}')");
            cmd.ExecuteNonQuery();
            connection.Close();

        }

        public List<Airq> ReadData(string col_name, string name)
        {
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = string.Format($"SELECT * FROM Airq WHERE {col_name}=N'{name}'");
            SqlDataReader reader = cmd.ExecuteReader();
            List<Airq> Air = new List<Airq>();
            while (reader.Read())
            {
                Airq aa = new Airq
                {
                    Air_area = reader[1].ToString(),
                    Amp = reader[2].ToString(),
                    AQI = reader[3].ToString(),
                    Date = reader[4].ToString()
                };
                Air.Add(aa);
            }
            connection.Close();
            return Air;
        }

        public void ShowData(List<Airq> list)
        {
            list.ForEach(x =>
            {
                Console.WriteLine("地區:" + x.Air_area);
                Console.WriteLine("主要汙染:" + x.Amp);
                Console.WriteLine("AQI值:" + x.AQI);
                Console.WriteLine("預測日期:" + x.Date);
                Console.WriteLine("----------------------------");
            });
        }

        public void UpdateData(int id, Airq item)
        {
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = string.Format($"UPDATE Airq SET Air_Area = N'{item.Air_area}',Amp = N'{item.Amp}',AQI = N'{item.AQI}',Date = N'{item.Date}' WHERE Id = {id}");
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public List<Airq> Xml_Load()
        {
            XElement xml = XElement.Load(@".\..\..\AppData\Airr.xml");
            List<Airq> Air = new List<Airq>();
            xml.Descendants("row").ToList().ForEach(row => {
                Airq aa = new Airq
                {
                    Air_area = row.Element("Col2").Value,
                    Amp = row.Element("Col3").Value,
                    AQI = row.Element("Col4").Value,
                    Date = row.Element("Col5").Value
                };
                Air.Add(aa);
            });
            return Air;
        }
    }
}