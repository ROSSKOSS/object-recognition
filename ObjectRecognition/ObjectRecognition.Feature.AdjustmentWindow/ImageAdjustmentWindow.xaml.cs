using ObjectRecognition.Foundation.Utilities.Converters;
using ObjectRecognition.Foundation.UI;
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

namespace ObjectRecognition.Feature.AdjustmentWindow
{
    /// <summary>
    /// Interaction logic for ImageAdjustmentWindow.xaml
    /// </summary>
    public partial class ImageAdjustmentWindow : UserControl
    {
        public ImageAdjustmentWindow(Bitmap source)
        {
            InitializeComponent();
            Bitmap imageSource = new Bitmap(source, 420, Convert.ToInt32((420 / Convert.ToDouble(source.Width)) * source.Height));
            image.Source = BitmapConverter.ToImageSource(imageSource);
            imageBorder.Height = imageSource.Height + 10;
            image.Height = imageSource.Height;
            CreateSliders();
        }

        private void CreateSliders()
        {
            var brightnessSlider = new Foundation.UI.Slider("Brightness", 50, 5) {Margin = new Thickness(5,5,5,5), Width = 400};
            adjustementPanel.Children.Add(brightnessSlider);
            var contrastSlider = new Foundation.UI.Slider("Contrast", 0, 5) { Margin = new Thickness(5, 5, 5, 5), Width = 400 };
            adjustementPanel.Children.Add(contrastSlider);
            var gammaSlider = new Foundation.UI.Slider("Gamma", 100, 5) { Margin = new Thickness(5, 5, 5, 5), Width = 400 };
            adjustementPanel.Children.Add(gammaSlider);
        }
    }
}
