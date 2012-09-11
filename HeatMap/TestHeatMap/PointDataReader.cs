using HeatMap;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHeatMap
{
    static class PointDataReader
    {
        public static List<HeatPoint> Read(string filePath)
        {
            var result = new List<HeatPoint>();

            using (var sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    var vals = sr.ReadLine().Split(',');
                    var point = new HeatPoint();
                    point.X = float.Parse(vals[0]);
                    point.Y = float.Parse(vals[1]);
                    point.W = float.Parse(vals[2]);

                    result.Add(point);
                }
            }

            return result;
        }
    }
}
