using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using HeatMap;

namespace TestHeatMap
{
    public partial class Form1 : Form
    {
        int _width;
        int _height;
        int _count;
        float _opacity;
        int _radius;
        List<HeatPoint> _points;

        public Form1()
        {
            InitializeComponent();

            _width = this.pictureBox1.Width;
            _height = this.pictureBox1.Height;

            _opacity = this.tbOpacity.Value = 10;
            _count = this.tbHPCount.Value = 1;
            _radius = this.tbHPRadius.Value = 25;
            _points = randomPoints(_width, _height, _count);
            make4Maps(_width, _height, _radius, _points, _opacity);


            //---
            //var points = PointDataReader.Read(@"C:\Users\敏\Desktop\heatdata_single.txt");
            //make4Maps(width, height, 25, points, 1f);

            //---
            //var timer = new Timer();
            //timer.Tick +=
            //    (s, e) =>
            //    {
            //        var points = randomPoints(width, height, 200);
            //        make4Maps(width, height, 50, points, 1f);
            //    };
            //timer.Start();

            //---
            //var points = randomPoints(_width, _height, 15);
            //make4Maps(_width, _height, 50, points, 1f);
        }

        List<HeatPoint> randomPoints(int width, int height, int count)
        {
            var result = new List<HeatPoint>();

            var r = new Random();
            for (int i = 0; i < count; i++)
            {
                var x = r.Next(width);
                var y = r.Next(height);
                //var w = (float)r.NextDouble()/2;

                var point = new HeatPoint
                {
                    X = x,
                    Y = y,
                    W = 1
                };

                result.Add(point);
            }

            return result;
        }

        private void make4Maps(int width, int height, int radius, List<HeatPoint> points, float opacity)
        {
            var hmMaker = new HeatMapMaker
            {
                Width = width,
                Height = height,
                Radius = radius,
                ColorRamp = ColorRamp.RAINBOW,
                HeatPoints = points,
                Opacity = opacity
            };
            this.pictureBox2.BackgroundImage = hmMaker.MakeHeatMap();

            //
            this.pictureBox1.BackgroundImage = hmMaker.GrayMap;

            //
            hmMaker.ColorRamp = ColorRamp.RED_WHITE_BLUE;
            this.pictureBox3.BackgroundImage = hmMaker.MakeHeatMap();

            //
            hmMaker.ColorRamp = ColorRamp.THERMAL;
            this.pictureBox4.BackgroundImage = hmMaker.MakeHeatMap();

            //
            displayInfo();
        }

        private void displayInfo()
        {
            this.lblCount.Text = String.Format("{0}:{1}", "Point Count:", _count);
            this.lblRadius.Text = String.Format("{0}:{1}", "Point Radius:", _radius);
            this.lblOpacity.Text = String.Format("{0}:{1}", "Map Opacity:", _opacity);
        }

        private void tbHPCount_Scroll(object sender, EventArgs e)
        {
            _count = this.tbHPCount.Value;
            _points = randomPoints(_width, _height, _count);
            make4Maps(_width, _height, _radius, _points, _opacity);
        }

        private void tbHPRadius_Scroll(object sender, EventArgs e)
        {
            _radius = this.tbHPRadius.Value;
            make4Maps(_width, _height, _radius, _points, _opacity);
        }

        private void tbOpacity_Scroll(object sender, EventArgs e)
        {
            _opacity = this.tbOpacity.Value/10f;
            make4Maps(_width, _height, _radius, _points, _opacity);
        }

        private void btnRandomPoints_Click(object sender, EventArgs e)
        {
            _points = randomPoints(_width, _height, _count);
            make4Maps(_width, _height, _radius, _points, _opacity);
        }


    }
}
