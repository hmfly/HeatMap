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
using TestHeatMap.Properties;

namespace TestHeatMap
{
    public partial class Form1 : Form
    {
        int _width;
        int _height;
        int _count;
        float _opacity;
        int _radius;
        ColorRamp _colorRamp;
        List<HeatPoint> _points;

        public Form1()
        {
            InitializeComponent();
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

        private async void make4Maps(int width, int height, int radius, List<HeatPoint> points, float opacity, ColorRamp cr)
        {
            var hmMaker = new HeatMapMaker
            {
                Width = width,
                Height = height,
                Radius = radius,
                ColorRamp = cr,
                HeatPoints = points,
                Opacity = opacity
            };
            this.pictureBox2.BackgroundImage = await hmMaker.MakeHeatMap();

            //
            this.pictureBox1.BackgroundImage = hmMaker.GrayMap;

            //
            displayInfo();
        }

        private void displayInfo()
        {
            this.lblCount.Text = String.Format("{0}:{1}", "Point Count", _count);
            this.lblRadius.Text = String.Format("{0}:{1}", "Point Radius", _radius);
            this.lblOpacity.Text = String.Format("{0}:{1}", "Map Opacity", _opacity);
        }

        private void tbHPCount_Scroll(object sender, EventArgs e)
        {
            _count = this.tbHPCount.Value;
            _points = randomPoints(_width, _height, _count);
            make4Maps(_width, _height, _radius, _points, _opacity, _colorRamp);
        }

        private void tbHPRadius_Scroll(object sender, EventArgs e)
        {
            _radius = this.tbHPRadius.Value;
            make4Maps(_width, _height, _radius, _points, _opacity, _colorRamp);
        }

        private void tbOpacity_Scroll(object sender, EventArgs e)
        {
            _opacity = this.tbOpacity.Value/10f;
            make4Maps(_width, _height, _radius, _points, _opacity, _colorRamp);
        }

        private void cmbColorRamp_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.cmbColorRamp.SelectedIndex)
            {
                case 0:
                    this.pictureBox3.BackgroundImage = Resources.i_thermal;
                    break;
                case 1:
                    this.pictureBox3.BackgroundImage = Resources.i_rainbow;
                    break;
                case 2:
                    this.pictureBox3.BackgroundImage = Resources.i_redwhiteblue;
                    break;
            }

            _colorRamp = (ColorRamp)Enum.Parse(typeof(ColorRamp), this.cmbColorRamp.SelectedItem.ToString());

            make4Maps(_width, _height, _radius, _points, _opacity, _colorRamp);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _width = this.pictureBox1.Width;
            _height = this.pictureBox1.Height;
            _opacity = this.tbOpacity.Value / 10f;
            _count = this.tbHPCount.Value;
            _radius = this.tbHPRadius.Value;
            _points = randomPoints(_width, _height, _count);
            this.cmbColorRamp.SelectedIndex = 1;
            _colorRamp = (ColorRamp)Enum.Parse(typeof(ColorRamp), this.cmbColorRamp.SelectedItem.ToString());
            make4Maps(_width, _height, _radius, _points, _opacity, _colorRamp);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://goo.gl/fwM4Y");
        }
    }
}