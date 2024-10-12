using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Gauss
{
    public class Ellipsoid//定义地球椭球，确定基本参数
    {
        public double a;      //长半轴      
        public double f;      //扁率
        public double e2;  //第一偏心率的平方
        public double e_2;   //第二偏心率的平方
        public double M0;
        public Ellipsoid()
        {
            a = 6378137.0;
            f = 0.335281066475e-2;
            Init();
        }
        /// <summary>
        /// 椭圆的构造函数
        /// </summary>
        /// <param name="a"></param>
        /// <param name="f"></param>
        public Ellipsoid(double a, double invf)
        {
            this.a = a;
            f = 1.0 / invf;
            M0 = a * (1 - e2);
            Init();
        }

        void Init()
        {
            double b = a * (1 - f);
            e2 = (a * a - b * b) / (a*a);
            e_2 = (a * a - b * b) /(b*b);
        }

        /// <summary>
        /// 获取W参数
        /// </summary>
        /// <param name="B">
        /// <returns>W参数</returns>
        public double W(double B)
        {
            double W = Math.Sqrt(1 - e2 * Math.Pow(Math.Sin(B), 2));//第一基本纬度函数
            return W;
        }

        /// <summary>
        /// 第二基本纬度函数
        /// </summary>
        /// <param name="B">纬度（以弧度为单位）</param>
        /// <returns>eta</returns>
        public double V(double B)
        {
            double V = Math.Sqrt(1+e_2 * Math.Pow(Math.Cos(B), 2));
            return V;
        }

        /// <summary>
        /// 获取卯酉圈半径
        /// </summary>
        /// <param name="B">纬度（以弧度为单位）</param>
        // <returns></returns>        
        public double N(double B)
        {
            double W = Math.Sqrt(1 - e2 * Math.Pow(Math.Sin(B), 2));
            return a / W;
        }

        /// <summary>
        /// 计算M，子午圈曲率半径
        /// </summary>
        /// <param name="B">纬度（以弧度为单位）</param>
        /// <param name="W">W</param>
        /// <returns></returns>
        public double M(double B)
        {
            double over = a * (1 - e2);
            double w = W(B);
            double res = over / (w * w * w);
            return res;
        }
    }
}
