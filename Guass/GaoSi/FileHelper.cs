using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace Gauss
{
    public class FileHelper
    {
        public static DATA ReadFile(string filepath,string name)
        {
            DATA data = new DATA();
            try
            {
                string line;
                string[] strs;
                StreamReader sr = new StreamReader(filepath);

                line = sr.ReadLine();
                strs = line.Split(',');
                data.L0 = GeoPro.Dms2Rad(double.Parse(strs[1]));

                if (name == "正算"|| name == "正算.txt")
                {
                    POINT p;
                    while ((line = sr.ReadLine()) != null)
                    {
                        p = new POINT();
                        strs = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        p.Name = strs[0];
                        p.B = GeoPro.Dms2Rad(double.Parse(strs[1]));
                        p.L = GeoPro.Dms2Rad(double.Parse(strs[2]));
                        p.H = double.Parse(strs[3]);
                        data.Data.Add(p);
                    }
                }
                else if(name == "反算"|| name == "反算.txt")
                {
                    POINT p;
                    while ((line = sr.ReadLine()) != null)
                    {
                        p = new POINT();
                        strs = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        p.Name = strs[0];
                        p.x = double.Parse(strs[1]);
                        p.y = double.Parse(strs[2]);
                        data.Data.Add(p);
                    }
                }
                else
                {
                    MessageBox.Show("文件命名不正确");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("文件格式不正确");
            }
            return data;
        }

    }
}
