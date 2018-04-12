using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MUS1062_Homework_SQL
{
    class NanDBHelper : DBHelper<Pharmacy>
    {
        static int count = 0;
        SqlConnection connection = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = HomeworkDB; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
        public void InsertData(Pharmacy ph)
        {
            count++;
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = string.Format($"INSERT INTO Pharmacy (Id,機構狀態,機構名稱,地址縣市別,地址鄉鎮市區,地址街道巷弄號,負責人姓名,負責人性別,電話,是否為健保特約藥局) " +
                                                        $"values ('{count}',N'{ph.機構狀態}',N'{ph.機構名稱}',N'{ph.地址縣市別}',N'{ph.地址鄉鎮市區}',N'{ph.地址街道巷弄號}',N'{ph.負責人姓名}',N'{ph.負責人性別}',N'{ph.電話}',N'{ph.是否為健保特約藥局}')");
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
                    機構狀態 = reader[1].ToString(),
                    機構名稱 = reader[2].ToString(),
                    地址縣市別 = reader[3].ToString(),
                    地址鄉鎮市區 = reader[4].ToString(),
                    地址街道巷弄號 = reader[5].ToString(),
                    負責人姓名 = reader[6].ToString(),
                    負責人性別 = reader[7].ToString(),
                    電話 = reader[8].ToString(),
                    是否為健保特約藥局 = reader[9].ToString()
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
                    機構狀態 = row.Element("Col1").Value,
                    機構名稱 = row.Element("Col2").Value,
                    地址縣市別 = row.Element("Col3").Value,
                    地址鄉鎮市區 = row.Element("Col4").Value,
                    地址街道巷弄號 = row.Element("Col5").Value,
                    負責人姓名 = row.Element("Col6").Value,
                    負責人性別 = row.Element("Col7").Value,
                    電話 = row.Element("Col8").Value,
                    是否為健保特約藥局 = row.Element("Col9").Value
                };
                phar.Add(ph);
            });
            return phar;
        }
        public void ShowData(List<Pharmacy> list)
        {
            list.ForEach(ph => {
                 Console.WriteLine("機構名稱:{0}\n地址:{1}\n------------", ph.機構名稱, ph.地址縣市別 + ph.地址鄉鎮市區 + ph.地址街道巷弄號);
             });
        }
    }
}
