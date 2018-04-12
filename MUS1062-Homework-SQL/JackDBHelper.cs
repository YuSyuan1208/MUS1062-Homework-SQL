using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MUS1062_Homework_SQL
{
    class JackDBHelper : DBHelper<Pharmacy>
    {
        static int count = 0;
        SqlConnection connection = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = HomeworkDB; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
        public void InsertData(Pharmacy ph)
        {
            count++;
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = string.Format($"INSERT INTO Pharmacy (Id,城市,發布機關,發布地區,紫外線指數,發布時間) " +
                                                        $"values ('{count}',N'{ph.城市}',N'{ph.發布機關}',N'{ph.發布地區}',N'{ph.紫外線指數}',N'{ph.發布時間}')");
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        public List<Pharmacy> ReadData(String col_name, String name)
        {
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = string.Format($"SELECT * FROM Pharmacy WHERE {col_name}=N'{name}'");
            SqlDataReader reader = cmd.ExecuteReader();
            List<Pharmacy> phar = new List<Pharmacy>();
            while (reader.Read())
            {
                Pharmacy ph = new Pharmacy
                {
                    城市 = reader[1].ToString(),
                    發布機關 = reader[2].ToString(),
                    發布地區 = reader[3].ToString(),
                    紫外線指數 = reader[4].ToString(),
                    發布時間 = reader[5].ToString(),
                };
                phar.Add(ph);
            }
            connection.Close();
            return phar;
        }
        public List<Pharmacy> Xml_Load()
        {
            XElement xml = XElement.Load("datas.xml");
            List<Pharmacy> phar = new List<Pharmacy>();
            xml.Descendants("row").ToList().ForEach(row => {
                Pharmacy ph = new Pharmacy
                {
                    城市 = row.Element("Col1").Value,
                    發布機關 = row.Element("Col2").Value,
                    發布地區 = row.Element("Col3").Value,
                    紫外線指數 = row.Element("Col4").Value,
                    發布時間 = row.Element("Col5").Value,
                };
                phar.Add(ph);
            });
            return phar;
        }
        public void ShowData(List<Pharmacy> list)
        {
            list.ForEach(ph => {
                 Console.WriteLine("城市:{0}\n地區:{1}\nUVI:{2}", ph.城市, ph.發布地區, ph.紫外線指數);
             });
        }
    }
}
