using System.Drawing;

namespace ImageProcessing
{
    /// <summary>
    /// Graphics wrapper.
    /// </summary>
    public static class Draw
    {

        /// <summary>
        /// Draws a circle on a bitmap and returns it.
        /// </summary>
        /// <param name="color">The color of the circle.</param>
        /// <param name="x">The x coordinate of the center of the circle.</param>
        /// <param name="y">The y coordinate of the center of the circle.</param>
        /// <param name="size">The size of the circle.</param>
        /// <param name="thickness">The thickness of the circle.</param>
        /// <param name="bmp">Bitmap to draw to.</param>
        /// <returns></returns>
        public static Bitmap Circle(Color color, int x, int y, int size, int thickness, Bitmap bmp)
        {
            Pen pen = new Pen(color, thickness);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawEllipse(pen, (x - size / 2), (y - size / 2), size, size);
            }
            return bmp;
        }

        /// <summary>
        /// Draws a rectangle to a bitmap and returns the result.
        /// </summary>
        /// <param name="color">The color of the rectangle.</param>
        /// <param name="rec">The rectangle to draw.</param>
        /// <param name="thickness">The thickness of the rectangle.</param>
        /// <param name="bmp">The bitmap to draw to.</param>
        /// <returns></returns>
        public static Bitmap Rectangle(Color color, Rectangle rec, int thickness, Bitmap bmp)
        {
            return Rectangle(color, rec.X, rec.Y, rec.Width, rec.Height, thickness, bmp);
        }

        /// <summary>
        /// Draws a rectangle to a bitmap and returns the result.
        /// </summary>
        /// <param name="color">The color of the rectangle.</param>
        /// <param name="x">The x coordinate of the top left corner.</param>
        /// <param name="y">The y coordinate of the top left corner.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        /// <param name="thickness">The thickness of the rectangle.</param>
        /// <param name="bmp">The bitmap to draw to.</param>
        /// <returns></returns>
        public static Bitmap Rectangle(Color color, int x, int y, int width, int height, int thickness, Bitmap bmp)
        {
            Pen pen = new Pen(color, thickness);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawRectangle(pen, new Rectangle(x, y, width, height));
            }
            return bmp;
        }

        /// <summary>
        /// Draws text to a bitmap and returns the result.
        /// </summary>
        /// <param name="str">The text to draw.</param>
        /// <param name="x">The x coordinate of the text.</param>
        /// <param name="y">The y coordinate of the text.</param>
        /// <param name="color">The color of the text.</param>
        /// <param name="fontSize">The size of the font.</param>
        /// <param name="bmp">The bitmap to draw to.</param>
        /// <returns></returns>
        public static Bitmap String(string str, int x, int y, Color color, int fontSize, Bitmap bmp)
        {
            Brush br = new SolidBrush(color);
            Font font = new Font("Arial", fontSize); // Lucida Console is also nice.
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // For higher quality use one of these:
                //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                g.DrawString(str, font, br, new Point(x, y));
            }
            return bmp;
        }

    }
}