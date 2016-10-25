using System.Drawing;

namespace ImageProcessing
{

    /// <summary>
    /// Used for manipulating color channels. This includes
    /// </summary>
    public static class Channels
    {

        /// <summary>
        /// Makes the green channel's intensity equal to the red channel for each pixel.
        /// </summary>
        /// <param name="bitmap">The bitmap to adjust.</param>
        /// <returns>The adjusted bitmap.</returns>
        public static Bitmap GreenToRed(Bitmap bitmap)
        {
            Color pixel;
            Bitmap bitmapResult = (Bitmap)bitmap.Clone();

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    pixel = bitmap.GetPixel(x, y);
                    pixel = Color.FromArgb(pixel.R, pixel.R, pixel.G);
                    bitmapResult.SetPixel(x, y, pixel);
                }
            }
            return bitmapResult;
        }

        /// <summary>
        /// Makes the blue channel's intensity equal to the red channel for each pixel.
        /// </summary>
        /// <param name="bitmap">The bitmap to adjust.</param>
        /// <returns>The adjusted bitmap.</returns>
        public static Bitmap BlueToRed(Bitmap bitmap)
        {
            Color pixel;
            Bitmap bitmapResult = (Bitmap)bitmap.Clone();

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    pixel = bitmap.GetPixel(x, y);
                    pixel = Color.FromArgb(pixel.R, pixel.G, pixel.R);
                    bitmapResult.SetPixel(x, y, pixel);
                }
            }
            return bitmapResult;
        }

        /// <summary>
        /// Makes the red channel's intensity equal to the green channel for each pixel.
        /// </summary>
        /// <param name="bitmap">The bitmap to adjust.</param>
        /// <returns>The adjusted bitmap.</returns>
        public static Bitmap RedToGreen(Bitmap bitmap)
        {
            Color pixel;
            Bitmap bitmapResult = (Bitmap)bitmap.Clone();

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    pixel = bitmap.GetPixel(x, y);
                    pixel = Color.FromArgb(pixel.G, pixel.G, pixel.B);
                    bitmapResult.SetPixel(x, y, pixel);
                }
            }
            return bitmapResult;
        }

        /// <summary>
        /// Makes the blue channel's intensity equal to the green channel for each pixel.
        /// </summary>
        /// <param name="bitmap">The bitmap to adjust.</param>
        /// <returns>The adjusted bitmap.</returns>
        public static Bitmap BlueToGreen(Bitmap bitmap)
        {
            Color pixel;
            Bitmap bitmapResult = (Bitmap)bitmap.Clone();

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    pixel = bitmap.GetPixel(x, y);
                    pixel = Color.FromArgb(pixel.R, pixel.G, pixel.G);
                    bitmapResult.SetPixel(x, y, pixel);
                }
            }
            return bitmapResult;
        }

        /// <summary>
        /// Makes the red channel's intensity equal to the blue channel for each pixel.
        /// </summary>
        /// <param name="bitmap">The bitmap to adjust.</param>
        /// <returns>The adjusted bitmap.</returns>
        public static Bitmap RedToBlue(Bitmap bitmap)
        {
            Color pixel;
            Bitmap bitmapResult = (Bitmap)bitmap.Clone();

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    pixel = bitmap.GetPixel(x, y);
                    pixel = Color.FromArgb(pixel.B, pixel.R, pixel.B);
                    bitmapResult.SetPixel(x, y, pixel);
                }
            }
            return bitmapResult;
        }

        /// <summary>
        /// Makes the green channel's intensity equal to the blue channel for each pixel.
        /// </summary>
        /// <param name="bitmap">The bitmap to adjust.</param>
        /// <returns>The adjusted bitmap.</returns>
        public static Bitmap GreenToBlue(Bitmap bitmap)
        {
            Color pixel;
            Bitmap bitmapResult = (Bitmap)bitmap.Clone();

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    pixel = bitmap.GetPixel(x, y);
                    pixel = Color.FromArgb(pixel.R, pixel.B, pixel.B);
                    bitmapResult.SetPixel(x, y, pixel);
                }
            }
            return bitmapResult;
        }

        /// <summary>
        /// Removes the red channel.
        /// </summary>
        /// <param name="bitmap">The bitmap to adjust.</param>
        /// <returns>The adjusted bitmap.</returns>
        public static Bitmap RemoveRed(Bitmap bitmap)
        {
            Color pixel;
            Bitmap bitmapResult = (Bitmap)bitmap.Clone();

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    pixel = bitmapResult.GetPixel(x, y);
                    pixel = Color.FromArgb(0, pixel.G, pixel.B);
                    bitmapResult.SetPixel(x, y, pixel);
                }
            }
            return bitmapResult;
        }

        /// <summary>
        /// Removes the green channel.
        /// </summary>
        /// <param name="bitmap">The bitmap to adjust.</param>
        /// <returns>The adjusted bitmap.</returns>
        public static Bitmap RemoveGreen(Bitmap bitmap)
        {
            Color pixel;
            Bitmap bitmapResult = (Bitmap)bitmap.Clone();

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    pixel = bitmapResult.GetPixel(x, y);
                    pixel = Color.FromArgb(pixel.R, 0, pixel.B);
                    bitmapResult.SetPixel(x, y, pixel);
                }
            }
            return bitmapResult;
        }

        /// <summary>
        /// Removes the blue channel.
        /// </summary>
        /// <param name="bitmap">The bitmap to adjust.</param>
        /// <returns>The adjusted bitmap.</returns>
        public static Bitmap RemoveBlue(Bitmap bitmap)
        {
            Color pixel;
            Bitmap bitmapResult = (Bitmap)bitmap.Clone();

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    pixel = bitmapResult.GetPixel(x, y);
                    pixel = Color.FromArgb(pixel.R, pixel.G, 0);
                    bitmapResult.SetPixel(x, y, pixel);
                }
            }
            return bitmapResult;
        }

        /// <summary>
        /// Switches the red and green channels.
        /// </summary>
        /// <param name="bitmap">The bitmap to adjust.</param>
        /// <returns>The adjusted bitmap.</returns>
        public static Bitmap SwapRedAndGreen(Bitmap bitmap)
        {
            Color pixel;
            Bitmap bitmapResult = (Bitmap)bitmap.Clone();

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    pixel = bitmap.GetPixel(x, y);
                    pixel = Color.FromArgb(pixel.G, pixel.R, pixel.B);
                    bitmapResult.SetPixel(x, y, pixel);
                }
            }
            return bitmapResult;
        }

        /// <summary>
        /// Switches the red and blue channels.
        /// </summary>
        /// <param name="bitmap">The bitmap to adjust.</param>
        /// <returns>The adjusted bitmap.</returns>
        public static Bitmap SwapRedAndBlue(Bitmap bitmap)
        {
            Color pixel;
            Bitmap bitmapResult = (Bitmap)bitmap.Clone();

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    pixel = bitmap.GetPixel(x, y);
                    pixel = Color.FromArgb(pixel.B, pixel.G, pixel.R);
                    bitmapResult.SetPixel(x, y, pixel);
                }
            }
            return bitmapResult;
        }

        /// <summary>
        /// Switches the green and blue channels.
        /// </summary>
        /// <param name="bitmap">The bitmap to adjust.</param>
        /// <returns>The adjusted bitmap.</returns>
        public static Bitmap SwapGreenAndBlue(Bitmap bitmap)
        {
            Color pixel;
            Bitmap bitmapResult = (Bitmap)bitmap.Clone();

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    pixel = bitmap.GetPixel(x, y);
                    pixel = Color.FromArgb(pixel.R, pixel.B, pixel.G);
                    bitmapResult.SetPixel(x, y, pixel);
                }
            }
            return bitmapResult;
        }

    }

}
