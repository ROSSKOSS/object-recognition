using System.ComponentModel;
using System.Drawing;

namespace ObjectRecognition.Foundation.Utilities.Bitmap
{
    public class ColorStatistic
    {
        private enum Colors
        {
            Red, Green, Blue, None
        }

        public void GetRedColors(object sender, DoWorkEventArgs e)
        {
            var source = (System.Drawing.Bitmap)e.Argument;
            var amounts = new int[256];

            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    var value = source.GetPixel(x, y);
                    amounts[value.R]++;
                }
            }
            e.Result = amounts;
        }

        public void GetGreenColors(object sender, DoWorkEventArgs e)
        {
            var source = (System.Drawing.Bitmap)e.Argument;
            var amounts = new int[256];

            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    var value = source.GetPixel(x, y);
                    amounts[value.G]++;
                }
            }
            e.Result = amounts;
        }

        public void GetBlueColors(object sender, DoWorkEventArgs e)
        {
            var source = (System.Drawing.Bitmap)e.Argument;
            var amounts = new int[256];

            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    var value = source.GetPixel(x, y);
                    amounts[value.B]++;
                }
            }
            e.Result = amounts;
        }

        public void GetColorDistribution(object sender, DoWorkEventArgs e)
        {
            var source = (System.Drawing.Bitmap)e.Argument;

            var colorRgb = new int[3];
            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    var popularColor = GetPopularColor(source.GetPixel(x, y));
                    if (Colors.Red == popularColor)
                    {
                        colorRgb[0]++;
                    }
                    if (Colors.Green == popularColor)
                    {
                        colorRgb[1]++;
                    }
                    if (Colors.Blue == popularColor)
                    {
                        colorRgb[2]++;
                    }
                }
            }
            e.Result = colorRgb;
        }

        private Colors GetPopularColor(Color pixel)
        {
            if (pixel.R > (pixel.G + pixel.B))
            {
                return Colors.Red;
            }
            else if (pixel.G > (pixel.R + pixel.B))
            {
                return Colors.Green;
            }
            else if (pixel.B > (pixel.R + pixel.G))
            {
                return Colors.Blue;
            }
            else
            {
                return Colors.None;
            }
        }

        public void GrayScale(object sender, DoWorkEventArgs e)
        {
            System.Drawing.Bitmap source = ((System.Drawing.Bitmap)e.Argument);
            for (int x = 0; x < source.Width; x++)
            {
                for (int y = 0; y < source.Height; y++)
                {
                    var pixel = source.GetPixel(x, y);
                    var average = (pixel.R + pixel.G + pixel.B) / 3;
                    source.SetPixel(x, y, Color.FromArgb(average, average, average));
                }
            }
            e.Result = source;
        }
    }
}