using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace MUS1062_Homework_SQL
{
    class Program
    {
       
        static void Main(string[] args)
        {
            //Nan Search
            DBHelper<Pharmacy> nanDB = new NanDBHelper();
            //nanDB.Xml_Load().ForEach(x => {
            //    nanDB.InsertData(x);
            //});
            //nanDB.ShowData(nanDB.ReadData("地址縣市別", "臺南市"));
            //FanQInsert
            DBHelper<rainfall> FanDB = new FnaQDBHelper();
            //FanDB.Xml_Load().ForEach(x => {
            //    FanDB.InsertData(x);
            //});
            //FanQ Search
            //FanDB.ShowData(FanDB.ReadData("地點縣市別", "高雄市"));
            //ZonGan Insert
            DBHelper<Career> ZongDB = new zonganDBHelper();
            //ZongDB.Xml_Load().ForEach(x => {
            //    ZongDB.InsertData(x);
            //});
            //ZonGan Search
            //ZongDB.ShowData(ZongDB.ReadData("服務分類", "求職及就業"));
            DBHelper<UVIResource> JackDB = new JackDBHelper();
            //JackDB.Xml_Load().ForEach(x => {
            //    JackDB.InsertData(x);
            //});
            JackDB.ShowData(JackDB.ReadData("城市","高雄市"));
            Console.WriteLine("按任意鍵繼續");
            Console.ReadKey();
        }
       
    }
}
