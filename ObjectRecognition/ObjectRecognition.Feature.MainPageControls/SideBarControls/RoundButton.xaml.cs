using ObjectRecognition.Foundation.Utilities.Converters;
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

namespace ObjectRecognition.Feature.MainPageControls.SideBarControls
{
    /// <summary>
    /// Interaction logic for RoundButton.xaml
    /// </summary>
    public partial class RoundButton : UserControl
    {
       public RoundButton(ImageSource iconSource)
        {
            InitializeComponent();
            Icon.Source = iconSource;
        }
        
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Body.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.RoundButton.DownColor);
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Body.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.RoundButton.EnterColor);
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            Body.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.RoundButton.EnterColor);
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            Body.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.RoundButton.Color);
        }
    }
}
