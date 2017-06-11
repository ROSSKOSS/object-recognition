using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ObjectRecognition.Foundation.Utilities.LocalBinaryPattern
{
    public class LocalBinaryPattern
    {
        public void Calculate(object sender, DoWorkEventArgs e)
        {
            var source = (System.Drawing.Bitmap)e.Argument;
            int threshold = 0;

            var result = new List<int[,]>();
            var array = new int[3, 3];
            var bg = sender as BackgroundWorker;
            int percent = 0;
            int count = 0;
            int pace = (source.Height * source.Width) / 100;
            int separator = 0;
            for (int x = 1; x < source.Width - 1; x++)
            {
                for (int y = 1; y < source.Height - 1; y++)
                {
                    var pixel = source.GetPixel(x, y);
                    threshold = pixel.R;

                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            var currentPixel = source.GetPixel(x - 1 + j, y - 1 + i);

                            if (currentPixel.R >= threshold)
                            {
                                array[i, j] = 1;
                            }
                            else
                            {
                                array[i, j] = 0;
                            }
                        }
                    }
                    result.Add(array);
                    array = new int[3, 3];
                }
            }

            e.Result = result;
        }

        public void MakeByteArray(object sender, DoWorkEventArgs e)
        {
            var source = (List<int[,]>)e.Argument;
            var result = new List<byte>();

            foreach (var array in source)
            {
                result.Add(array.ToByte());
            }
            e.Result = result;
        }

        public void CountDistinct(object sender, DoWorkEventArgs e)
        {
            var source = (List<byte>)e.Argument;

            var amounts = new Dictionary<byte, int>();

            foreach (var number in source)
            {
                if (amounts.ContainsKey(number))
                {
                    amounts[number]++;
                }
                else
                {
                    amounts.Add(number, 1);
                }
            }
            e.Result = amounts;
        }

        public void MakeBitmap(object sender, DoWorkEventArgs e)
        {
            var source = (System.Drawing.Bitmap)e.Argument;


        }
    }

    public static class LbpHelper
    {
        public static byte ToByte(this int[,] source)
        {
            string array = String.Empty;

            array += source[1, 2];
            array += source[2, 2];
            array += source[2, 1];
            array += source[2, 0];
            array += source[1, 0];
            array += source[0, 0];
            array += source[0, 1];
            array += source[0, 2];

            return Convert.ToByte(array, 2);
        }
    }
}