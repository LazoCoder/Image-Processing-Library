using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;

namespace ImageProcessing
{

    /// <summary>
    /// Creates histogram from an image.
    /// </summary>
    public class Histogram
    {
        // These buckets are meant to keep a tally of the intensity of each RGB color.
        // 0-63 goes into bucket[0]
        // 64-127 goes into bucket[1]
        // 128-191 goes into bucket[2]
        // 192-255 goes into bucket[3]
        private int[] redBucket = new int[256];
        private int[] greenBucket = new int[256];
        private int[] blueBucket = new int[256];

        private int count = 0;

        Bitmap bmp;

        public Histogram(Bitmap bmp)
        {
            Create(bmp);
            this.bmp = bmp;
        }

        /// <summary>
        /// Creates a Histogram based on a Bitmap.
        /// </summary>
        /// <param name="bmp">The bitmap to create the histogram from.</param>
        private void Create(Bitmap bmp)
        {
            unsafe
            {
                BitmapData bitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);

                int bytesPerPixel = Image.GetPixelFormatSize(bmp.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                for (int y = 0; y < heightInPixels; y++)
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        int blue = currentLine[x];
                        int green = currentLine[x + 1];
                        int red = currentLine[x + 2];

                        redBucket[red]++;
                        greenBucket[green]++;
                        blueBucket[blue]++;

                        count += 3;

                    }
                }
                bmp.UnlockBits(bitmapData);
            }
        }

        /// <summary>
        /// Returns the total pixels.
        /// </summary>
        /// <returns>The total number of pixels in the histogram.</returns>
        public int GetCount()
        {
            return count;
        }

        /// <summary>
        /// Get an array of all the red values.
        /// </summary>
        /// <returns>The red values.</returns>
        public int[] GetRed()
        {
            return bucketCopy(redBucket);
        }

        /// <summary>
        /// Get an array of all the green values.
        /// </summary>
        /// <returns>The green values.</returns>
        public int[] GetGreen()
        {
            return bucketCopy(greenBucket);
        }

        /// <summary>
        /// Get an array of all the blue values.
        /// </summary>
        /// <returns>The blue values.</returns>
        public int[] GetBlue()
        {
            return bucketCopy(blueBucket);
        }

        /// <summary>
        /// Helper method that copies the contents of an array to a new array.
        /// </summary>
        /// <param name="bucket">The array to copy from.</param>
        /// <returns>The cloned array.</returns>
        private int[] bucketCopy(int[] bucket)
        {
            int[] newArray = new int[256];
            Array.Copy(bucket, newArray, 256);
            return newArray;
        }

        /// <summary>
        /// Displays the RGB values, each in a different window per color.
        /// </summary>
        public void ViewHistogram()
        {
            Thread th = new Thread(ViewHistogramForm);
            th.Start();
        }

        /// <summary>
        /// Helper method to create the forms.
        /// </summary>
        private void ViewHistogramForm()
        {
            double highestValue = GetHighestValue();

            ViewHistogram viewRed = new ViewHistogram(GetRed(), highestValue, Color.Red);
            viewRed.Show();

            ViewHistogram viewGreen = new ViewHistogram(GetGreen(), highestValue, Color.Green);
            viewGreen.Show();

            ViewHistogram viewBlue = new ViewHistogram(GetBlue(), highestValue, Color.Blue);
            viewBlue.Show();

            Application.Run();
        }

        /// <summary>
        /// Gets the highest value of all the buckets.
        /// </summary>
        /// <returns>The highest value.</returns>
        private int GetHighestValue()
        {
            int highestValue = 0;

            for (int i = 0; i < 256; i++)
            {
                if (redBucket[i] > highestValue) highestValue = redBucket[i];
                if (greenBucket[i] > highestValue) highestValue = greenBucket[i];
                if (blueBucket[i] > highestValue) highestValue = blueBucket[i];
            }

            return highestValue;
        }

        /// <summary>
        /// Used to compare two histograms.
        /// </summary>
        /// <param name="hist">The second histogram to compare to.</param>
        /// <returns>The difference between the two.</returns>
        public double CompareTo(Histogram hist)
        {
            int delta = 0;

            for (int i = 0; i < 256; i++)
            {
                delta += Math.Min(redBucket[i], hist.GetRed()[i]);
                delta += Math.Min(greenBucket[i], hist.GetGreen()[i]);
                delta += Math.Min(blueBucket[i], hist.GetBlue()[i]);
            }

            return (delta / (double)count);
        }

    }
}
