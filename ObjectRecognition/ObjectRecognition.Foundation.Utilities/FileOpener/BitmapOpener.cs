using Microsoft.Win32;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;

namespace FileOpener
{
    public class BitmapOpener
    {
        public static string FileName { get; set; }
        public static string FilePath { get; set; }
        public static MemoryStream FileStream { get; set; }
        public static Bitmap Open()
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == true)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            Bitmap bmp = (Bitmap)Image.FromFile(openFileDialog1.FileName);
                            FileName = openFileDialog1.SafeFileName;
                            FilePath = openFileDialog1.FileName;
                            //BitmapImage bmp = new BitmapImage();
                            return bmp;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Can't open the file!", ex.Source);
                }
            }
            return null;
        }
    }
}