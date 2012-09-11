using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace HeatMap
{
    public class HeatMapMaker
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Radius { get; set; }
        public float Opacity { get; set; }
        public ColorRamp ColorRamp { get; set; }
        public List<HeatPoint> HeatPoints { get; set; }
        public Bitmap GrayMap { get; private set; }

        public Bitmap MakeHeatMap()
        {
            var result = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb);

            this.GrayMap = this.makeGrayMap(); // at first, make a graymap

            for (int x = 0; x < this.Width; x++)
            {
                for (int y = 0; y < this.Height; y++)
                {
                    var grayVal = this.GrayMap.GetPixel(x, y);
                    var index = grayVal.A; // use the Alpha channel val as the index of colorful ramp.
                    var color = ColorUtil.GetColorInRamp(index, this.ColorRamp);
                    result.SetPixel(x, y, color);
                }
            }

            ColorUtil.AdjustOpacity(result, this.Opacity);////

            return result;
        }

        private Bitmap makeGrayMap()
        {
            var result = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb);

           using(var graphics = Graphics.FromImage(result))
           {
               foreach (var point in this.HeatPoints)
               {
                   //var r = this.Radius * (1 + point.W / 2); // Make circle radius size by Weight
                   var r = this.Radius;
                   var rect = new Rectangle((int)point.X - (int)r, (int)point.Y - (int)r, (int)r * 2, (int)r * 2);

                   var path = new GraphicsPath();
                   path.AddEllipse(rect);
                   var brush = new PathGradientBrush(path);

                   //var grayRamp = ColorUtil.GetGrayRamp(point.W);
                   var grayRamp = ColorUtil.GetGrayRamp();
                   brush.InterpolationColors = grayRamp;
                   graphics.FillEllipse(brush, rect);
               }
           }

            return result;
        }       
    }
}