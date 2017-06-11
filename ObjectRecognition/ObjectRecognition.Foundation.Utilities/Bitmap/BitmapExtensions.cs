using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ObjectRecognition.Foundation.Utilities.Bitmap
{
    public static class BitmapExtensions
    {
        public static MemoryStream ToMemoryStream(this Image image, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var bitmap = (System.Drawing.Bitmap)image.Clone();
                bitmap.Save(ms, format);
                bitmap.Dispose();
                return ms;
            }
        }

        public static byte[] ToByteArray(this Image image, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var bitmap = (System.Drawing.Bitmap)image.Clone();
                var array = new MemoryStream();
                bitmap.Save(array, format);

                return new byte[10];
            }
        }
    }
}