using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauss
{
    public class GeoPro
    {
        /// <summary>
        /// rad格式转dms的string
        /// </summary>
        /// <param name="rad">rad</param>
        /// <returns>str</returns>
        public static string Rad2Str(double rad)//弧度转化为角度字符串
        {
            string str = "";
            double d = rad / Math.PI * 180;
            string sign = "";
            if (d < 0)
            {
                sign = "-";
            }
            d = Math.Abs(d);
            double dd, mm, ss;
            dd = Math.Floor(d);//舍弃小数，保留整数，度。
            mm = Math.Floor((d - dd) * 60.0);//分
            ss = (d - dd - mm / 60.0) * 3600.0;//秒
            str = sign.ToString() + dd.ToString() + "°" + mm.ToString()+ "′" + ss.ToString("f4") + "″";
            return str;
        }

        /// <summary>
        /// 度分秒格式转化为弧度
        /// </summary>
        /// <param name="dms">dms</param>
        /// <returns>rad</returns>
        public static double Dms2Rad(double dms)
        {
            double rad = 0, sec = 0;
            int deg = 0, minu = 0;
     
            deg = (int)(dms + 0.0001);
            minu = (int)((dms - deg) * 100 + 0.0001);
            sec = (dms - deg - minu / 100.0) * 10000;

            rad = deg + minu / 60.0 + sec / 3600.0;
            rad = rad / 180.0 * Math.PI;
            return rad;
        }

        /// <summary>
        /// 弧度转换为度分秒
        /// </summary>
        /// <param name="rad">Rad</param>
        /// <returns>dms</returns>
        public static double Rad2Dms(double rad)
        {
            double dms = 0, sec = 0;
            int deg = 0, minu = 0;
            dms = rad / Math.PI * 180;

            deg = (int)(dms + 0.0001);
            minu = (int)((dms - deg) * 60 + 0.0001);

            sec = (dms - deg - minu / 60.0) * 3600.0;
            dms = deg + minu / 100.0 + sec / 10000.0;
            return dms;
        }   
       
    }
}
