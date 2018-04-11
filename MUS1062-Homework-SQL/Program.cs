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
            List<Pharmacy> pharmacies = Xml_Load();
            DBHelper db = new DBHelper();
            //pharmacies.ForEach(ph => {
            //    db.InsertData(ph);
            //});
            db.ReadData("地址縣市別", "臺南市").ForEach(ph => {
                Console.WriteLine("機構名稱:{0}\n地址:{1}\n------------", ph.機構名稱, ph.地址縣市別 + ph.地址鄉鎮市區 + ph.地址街道巷弄號);
            });
            Console.ReadKey();
        }
        static List<Pharmacy> Xml_Load() {
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
    }
}
