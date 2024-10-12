using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauss
{
    /// <summary>
    ///高斯正反算
    /// </summary>
    public class Calacute
    {
        private Ellipsoid ell;
        private double L0;               //中央子午线
        private double Y0 = 500000.0;    //Y方向平移量（以m为单位）
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ellipsoid">椭球</param>
        public Calacute(Ellipsoid ellipsoid, double midLon)
        {
            this.ell = ellipsoid;
            L0 = midLon;
        }
        /// <summary>
        /// 高斯正算
        /// </summary>
        /// <param name="Pbl">点</param>
        public void BL2xy(double B, double L, out double x, out double y)
        {
            double l = L - L0;
            double N = ell.N(B);

            double eta2 = ell.e_2 * Math.Cos(B) * Math.Cos(B);
            double eta4 = Math.Pow(eta2, 2);

            double t = Math.Tan(B);
            double t2 = t * t;
            double t4 = Math.Pow(t, 4);

            double X = Arclength(B);

            double m = l * Math.Cos(B);
            double m2 = m * m;
            double m3 = m2 * m;
            double m4 = m3 * m;
            double m5 = m4 * m;
            double m6 = m5 * m;

            x = X + N * t * (0.5 * m2 + 1 / 24.0 * (5 - t2 + 9 * eta2 + 4 * eta4) * m4
                + 1 / 720.0 * (61 - 58 * t2 + t4) * m6);
            y = N * (m + 1 / 6.0 * (1 - t2 + eta2) * m3 +
                1 / 120.0 * (5 - 18 * t2 + t4 + 14 * eta2 - 58 * eta2 * t2) * m5);

            y = y + Y0;
        }

    /// <summary>
    /// 计算高斯投影反算
    /// </summary>
    /// <param name="Pxy">点</param>
    public void xy2BL(double x, double y, out double B, out double L)
        {
            //底点维度    EndPointLat(x,eccSq,B);
            double B_f = EndPointLat(x);
            double slat = Math.Sin(B_f);
            double clat = Math.Cos(B_f);

            double N = ell.a / Math.Sqrt(1.0 - ell.e2 * slat * slat);
            double t = Math.Tan(B_f);
            double t2 = t * t;
            double t4 = t2 * t2;

            double etaSq = ell.e2 / (1 - ell.e2) * clat * clat;

            double Y1 = (y - Y0) / N;
            double Y2 = Y1 * Y1;
            double Y3 = Y2 * Y1;
            double Y4 = Y3 * Y1;
            double Y5 = Y4 * Y1;
            double Y6 = Y5 * Y1;
            double V = 1 + etaSq;

            B = B_f - 0.5 * t * V * Y2 + 1 / 24.0 * (5 + 3 * t2 + etaSq -
                9 * etaSq * t2) * V * t * Y4 - 1 / 720.0 * (61 + 90 * t2 + 45 * t4) * V * t * Y6;
            L = 1 / clat * (Y1 - 1 / 6.0 * (1 + 2 * t2 + etaSq) * Y3 +
                1 / 120.0 * (5 + 28 * t2 + 24 * t4 + 6 * etaSq + 8 * etaSq * t2) * Y5);

            L += L0;
        }

        /// <summary>
        /// 计算从赤道到投影点的子午线弧长
        /// </summary>
        /// <param name="lat"> 纬度 （以度为单位） </param>
        /// <returns></returns>
        private double Arclength(double lat)
        {
            double B = lat;
            double X;
            double a = ell.a;
            double e2 = ell.e2;

            double m0 = a * (1 - e2);
            double m2 = 3 / 2.0 * e2 * m0;
            double m4 = 5 / 4.0 * e2 * m2;
            double m6 = 7 / 6.0 * e2 * m4;
            double m8 = 9 / 8.0 * e2 * m6;

            double a0 = m0 + m2 / 2 + 3 / 8.0 * m4 + 5 / 16.0 * m6 + 35 / 128.0 * m8;
            double a2 = m2 / 2 + m4 / 2 + 15 / 32.0 * m6 + 7 / 16.0 * m8;
            double a4 = m4 / 8 + 3 / 16.0 * m6 + 7 / 32.0 * m8;
            double a6 = m6 / 32 + m8 / 16;
            double a8 = m8 / 128;

            double sin2B = Math.Sin(B * 2);
            double sin4B = Math.Sin(B * 4);
            double sin6B = Math.Sin(B * 6);
            double sin8B = Math.Sin(B * 8);

            X = a0 * B - a2/2*sin2B+a4/4*sin4B-a6/6*sin6B+a8/8*sin8B;
            return X;
        }

        /// <summary>
        /// 计算底点纬度
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private double EndPointLat(double x)
        {

            double X = x;
            double a = ell.a;
            double e2 = ell.e2;

            double m0 = a * (1 - e2);
            double m2 = 3 / 2.0 * e2 * m0;
            double m4 = 5 / 4.0 * e2 * m2;
            double m6 = 7 / 6.0 * e2 * m4;
            double m8 = 9 / 8.0 * e2 * m6;

            double a0 = m0 + m2 / 2 + 3 / 8.0 * m4 + 5 / 16.0 * m6 + 35 / 128.0 * m8;
            double a2 = m2 / 2 + m4 / 2 + 15 / 32.0 * m6 + 7 / 16.0 * m8;
            double a4 = m4 / 8 + 3 / 16.0 * m6 + 7 / 32.0 * m8;
            double a6 = m6 / 32 + m8 / 16;
            double a8 = m8 / 128;

            double delta = 0;
            double Bf = 0;
            double B0 = X / a0;
            do
            {
                double sin2B = Math.Sin(B0 * 2);
                double sin4B = Math.Sin(B0 * 4);
                double sin6B = Math.Sin(B0 * 6);
                double sin8B = Math.Sin(B0 * 8);
                delta = -a2 / 2 * sin2B + a4 / 4 * sin4B - a6 / 6 * sin6B + a8 / 8 * sin8B;
                Bf = (X - delta) / a0;
                if (Math.Abs(Bf - B0) < 1e-20)
                    break;

                B0 = Bf;
            } while (true);

            return Bf;
        }
    }
}
