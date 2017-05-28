using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectRecognition.Foundation.Utilities.Bitmap
{
    public class ColorStatistic
    {
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
    }
}
