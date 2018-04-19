using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MUS1062_Homework_SQL
{
    class JackDBHelper : DBHelper<UVIResource>
    {
        static int count = 0;
        SqlConnection connection = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = HomeworkDB; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
        public void InsertData(UVIResource ph)
        {
            count++;
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = string.Format($"INSERT INTO UVIResource (Id,城市,發布機關,發布地區,紫外線指數,發布時間) " +
                                                        $"values ('{count}',N'{ph.城市}',N'{ph.發布機關}',N'{ph.發布地區}',N'{ph.紫外線指數}',N'{ph.發布時間}')");
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        public List<UVIResource> ReadData(String col_name, String name)
        {
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = string.Format($"SELECT * FROM UVIResource WHERE {col_name}=N'{name}'");
            SqlDataReader reader = cmd.ExecuteReader();
            List<UVIResource> phar = new List<UVIResource>();
            while (reader.Read())
            {
                UVIResource ph = new UVIResource
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
        public List<UVIResource> Xml_Load()
        {
            XElement xml = XElement.Load("datas.xml");
            List<UVIResource> phar = new List<UVIResource>();
            xml.Descendants("row").ToList().ForEach(row => {
                UVIResource ph = new UVIResource
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
        public void ShowData(List<UVIResource> list)
        {
            list.ForEach(ph => {
                 Console.WriteLine("城市:{0}\n地區:{1}\nUVI:{2}", ph.城市, ph.發布地區, ph.紫外線指數);
             });
        }
    }
}
