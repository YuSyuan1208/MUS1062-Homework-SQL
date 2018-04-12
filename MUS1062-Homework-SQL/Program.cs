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
            DBHelper<Pharmacy> jackDB = new JackDBHelper();
            jackDB.ShowData(jackDB.ReadData("地址縣市別", "臺南市"));
            Console.ReadKey();
        }
       
    }
}
