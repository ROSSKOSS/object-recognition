using ObjectRecognition.Foundation.Utilities.Converters;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ObjectRecognition.Foundation.Windows
{
    /// <summary>
    /// Interaction logic for ImageFullscreen.xaml
    /// </summary>
    public partial class ImageFullscreen : Window
    {

        System.Windows.Point? lastCenterPositionOnTarget;
        System.Windows.Point? lastMousePositionOnTarget;
        System.Windows.Point? lastDragPoint;

        public ImageFullscreen(Bitmap source, string title)
        {
            InitializeComponent();
            image.Source = BitmapConverter.ToImageSource(source);
            Title = title;
            Style ScrollStyle = (Style)Application.Current.Resources["ScrollViewerControlTemplate"];
            ImageScrollView.Style = ScrollStyle;
        }

        private void image_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var matrix = image.LayoutTransform.Value;

            if (e.Delta > 0)
            {
                matrix.ScaleAt(1.5, 1.5, e.GetPosition(ImageScrollView).X, e.GetPosition(ImageScrollView).Y);
            }
            else
            {
                matrix.ScaleAt(1.0 / 1.5, 1.0 / 1.5, e.GetPosition(ImageScrollView).X, e.GetPosition(ImageScrollView).Y);
            }

            image.LayoutTransform = new MatrixTransform(matrix);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.F11)
            {
                if (WindowState == WindowState.Maximized)
                {
                    WindowStyle = WindowStyle.SingleBorderWindow;
                    WindowState = WindowState.Normal;
                }
                else
                {
                    WindowStyle = WindowStyle.None;
                    WindowState = WindowState.Maximized;
                }
            }
        }
    }
}
