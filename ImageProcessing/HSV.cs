using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace ImageProcessing
{
    /// <summary>
    /// HSV color object.
    /// The hue, h, is from 0 to 360 (where 0 and 360 are the same colour).
    /// The saturation, s, is from 0 to 1.
    /// The value, v, is from 0 to 1.
    /// </summary>
    public class HSV
    {
        private double H;
        private double S;
        private double V;

        /// <summary>
        /// Sets the hue, saturation and value.
        /// </summary>
        /// <param name="hue">The hue.</param>
        /// <param name="saturation">The saturation.</param>
        /// <param name="value">The value.</param>
        public HSV(double hue, double saturation, double value)
        {
            SetHue(hue);
            SetSaturation(saturation);
            SetValue(value);
        }

        /// <summary>
        /// Set the hue.
        /// </summary>
        /// <param name="hue">The hue is measured in degrees (0 is the same as 360).</param>
        public void SetHue(double hue)
        {
            if (hue < 0)
            {
                double positive = hue * (-1.0);
                double multiple = Math.Round(positive / 360.0);

                H = hue + (360.0 * (multiple + 1.0));
            }
            else H = hue % 360;
        }

        /// <summary>
        /// Set the saturation.
        /// </summary>
        /// <param name="saturation">The saturation is a percentage (0 to 1).</param>
        public void SetSaturation(double saturation)
        {
            if (saturation < 0) S = 0;
            else if (saturation > 1) S = 1;
            else S = saturation;
        }

        /// <summary>
        /// Set the value.
        /// </summary>
        /// <param name="value">The value is a percentage (0 to 1).</param>
        public void SetValue(double value)
        {
            if (value < 0) V = 0;
            else if (value > 1) V = 1;
            else V = value;
        }

        /// <summary>
        /// Returns the hue as a double.
        /// </summary>
        /// <returns></returns>
        public double GetHue()
        {
            return H;
        }

        /// <summary>
        /// Returns the saturation as a double.
        /// </summary>
        /// <returns></returns>
        public double GetSaturation()
        {
            return S;
        }

        /// <summary>
        /// Returns the value as a double.
        /// </summary>
        /// <returns></returns>
        public double GetValue()
        {
            return V;
        }

        /// <summary>
        /// Converts to RGB color model.
        /// </summary>
        /// <returns></returns>
        public Color ToRGB()
        {
            if (H < 0 || H > 360) throw new Exception("Hue must be between 0 and 360.");
            if (S < 0 || S > 1) throw new Exception("Saturation must be between 0 and 1.");
            if (V < 0 || V > 1) throw new Exception("Value must be between 0 and 1.");

            // These variables are necessary for the convserion.
            // Don't worry about what they mean.
            double c = V * S;
            double x = c * (1.0 - Math.Abs((H / 60.0) % 2.0 - 1.0));
            double m = V - c;

            double rPrime = 0;
            double gPrime = 0;
            double bPrime = 0;

            int r, g, b;

            switch ((int)Math.Floor(H / 60))
            {
                case 0:
                    rPrime = c;
                    gPrime = x;
                    bPrime = 0;
                    break;
                case 1:
                    rPrime = x;
                    gPrime = c;
                    bPrime = 0;
                    break;
                case 2:
                    rPrime = 0;
                    gPrime = c;
                    bPrime = x;
                    break;
                case 3:
                    rPrime = 0;
                    gPrime = x;
                    bPrime = c;
                    break;
                case 4:
                    rPrime = x;
                    gPrime = 0;
                    bPrime = c;
                    break;
                case 5:
                    rPrime = c;
                    gPrime = 0;
                    bPrime = x;
                    break;
            }

            r = (int)Math.Ceiling(((rPrime + m) * 255));
            g = (int)Math.Ceiling(((gPrime + m) * 255));
            b = (int)Math.Ceiling(((bPrime + m) * 255));

            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Creates and returns a new identical HSV.
        /// </summary>
        /// <returns>An identical HSV.</returns>
        public HSV Clone()
        {
            return new HSV(H, S, V);
        }

        /// <summary>
        /// Opens a new form with a hue detector.
        /// </summary>
        public static void ViewColors()
        {
            Thread th = new Thread(ViewColorsForm);
            th.Start();
        }

        /// <summary>
        /// Helper method to create the form.
        /// </summary>
        private static void ViewColorsForm()
        {
            ViewColors viewColors = new ViewColors();
            viewColors.Show();
            Application.Run();
        }

        public int CompareHue(HSV color)
        {
            int delta = (int)Math.Abs(H - color.GetHue());
            return (delta >= 180) ? 360 - delta : delta;
        }
    }
}
