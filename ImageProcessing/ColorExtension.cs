using System.Drawing;

namespace ImageProcessing
{

    /// <summary>
    /// Extends the Color object to support HSV.
    /// </summary>
    public static class ColorExtension
    {

        /// <summary>
        /// Convert to HSV color model.
        /// </summary>
        /// <param name="color">The color to convert.</param>
        /// <returns>The color as an HSV.</returns>
        public static HSV ToHSV(this Color color)
        {
            // Changing the range from 0-255 to 0-1.
            double R = color.R / 255.0;
            double G = color.G / 255.0;
            double B = color.B / 255.0;

            // Get the max and min color channels.
            double cMax = GetMax(color);
            double cMin = GetMin(color);

            double avg = cMax - cMin;

            // Calculation methods.
            double h = CalculateHue(R, G, B, cMax, avg);
            double s = CalculateSaturation(cMax, avg);
            double v = CalculateValue(cMax);

            return new HSV(h, s, v);
        }

        private static double GetMax(Color color)
        {
            double R = color.R / 255.0;
            double G = color.G / 255.0;
            double B = color.B / 255.0;

            double cMax = 0;

            if (R > cMax) cMax = R;
            if (G > cMax) cMax = G;
            if (B > cMax) cMax = B;

            return cMax;
        }

        private static double GetMin(Color color)
        {
            double R = color.R / 255.0;
            double G = color.G / 255.0;
            double B = color.B / 255.0;

            double cMin = double.PositiveInfinity;

            if (R < cMin) cMin = R;
            if (G < cMin) cMin = G;
            if (B < cMin) cMin = B;

            return cMin;
        }

        private static double CalculateHue(double R, double G, double B, double cMax, double avg)
        {
            if (avg == 0) return 0;
            if (cMax == R) return (60.0 * (((G - B) / avg) % 6.0));
            if (cMax == G) return (60.0 * (((B - R) / avg) + 2.0));
            if (cMax == B) return (60.0 * (((R - G) / avg) + 4.0));

            return double.NaN;
        }

        private static double CalculateSaturation(double cMax, double avg)
        {
            if (cMax == 0) return 0.0;
            if (cMax != 0) return avg / cMax;

            return double.NaN;
        }

        private static double CalculateValue(double cMax)
        {
            return cMax;
        }
    }
}
