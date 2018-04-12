using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUS1062_Homework_SQL
{
    class Pharmacy
    {
        public string 機構狀態 { internal set; get; }
        public string 機構名稱 { set; get; }
        public string 地址縣市別 { set; get; }
        public string 地址鄉鎮市區 { set; get; }
        public string 地址街道巷弄號 { set; get; }
        public string 負責人姓名 { set; get; }
        public string 負責人性別 { set; get; }
        public string 電話 { set; get; }
        public string 是否為健保特約藥局 { set; get; }
    }

}
