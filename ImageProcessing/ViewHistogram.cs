using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// This class is for drawing and displaying histograms.
/// </summary>

namespace ImageProcessing
{
    internal partial class ViewHistogram : Form
    {
        int[] colorBucket;
        Bitmap canvas;

        // Width and height of the PictureBox.
        int width;
        int height;

        // Point where x and y axis intersect in the bottom left corner.
        Point center;

        // Highest value of all the buckets.
        double highestValue;

        Color color = Color.Black;

        /// <summary>
        /// Creates a histogram from a color bucket.
        /// </summary>
        /// <param name="colorBucket">The values for the histogram.</param>
        /// <param name="highestValue">The highest value of any color.</param>
        /// <param name="color">The color of the values being inputted.</param>
        public ViewHistogram(int[] colorBucket, double highestValue, Color color)
        {
            InitializeComponent();

            this.colorBucket = colorBucket;
            this.highestValue = highestValue;
            this.color = color;

            width = PictureBox.Width;
            height = PictureBox.Height;

            canvas = BlankBitmap(width, height);

            SetTitle();
            DrawHistogram();
        }

        /// <summary>
        /// Sets the title of the form.
        /// </summary>
        private void SetTitle()
        {
            if (color == Color.Red) Text = "Histogram - Red";
            else if (color == Color.Green) Text = "Histogram - Green";
            else if (color == Color.Blue) Text = "Histogram - Blue";
        }

        /// <summary>
        /// Draws the histogram.
        /// </summary>
        private void DrawHistogram()
        {
            DrawAxis();
            DrawValues();
            PictureBox.Image = canvas;
            PictureBox.Update();
        }

        /// <summary>
        /// Helper method to draw the axis.
        /// </summary>
        private void DrawAxis()
        {
            using (Graphics g = Graphics.FromImage(canvas))
            {
                Pen pen = new Pen(Color.Black);
                int margin = 5;

                center = new Point(margin, 256 + margin);
                Point xAxis = Point.Add(center, new Size(256, 0));
                Point yAxis = Point.Add(center, new Size(0, -256));


                // Drawing the x-axis.
                g.DrawLine(pen, center, xAxis);

                // Drawing the y-axis.
                g.DrawLine(pen, center, yAxis);
            }
        }

        /// <summary>
        /// Helper method to draw the values.
        /// </summary>
        private void DrawValues()
        {
            int x = center.X + 1;
            int y;

            Point bottom = Point.Empty;
            Point top = Point.Empty;

            Color c = Color.Black;

            for (int i = 0; i < 256; i++)
            {
                y = (int)(colorBucket[i] / highestValue * 256);

                if (color == Color.Red) c = Color.FromArgb(i, 0, 0);
                else if (color == Color.Green) c = Color.FromArgb(0, i, 0);
                else if (color == Color.Blue) c = Color.FromArgb(0, 0, i);

                bottom = new Point(x + i, center.Y - 1);
                top = new Point(x + i, center.Y - 1 - y);

                DrawValue(bottom, top, c);
            }
        }

        /// <summary>
        /// Helper method to draw each value.
        /// </summary>
        /// <param name="bottom">The location of bottom of the value.</param>
        /// <param name="top">The location of top of the value.</param>
        private void DrawValue(Point bottom, Point top, Color c)
        {
            using (Graphics g = Graphics.FromImage(canvas))
            {
                Pen pen = new Pen(c);
                g.DrawLine(pen, bottom, top);
            }
        }

        /// <summary>
        /// Helper class that creates a blank white bitmap.
        /// </summary>
        /// <param name="width">The width of the bitmap.</param>
        /// <param name="height">The height of the bitmap.</param>
        /// <returns></returns>
        private Bitmap BlankBitmap(int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                Rectangle imageSize = new Rectangle(0, 0, width, height);
                g.FillRectangle(Brushes.White, imageSize);
            }
            return bitmap;
        }
    }
}
