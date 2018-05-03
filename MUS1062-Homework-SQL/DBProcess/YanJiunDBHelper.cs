using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace MUS1062_Homework_SQL
{
    class YanJiunDBHelper:DBHelper<Forex>
    {
        public List<Forex> Xml_Load()
        {
            List<Forex> data=new List<Forex>();
            XElement xml = XElement.Load(@".\..\..\AppData\YanJiun.xml");

            xml.Descendants("row").ToList().ForEach(row => {
                Forex temp = new Forex {
                    月別=row.Element("月別").Value,
                    新台幣=row.Element("新台幣").Value,
                    人民幣=row.Element("人民幣").Value,
                    日圓=row.Element("日圓").Value,
                    韓元=row.Element("韓元").Value,
                    新加坡元=row.Element("新加坡元").Value,
                    歐元=row.Element("歐元").Value,
                    英鎊=row.Element("英鎊").Value,
                    澳幣=row.Element("澳幣").Value
                };
                data.Add(temp);
            });
            return data;
        }

        static int counter = 0;
        static string path = System.Environment.CurrentDirectory;
        SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=" + System.IO.Directory.GetParent(path).Parent.FullName + @"\AppData\HomeWorkDB.mdf;Integrated Security = True");

        public void InsertData(Forex item)
        {
            ++counter;

            connection.Open();
            SqlCommand cmd = connection.CreateCommand();

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = string.Format($"INSERT INTO Forex (Id,月別,新台幣,人民幣,日圓,韓元,新加坡元,歐元,英鎊,澳幣)"+
                $"values('{counter}',N'{item.月別}',N'{item.新台幣}',N'{item.人民幣}',N'{item.日圓}',N'{item.韓元}',N'{item.新加坡元}',N'{item.歐元}',N'{item.英鎊}',N'{item.澳幣}')");
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public List<Forex> ReadData(String col_name, String name)
        {
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = string.Format($"SELECT * FROM Forex WHERE {col_name}=N'{name}'");

            SqlDataReader reader = cmd.ExecuteReader();
            List<Forex> data = new List<Forex>();

            while (reader.Read())
            {
                Forex temp = new Forex
                {
                    月別 = reader[1].ToString(),
                    新台幣 = reader[2].ToString(),
                    人民幣 = reader[3].ToString(),
                    日圓 = reader[4].ToString(),
                    韓元 = reader[5].ToString(),
                    新加坡元 = reader[6].ToString(),
                    歐元 = reader[7].ToString(),
                    英鎊 = reader[8].ToString(),
                    澳幣 = reader[9].ToString()
                };
                data.Add(temp);
            }

            connection.Close();
            return data;
        }

        public void ShowData(List<Forex> list)
        {
            list.ForEach(element => {
                Console.WriteLine("月別:{0}\n新台幣:{1}\n人民幣:{2}\n日圓:{3}\n歐元:{4}\n英鎊:{5}\n-------------------------------",element.月別,element.新台幣,element.人民幣,element.日圓,element.歐元,element.英鎊);
            });
        }

        public void UpdateData(int id, Forex item)
        {
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = string.Format($"UPDATE Forex SET 月別 = N'{item.月別}',新台幣 = N'{item.新台幣}',人民幣 = N'{item.人民幣}',日圓 = N'{item.日圓}',韓元 = N'{item.韓元}',新加坡元 = N'{item.新加坡元}',歐元 = N'{item.歐元}',英鎊 = N'{item.英鎊}',澳幣 = N'{item.澳幣}' WHERE Id = {id} ");

            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void DeleteData(int id)
        {
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = string.Format($"DELETE FROM Forex WHERE Id={id}");

            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void ClearData()
        {
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = string.Format($"DELETE FROM Forex WHERE");
            counter = 0;

            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}
