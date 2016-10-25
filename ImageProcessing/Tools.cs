using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace ImageProcessing
{
    /// <summary>
    /// Tools for image manipulation. Includes cropping, resizing, copying and creating blank bitmaps.
    /// </summary>
    public static class Tools
    {

        /// <summary>
        /// Crop an image.
        /// </summary>
        /// <param name="bmp">The image to crop.</param>
        /// <param name="rec">The region to crop around.</param>
        /// <returns>The cropped image.</returns>
        public static Bitmap Crop(Bitmap bmp, Rectangle rec)
        {
            if (rec.Width > bmp.Width || rec.Height > bmp.Height) throw new Exception("Region cannot be larger then the image.");
            bmp = bmp.Clone(rec, bmp.PixelFormat);
            return bmp;
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap Resize(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return destImage;
        }

        /// <summary>
        /// Copies a source image into a target image at a specific location.
        /// </summary>
        /// <param name="target">The target bitmap to copy into.</param>
        /// <param name="source">The source bitmap which will be copied.</param>
        /// <param name="x">The x location of the copied bitmap.</param>
        /// <param name="y">The y location of the copied bitmap.</param>
        public static void Copy(Bitmap target, Bitmap source, int x, int y)
        {
            Graphics g = Graphics.FromImage(target);
            g.DrawImage(source, x, y);
        }

        /// <summary>
        /// Helper method that creates a blank white bitmap.
        /// </summary>
        /// <param name="width">The width of the bitmap.</param>
        /// <param name="height">The height of the bitmap.</param>
        /// <returns>A blank white image.</returns>
        public static Bitmap BlankBitmap(int width, int height)
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