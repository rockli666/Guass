using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Gauss
{
    public class DATA
    {
        public Ellipsoid Datum;
        public double L0;
        public List<POINT> Data;

        public DATA()
        {
            Datum = new Ellipsoid();
            Data = new List<POINT>();
        }
        /// <summary>
        /// 增加一组记录
        /// </summary>
        /// <param name="record">一次测量记录</param>
        public void Add(POINT record)
        {
            Data.Add(record);
        }

        string Title()
        {
            string line = string.Format("\r\n大地坐标(BL)<--> 平面坐标(xy)\r\n");
            line += "--------------------------------------\r\n";
            line += string.Format(" {0,15} {1,15} ", "B", "L");
            line += string.Format(" {0,12} {1,12}\r\n", "x", "y");
            return line;
        }
        public override string ToString()
        {
            string res = Title();
            foreach (var d in Data)
            {
                res += d.ToString() + "\r\n";
            }
            return res;
        }

        public DataTable ToDataTable()
        {
            DataTable table = InitTable();
            try
            {
                foreach (var d in Data)
                {
                    DataRow row = table.NewRow();
                    row["Name"] = d.Name;
                    row["B"] = GeoPro.Rad2Str(d.B);
                    row["L"] = GeoPro.Rad2Str(d.L);
                    row["x(平面)"] = $"{d.x:f3}"; 
                    row["y(平面)"] = $"{d.y:f3}";     
                    table.Rows.Add(row);
                }
            }
            catch (Exception)
            {
            
            }
            return table;
        }

        DataTable InitTable()
        {
            DataTable table = new DataTable("Coor");
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("B", typeof(string));
            table.Columns.Add("L", typeof(string));
            table.Columns.Add("x(平面)", typeof(string));
            table.Columns.Add("y(平面)", typeof(string));
            return table;
        }
    }
}
