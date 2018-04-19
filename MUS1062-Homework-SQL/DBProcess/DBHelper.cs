using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUS1062_Homework_SQL
{
    public interface DBHelper<T> where T:class
    {
        List<T> Xml_Load();
        void InsertData(T item);
        List<T> ReadData(String col_name, String name);
        void ShowData(List<T> list);
        void UpdateData(int id,T item);
        void DeleteData(int id);
    }
}
