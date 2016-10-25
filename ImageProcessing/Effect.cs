using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace ImageProcessing
{

    /// <summary>
    /// Effects that do not use kernels (convolutional matrices). Everything but cartoonify
    /// uses lockbits (unsafe code) and multiple threads for efficiency.
    /// </summary>
    public static class Effect
    {

        /// <summary>
        /// Uses lockbits and multiple threads to create and return a new thresholded image.
        /// </summary>
        /// <param name="bmp">The bitmap to threshold.</param>
        /// <returns>The thresholded image.</returns>
        public static Bitmap Threshold(Bitmap bmp)
        {
            return Threshold(bmp, 200);
        }

        /// <summary>
        /// Uses lockbits and multiple threads to create and return a new threshold image.
        /// </summary>
        /// <param name="bmp">The bitmap to threshold.</param>
        /// <param name="T">The intensity of the threshold (0 to 255). Default is 200.</param>
        /// <returns>The thresholded image.</returns>
        public static Bitmap Threshold(Bitmap bmp, int T)
        {
            int[] array = new int[1];
            array[0] = T;

            return Threshold(bmp, array);
        }

        /// <summary>
        /// Uses lockbits and multiple threads to create and return a new threshold image.
        /// </summary>
        /// <param name="bmp">The bitmap to threshold.</param>
        /// <param name="T">The values to threshold to (0 to 255).</param>
        /// <returns>The thresholded image.</returns>
        public static Bitmap Threshold(Bitmap bmp, int[] T)
        {
            unsafe
            {
                BitmapData bitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);

                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bmp.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                Parallel.For(0, heightInPixels, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        int oldBlue = currentLine[x];
                        int oldGreen = currentLine[x + 1];
                        int oldRed = currentLine[x + 2];
                        Color newColor;

                        int avg = (oldBlue + oldRed + oldGreen) / (3);

                        newColor = Color.White;
                        int rate = 255;
                        for (int i = T.Length - 1; i >= 0; i--)
                        {
                            rate -= 255 / (T.Length);
                            if (avg <= T[i]) newColor = Color.FromArgb(rate, rate, rate);
                        }

                        oldBlue = newColor.B;
                        oldGreen = newColor.G;
                        oldRed = newColor.R;

                        currentLine[x] = (byte)oldBlue;
                        currentLine[x + 1] = (byte)oldGreen;
                        currentLine[x + 2] = (byte)oldRed;
                    }
                });
                bmp.UnlockBits(bitmapData);
            }
            return bmp;
        }

        /// <summary>
        /// Uses lockbits and multiple threads to create and return a new inverted image.
        /// </summary>
        /// <param name="bmp">The image to invert.</param>
        /// <returns>The inverted image.</returns>
        public static Bitmap Invert(Bitmap bmp)
        {
            unsafe
            {
                BitmapData bitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);

                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bmp.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                Parallel.For(0, heightInPixels, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        int oldBlue = currentLine[x];
                        int oldGreen = currentLine[x + 1];
                        int oldRed = currentLine[x + 2];

                        oldBlue = 255 - oldBlue;
                        oldGreen = 255 - oldGreen;
                        oldRed = 255 - oldRed;

                        currentLine[x] = (byte)oldBlue;
                        currentLine[x + 1] = (byte)oldGreen;
                        currentLine[x + 2] = (byte)oldRed;
                    }
                });
                bmp.UnlockBits(bitmapData);
            }
            return bmp;
        }
        
        /// <summary>
        /// Turns an image to a cartoon.
        /// </summary>
        /// <param name="bitmap">The bitmap to cartoonify.</param>
        /// <returns>The cartoonified bitmap.</returns>
        public static Bitmap Cartoonify(Bitmap bitmap)
        {
            Color pixel;
            Bitmap bitmapResult = (Bitmap)bitmap.Clone();
            int r, g, b;

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    pixel = bitmap.GetPixel(x, y);

                    double ratio = 64.0;

                    r = (int)((Math.Floor(pixel.R / ratio)) * ratio);
                    g = (int)((Math.Floor(pixel.G / ratio)) * ratio);
                    b = (int)((Math.Floor(pixel.B / ratio)) * ratio);

                    pixel = Color.FromArgb(r, g, b);
                    bitmapResult.SetPixel(x, y, pixel);
                }
            }
            return bitmapResult;
        }

    }
}
