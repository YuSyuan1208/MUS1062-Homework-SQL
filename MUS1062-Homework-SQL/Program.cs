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
            Nan();
            FanQ();
            Jack();
            Zongan();
            Console.WriteLine("按任意鍵繼續");
            Console.ReadKey();
        }
        static void FanQ() {
            DBHelper<rainfall> FanDB = new FnaQDBHelper();
            //FanQInsert
            //FanDB.Xml_Load().ForEach(x => {
            //    FanDB.InsertData(x);
            //});
            //FanQ Search
            //FanDB.ShowData(FanDB.ReadData("地點縣市別", "高雄市"));
            //FandQ Update
            //rainfall r = new rainfall
            //{
            //    地點名稱 = "這裡",
            //    地點縣市別 = "縣市",
            //    地點鄉鎮市區 = "市區",
            //    累積雨量 = "累積雨量"
            //};
            //FanDB.UpdateData(1, r);
            //FanQ DELETE
            //FanDB.DeleteData(1);
        }
        static void Jack()
        {
            DBHelper<UVIResource> JackDB = new JackDBHelper();
            //JackDB.Xml_Load().ForEach(x => {
            //    JackDB.InsertData(x);
            //});
            //Jack Search
            //JackDB.ShowData(JackDB.ReadData("城市","高雄市"));
            //Jack Update
            //UVIResource uv = new UVIResource {
            //    城市 = "城市",
            //    發布地區 = "發布地區",
            //    發布時間 = "發布時間",
            //    發布機關 = "發布機關",
            //    紫外線指數 = "0"
            //};
            //JackDB.UpdateData(1, uv);
            //JackDB.DeleteData(1);
        }
        static void Zongan()
        {
            //ZonGan Insert
            DBHelper<Career> ZongDB = new zonganDBHelper();
            //ZongDB.Xml_Load().ForEach(x => {
            //    ZongDB.InsertData(x);
            //});
            //ZonGan Search
            //ZongDB.ShowData(ZongDB.ReadData("服務分類", "求職及就業"));
            //ZongDB.UpdateData(1, new Career { 服務分類 = "服務分類",資料集名稱="資料及名稱"});
            //ZongDB.DeleteData(1);
        }
        static void Nan()
        {
            DBHelper<Pharmacy> nanDB = new NanDBHelper();
            //Nan Insert
            //nanDB.Xml_Load().ForEach(x => {
            //    nanDB.InsertData(x);
            //});
            //Nan Search
            //nanDB.Xml_Load().ForEach(x => {
            //    nanDB.InsertData(x);
            //});
            //nanDB.ShowData(nanDB.ReadData("地址縣市別", "臺南市"));
            //
            //Pharmacy pharmacy = new Pharmacy
            //{
            //    地址縣市別 = "縣市",
            //    地址街道巷弄號 = "巷弄",
            //    地址鄉鎮市區 = "市區",
            //    是否為健保特約藥局 = "N",
            //    機構名稱 = "名稱",
            //    機構狀態 = "die",
            //    負責人姓名 = "姓名",
            //    負責人性別 = "第三性",
            //    電話 = "電話"
            //};
            //Nan Delete
            //nanDB.UpdateData(1, pharmacy);
            //nanDB.DeleteData(1);
        }
    }
}
