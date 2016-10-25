using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessing
{
    internal partial class ViewColors : Form
    {
        Bitmap bmp;

        public ViewColors()
        {
            InitializeComponent();
            bmp = BlankBitmap(360, 360);
            DrawColorRange();
            PictureBox.Image = bmp;
            PictureBox.Update();
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

        /// <summary>
        /// Draws all degrees of hue to the bitmap.
        /// </summary>
        private void DrawColorRange()
        {
            HSV colorHSV = new HSV(0, 1, 1);
            Color colorRGB;

            unsafe
            {
                BitmapData bitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);

                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bmp.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                for (int y = 0; y < heightInPixels; y++)
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        //int oldBlue = currentLine[x];
                        //int oldGreen = currentLine[x + 1];
                        //int oldRed = currentLine[x + 2];

                        colorHSV.SetHue(x / bytesPerPixel);
                        //colorHSV.SetValue(y/360.0);
                        //colorHSV.SetSaturation(y / 360.0);
                        colorRGB = colorHSV.ToRGB();
  
                        currentLine[x] = (byte)colorRGB.B;
                        currentLine[x + 1] = (byte)colorRGB.G;
                        currentLine[x + 2] = (byte)colorRGB.R;
                    }
                }
                bmp.UnlockBits(bitmapData);
            }
        }

        /// <summary>
        /// Looks under the mouse for the color and updates the bitmap.
        /// </summary>
        public void FindColor()
        {
            Bitmap screen = Screenshot();
            Bitmap temp = new Bitmap(bmp);
            Color c = screen.GetPixel(Cursor.Position.X, Cursor.Position.Y);
            HSV h;
            PictureBox.Image = bmp;
            using (Graphics g = Graphics.FromImage(temp))
            {
                h = c.ToHSV();
                g.DrawLine(new Pen(Color.Black), new Point((int)h.GetHue(), 0), new Point((int)h.GetHue(), 180));
                g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(300, 300, 40, 40));
                g.FillRectangle(new SolidBrush(h.ToRGB()), new Rectangle(302, 302, 36, 36));
            }
            //Console.WriteLine((int)h.GetHue());

            temp = Draw.String("Hue:\t\t" + h.GetHue().ToString(), 10, 300, Color.Black, 10, temp);
            temp = Draw.String("Saturation:\t" + h.GetSaturation().ToString(), 10, 315, Color.Black, 10, temp);
            temp = Draw.String("Value:\t\t" + h.GetValue().ToString(), 10, 330, Color.Black, 10, temp);

            PictureBox.Image = temp;
            PictureBox.Update();
            screen.Dispose();
            temp.Dispose();
        }

        /// <summary>
        /// Runs the FindColor() method periodically.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            FindColor();
        }

        /// <summary>
        /// Get a screenshot of the Desktop.
        /// </summary>
        /// <returns>Desktop screenshot</returns>
        public static Bitmap Screenshot()
        {
            Bitmap bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(bmpScreenshot);
            g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
            g.Dispose();
            return bmpScreenshot;
        }
    }
}
