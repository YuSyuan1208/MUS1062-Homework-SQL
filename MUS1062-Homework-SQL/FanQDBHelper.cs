using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MUS1062_Homework_SQL
{
	class FnaQDBHelper : DBHelper<rainfall>
	{
		static int count = 0;
		SqlConnection connection = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = HomeworkDB; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
		public void InsertData(rainfall rain)
		{
			count++;
			connection.Open();
			SqlCommand cmd = connection.CreateCommand();
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = string.Format($"INSERT INTO RainFall (Id,地點名稱,地點縣市別,地點鄉鎮市區,累積雨量) " +
														$"values ('{count}',N'{rain.地點名稱}',N'{rain.地點縣市別}',N'{rain.地點鄉鎮市區}',N'{rain.累積雨量}')");
			cmd.ExecuteNonQuery();
			connection.Close();
		}
		public List<rainfall> ReadData(String col_name, String name)
		{
			connection.Open();
			SqlCommand cmd = connection.CreateCommand();
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = string.Format($"SELECT * FROM RainFall WHERE {col_name}=N'{name}'");
			SqlDataReader reader = cmd.ExecuteReader();
			List<rainfall> rain = new List<rainfall>();
			while (reader.Read())
			{
				rainfall r = new rainfall
				{
					地點名稱 = reader[1].ToString(),
					地點縣市別 = reader[2].ToString(),
					地點鄉鎮市區 = reader[3].ToString(),
					累積雨量 = reader[4].ToString()
				};
				rain.Add(r);
			}
			connection.Close();
			return rain;
		}
		public List<rainfall> Xml_Load()
		{
			XElement xml = XElement.Load(@".\..\..\O-A0002-001.xml");
			List<rainfall> rain = new List<rainfall>();

			XNamespace xmln = @"urn:cwb:gov:tw:cwbcommon:0.1";
			var stationsNode = xml.Descendants(xmln + "location").ToList();
			stationsNode.Where(x => !x.IsEmpty).ToList().ForEach(stationNode =>
			{
				rainfall station = new rainfall();
				station.地點名稱 = stationNode.Element(xmln + "locationName").Value;

				int num = 1;
				stationNode.Descendants(xmln + "parameter").ToList().ForEach(parameterNode =>
				{
					if (num == 1) station.地點縣市別 = parameterNode.Element(xmln + "parameterValue").Value;
					else if (num == 3) station.地點鄉鎮市區 = parameterNode.Element(xmln + "parameterValue").Value;
					num++;
				});

				num = 1;
				stationNode.Descendants(xmln + "weatherElement").ToList().ForEach(weatherNode =>
				{
					if (num == 7) station.累積雨量 = weatherNode.Element(xmln + "elementValue").Element(xmln + "value").Value;
					num++;
				});

				rain.Add(station);
			});
			return rain;
		}
		public void ShowData(List<rainfall> list)
		{
			list.ForEach(r => {
				Console.WriteLine("地點名稱:{0}\n地址:{1}\n日累積雨量:{2}------------", r.地點名稱, r.地點縣市別 + r.地點鄉鎮市區 , r.累積雨量);
			});
		}
	}
}
