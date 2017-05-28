using System;
using System.Collections.Generic;
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
using ObjectRecognition.Foundation.Utilities;

namespace ObjectRecognition.Feature.MainPageControls.SideMenuControls
{
    /// <summary>
    /// Interaction logic for CategoryButton.xaml
    /// </summary>
    public partial class CategoryButton : UserControl
    {
        public CategoryButton(ImageSource iconSource, string title)
        {
            InitializeComponent();
            Icon.Source = iconSource;
            Title.Content = title;
        }

        private void Body_MouseEnter(object sender, MouseEventArgs e)
        {
            BodyRectangle.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.CategoryButton.EnterColor);
        }

        private void Body_MouseLeave(object sender, MouseEventArgs e)
        {
            BodyRectangle.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.CategoryButton.Color);
        }

        private void Body_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BodyRectangle.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.CategoryButton.DownColor);
        }

        private void Body_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BodyRectangle.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.CategoryButton.EnterColor);
        }
    }
}
