using System.Windows.Media;

namespace ObjectRecognition.Foundation.Utilities.Converters
{
    public static class ColorConverter
    {
        public static Brush ConvertToBrush(string hex)
        {
            return (Brush)new BrushConverter().ConvertFrom(hex);
        }
    }
}
