using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessing
{

    /// <summary>
    /// For applying filters such as blurring, edge detection and embossing.
    /// Each method contains a kernel (convolutional matrix) to be used 
    /// on the image to produce the filter. Lockbits (unsafe) code is used
    /// for efficiency.
    /// </summary>
    public static class Filter
    {

        /// <summary>
        /// Apply a small amount of blur to an image.
        /// </summary>
        /// <param name="bitmap">The image to apply the blur to.</param>
        /// <returns>The blurred image.</returns>
        public static Bitmap BlurLow(Bitmap bitmap)
        {
            double[,] kernel =
            {
                { 0,1,0 },
                { 1,1,1 },
                { 0,1,0 }
            };
            return Convolution(bitmap, kernel, 1.0 / 5.0, 0.0);
        }

        /// <summary>
        /// Apply a medium amount of blur to an image.
        /// </summary>
        /// <param name="bitmap">The image to apply the blur to.</param>
        /// <returns>The blurred image.</returns>
        public static Bitmap BlurMedium(Bitmap bitmap)
        {
            double[,] kernel =
            {
                { 0,0,1,0,0 },
                { 0,1,1,1,0 },
                { 1,1,1,1,1 },
                { 0,1,1,1,0 },
                { 0,0,1,0,0 }
            };
            return Convolution(bitmap, kernel, 1.0 / 13.0, 0.0);
        }

        /// <summary>
        /// Apply a high amount of blur to an image.
        /// </summary>
        /// <param name="bitmap">The image to blur.</param>
        /// <returns>The blurred image.</returns>
        public static Bitmap BlurHigh(Bitmap bitmap)
        {
            double[,] kernel =
            {
                { 0,0,0,1,0,0,0 },
                { 0,0,1,1,1,0,0 },
                { 0,1,1,1,1,1,0 },
                { 1,1,1,1,1,1,1 },
                { 0,1,1,1,1,1,0 },
                { 0,0,1,1,1,0,0 },
                { 0,0,0,1,0,0,0 }
            };
            return Convolution(bitmap, kernel, 1.0 / 25.0, 0.0);
        }

        /// <summary>
        /// Apply a motion blur to an image.
        /// </summary>
        /// <param name="bitmap">The image to blur.</param>
        /// <returns>The blurred image.</returns>
        public static Bitmap MotionBlur(Bitmap bitmap)
        {
            double[,] kernel =
            {
                { 1,0,0,0,0,0,0,0,0 },
                { 0,1,0,0,0,0,0,0,0 },
                { 0,0,1,0,0,0,0,0,0 },
                { 0,0,0,1,0,0,0,0,0 },
                { 0,0,0,0,1,0,0,0,0 },
                { 0,0,0,0,0,1,0,0,0 },
                { 0,0,0,0,0,0,1,0,0 },
                { 0,0,0,0,0,0,0,1,0 },
                { 0,0,0,0,0,0,0,0,1 }
            };
            return Convolution(bitmap, kernel, 1.0 / 9.0, 0.0);
        }

        /// <summary>
        /// Apply a small amount of horizontal edge detection to an image.
        /// </summary>
        /// <param name="bitmap">The image to apply the filter to.</param>
        /// <returns>The filterd image.</returns>
        public static Bitmap HorizontalEdgesLow(Bitmap bitmap)
        {
            double[,] kernel =
            {
                {  0,0,0 },
                { -1,1,0 },
                {  0,0,0 }
            };
            return Convolution(bitmap, kernel, 1.0, 0.0);
        }

        /// <summary>
        /// Apply a large amount of hortizontal edge detection to an image.
        /// </summary>
        /// <param name="bitmap">The image to apply the filter to.</param>
        /// <returns>The filtered image.</returns>
        public static Bitmap HorizontalEdgesHigh(Bitmap bitmap)
        {
            double[,] kernel =
            {
                {  0, 0,0,0,0 },
                {  0, 0,0,0,0 },
                { -1,-1,2,0,0 },
                {  0, 0,0,0,0 },
                {  0, 0,0,0,0 }
            };
            return Convolution(bitmap, kernel, 1.0, 0.0);
        }

        /// <summary>
        /// Apply a small amount of vertical edge detection to an image.
        /// </summary>
        /// <param name="bitmap">The image to apply the filter to.</param>
        /// <returns>The filtered image.</returns>
        public static Bitmap VerticalEdgesLow(Bitmap bitmap)
        {
            double[,] kernel =
            {
                { 0,-1,0 },
                { 0, 1,0 },
                { 0, 0,0 }
            };
            return Convolution(bitmap, kernel, 1.0, 0.0);
        }

        /// <summary>
        /// Apply a large amount of vertical edge detection to an image.
        /// </summary>
        /// <param name="bitmap">The image to apply the filter to.</param>
        /// <returns>The filtered image.</returns>
        public static Bitmap VerticalEdgesHigh(Bitmap bitmap)
        {
            double[,] kernel =
            {
                { 0,0,-1,0,0 },
                { 0,0,-1,0,0 },
                { 0,0, 2,0,0 },
                { 0,0, 0,0,0 },
                { 0,0, 0,0,0 }
            };
            return Convolution(bitmap, kernel, 1.0, 0.0);
        }

        /// <summary>
        /// Apply a low amount of edge detection to an image.
        /// </summary>
        /// <param name="bitmap">The image to apply the filter to.</param>
        /// <returns>The filtered image.</returns>
        public static Bitmap EdgesLow(Bitmap bitmap)
        {
            double[,] kernel =
            {
                { -1, -1, -1 },
                { -1,  8, -1 },
                { -1, -1, -1 }
            };
            return Convolution(bitmap, kernel, 1.0, 0.0);
        }

        /// <summary>
        /// Apply a high amount of edge detection to an image.
        /// </summary>
        /// <param name="bitmap">The image to apply the filter to.</param>
        /// <returns>The filtered image.</returns>
        public static Bitmap EdgesHigh(Bitmap bitmap)
        {
            double[,] kernel =
            {
                { -1, -1, -1, -1, -1 },
                { -1, -1, -1, -1, -1 },
                { -1, -1, 24, -1, -1 },
                { -1, -1, -1, -1, -1 },
                { -1, -1, -1, -1, -1 },
            };
            return Convolution(bitmap, kernel, 1.0, 0.0);
        }

        /// <summary>
        /// Apply a sharpening effect to an image.
        /// </summary>
        /// <param name="bitmap">The image to apply the filter to.</param>
        /// <returns>The filtered image.</returns>
        public static Bitmap Sharpen(Bitmap bitmap)
        {
            double[,] kernel =
            {
                { -1, -1, -1 },
                { -1,  9, -1 },
                { -1, -1, -1 }
            };
            return Convolution(bitmap, kernel, 1.0, 0.0);
        }

        /// <summary>
        /// Apply an embossing effect to an image.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static Bitmap Emboss(Bitmap bitmap)
        {
            double[,] kernel =
            {
                { -1, -1,  0 },
                { -1,  0,  1 },
                {  0,  1,  1 }
            };
            return Convolution(bitmap, kernel, 1.0, 128.0);
        }

        /// <summary>
        /// Brighten an image.
        /// </summary>
        /// <param name="bitmap">The image to brighten.</param>
        /// <param name="level">The amount to brighten by.</param>
        /// <returns>The brightened image.</returns>
        public static Bitmap Brighten(Bitmap bitmap, int level)
        {
            if (level < 0)
                throw new ArgumentException("Level must be greater than 0.");

            double[,] kernel =
            {
                { 1 }
            };
            return Convolution(bitmap, kernel, 1.0, level);
        }

        /// <summary>
        /// Darken an image.
        /// </summary>
        /// <param name="bitmap">The image to darken.</param>
        /// <param name="level">The amount to darken by.</param>
        /// <returns>The darkened image.</returns>
        public static Bitmap Darken(Bitmap bitmap, int level)
        {
            if (level < 0)
                throw new ArgumentException("Level must be greater than 0.");

            double[,] kernel =
            {
                { 1 }
            };
            return Convolution(bitmap, kernel, 1.0, level*-1);
        }

        /// <summary>
        /// Applies the kernel to the image.
        /// </summary>
        /// <param name="bitmap">The bitmap to apply convolution to.</param>
        /// <param name="kernel">The convolutional matrix.</param>
        /// <param name="factor">The value of all the 1's in the matrix.</param>
        /// <param name="bias">Increase/decrease all the 1 values in the matrix.</param>
        /// <returns>The image with the convolution applied to it.</returns>
        private static Bitmap Convolution(Bitmap bitmap, double[,] kernel, double factor, double bias)
        {
            if (kernel.GetLength(0) != kernel.GetLength(1)) throw new Exception("Kernel must be a perfect square.");
            if (kernel.GetLength(0) % 2 == 0) throw new Exception("Kernel width and height must be odd.");

            Color pixel;
            Bitmap bitmapResult = (Bitmap)bitmap.Clone();
            double r, g, b;

            // The centre of the kernel should be (0,0).
            // If kernel is 3x3 then top left will be (-1, -1) and top right will be (1, 1).
            int range = (int)Math.Floor(kernel.GetLength(0) / 2.0);
            int start = 0 - range;
            int end = 0 + range;

            for (int bmpX = 0; bmpX < bitmap.Width; bmpX++)
            {
                for (int bmpY = 0; bmpY < bitmap.Height; bmpY++)
                {

                    r = 0.0;
                    g = 0.0;
                    b = 0.0;

                    for (int kernelX = start; kernelX <= end; kernelX++)
                    {
                        for (int kernelY = start; kernelY <= end; kernelY++)
                        {
                            if (IsInBounds(bitmap, bmpX + kernelX, bmpY + kernelY) && kernel[kernelX + range, kernelY + range] != 0)
                            {
                                r += bitmap.GetPixel(bmpX + kernelX, bmpY + kernelY).R * (kernel[kernelX + range, kernelY + range] * factor);
                                g += bitmap.GetPixel(bmpX + kernelX, bmpY + kernelY).G * (kernel[kernelX + range, kernelY + range] * factor);
                                b += bitmap.GetPixel(bmpX + kernelX, bmpY + kernelY).B * (kernel[kernelX + range, kernelY + range] * factor);
                            }
                        }
                    }

                    r += bias;
                    g += bias;
                    b += bias;

                    if (r < 0) r = 0;
                    if (g < 0) g = 0;
                    if (b < 0) b = 0;

                    if (r > 255) r = 255;
                    if (g > 255) g = 255;
                    if (b > 255) b = 255;

                    pixel = Color.FromArgb((int)r, (int)g, (int)b);

                    bitmapResult.SetPixel(bmpX, bmpY, pixel);
                }
            }


            return bitmapResult;
        }

        // This ignores the pixels on the outside (starts on 1,1 as opposed to 0,0)
        /// <summary>
        /// Similar to BlurLow method but much faster as it uses lockbits.
        /// </summary>
        /// <param name="bmp">The image to blur.</param>
        /// <returns>The blurred image.</returns>
        public static Bitmap BlurFast(Bitmap bmp)
        {
            Bitmap output = new Bitmap(bmp.Width, bmp.Height);

            unsafe
            {
                BitmapData bitmapData1 = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
                BitmapData bitmapData2 = output.LockBits(new Rectangle(0, 0, output.Width, output.Height), ImageLockMode.ReadWrite, output.PixelFormat);

                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bmp.PixelFormat) / 8;
                int heightInPixels = bitmapData1.Height;
                int widthInBytes = bitmapData1.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData1.Scan0;

                int bytesPerPixel2 = System.Drawing.Bitmap.GetPixelFormatSize(output.PixelFormat) / 8;
                int heightInPixels2 = bitmapData2.Height;
                int widthInBytes2 = bitmapData2.Width * bytesPerPixel;
                byte* PtrFirstPixel2 = (byte*)bitmapData2.Scan0;

                // For row of pixels. (start from 2nd pixel)
                for (int y = 1; y < heightInPixels - 1; y++)
                {
                    // For each pixel in that row. (start from second pixel)
                    for (int x = bytesPerPixel; x < widthInBytes - bytesPerPixel; x = x + bytesPerPixel)
                    {
                        byte* lineAbove = PtrFirstPixel + ((y - 1) * bitmapData1.Stride);
                        byte* lineCurrent = PtrFirstPixel + (y * bitmapData1.Stride);
                        byte* lineBelow = PtrFirstPixel + ((y + 1) * bitmapData1.Stride);

                        byte* lineCurrent2 = PtrFirstPixel + (y * bitmapData2.Stride);

                        int topLeftB = lineAbove[x - bytesPerPixel];
                        int topLeftG = lineAbove[x - bytesPerPixel + 1];
                        int topLeftR = lineAbove[x - bytesPerPixel + 2];

                        int topB = lineAbove[x];
                        int topG = lineAbove[x + 1];
                        int topR = lineAbove[x + 2];

                        int topRightB = lineAbove[x + bytesPerPixel];
                        int topRightG = lineAbove[x + bytesPerPixel + 1];
                        int topRightR = lineAbove[x + bytesPerPixel + 2];

                        int leftB = lineCurrent[x - bytesPerPixel];
                        int leftG = lineCurrent[x - bytesPerPixel + 1];
                        int leftR = lineCurrent[x - bytesPerPixel + 2];

                        int centerB = lineCurrent[x];
                        int centerG = lineCurrent[x + 1];
                        int centerR = lineCurrent[x + 2];

                        int rightB = lineCurrent[x + bytesPerPixel];
                        int rightG = lineCurrent[x + bytesPerPixel + 1];
                        int rightR = lineCurrent[x + bytesPerPixel + 2];

                        int bottomLeftB = lineCurrent[x - bytesPerPixel];
                        int bottomLeftG = lineCurrent[x - bytesPerPixel + 1];
                        int bottomLeftR = lineCurrent[x - bytesPerPixel + 2];

                        int bottomB = lineCurrent[x];
                        int bottomG = lineCurrent[x + 1];
                        int bottomR = lineCurrent[x + 2];

                        int bottomRightB = lineCurrent[x + bytesPerPixel];
                        int bottomRightG = lineCurrent[x + bytesPerPixel + 1];
                        int bottomRightR = lineCurrent[x + bytesPerPixel + 2];

                        int newBlue = (topLeftB + topB + topRightB + leftB + centerB + rightB + bottomLeftB + centerB + rightB) / 9;
                        int newGreen = (topLeftG + topG + topRightG + leftG + centerG + rightG + bottomLeftG + bottomG + bottomRightG) / 9;
                        int newRed = (topLeftR + topR + topRightR + leftR + centerR + rightR + bottomLeftR + bottomR + bottomRightR) / 9;

                        lineCurrent2[x] = (byte)newBlue;
                        lineCurrent2[x + 1] = (byte)newGreen;
                        lineCurrent2[x + 2] = (byte)newRed;
                    }
                }
                bmp.UnlockBits(bitmapData1);
                output.UnlockBits(bitmapData2);
            }

            return bmp;
        }

        /// <summary>
        /// Check to see if a coordinate is in the bounds of an image.
        /// </summary>
        /// <param name="bitmap">The image to check.</param>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <returns>True if in bounds.</returns>
        private static bool IsInBounds(Bitmap bitmap, int x, int y)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            if ((x >= 0 && x < bitmap.Width) &&
                (y >= 0 && y < bitmap.Height))
                return true;

            return false;
        }

        /// <summary>
        /// Used for debugging. Prints out the color channels.
        /// </summary>
        /// <param name="r">The red channel.</param>
        /// <param name="g">The green channel.</param>
        /// <param name="b">The blue channel.</param>
        private static void DebugRGB(double r, double g, double b)
        {
            Console.WriteLine("R: " + r);
            Console.WriteLine("G: " + g);
            Console.WriteLine("B: " + b);
            Console.WriteLine();
            System.Threading.Thread.Sleep(1000);
        }

    }
}
