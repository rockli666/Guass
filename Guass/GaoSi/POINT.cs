using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauss
{
    /// <summary>
    /// 点信息
    /// </summary>
    public class POINT
    {
        public string Name;
        public double B, L, H;//大地坐标系
        public double x, y;//高斯平面直角坐标系
        public int Zone;//带号

        public override string ToString()
        {
            string line = string.Format("{0,-5}",  Name);
            line += string.Format("{0,15}{1,15}",GeoPro.Rad2Str(B),GeoPro.Rad2Str( L));
            line += string.Format("{0,15:f3} {1,15:f3}",x,y);
            return line;
        }
    }

    
}
