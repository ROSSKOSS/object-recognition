using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ObjectRecognition.Foundation.Utilities.Converters;

namespace ObjectRecognition.Foundation.UI
{
    /// <summary>
    /// Interaction logic for ImageDisplay.xaml
    /// </summary>
    public partial class ImageDisplay : UserControl
    {
        public bool DeletedFlag;
        public ImageDisplay(Bitmap source, double width, double height)
        {
            InitializeComponent();
            image.Source = BitmapConverter.ToImageSource(source);
            Width = width;
            Height = height;
            DeletedFlag = false;
        }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (DeletedFlag)
            {
                deletedMarker.Fill = null;
                DeletedFlag = false;
            }
            else
            {
                deletedMarker.Fill = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FFFF0000");
                DeletedFlag = true;
            }

        }
    }
}
