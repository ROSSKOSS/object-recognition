using ObjectRecognition.Foundation.Utilities;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

        protected virtual void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Body.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.RoundButton.DownColor);
        }

        protected virtual void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Body.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.RoundButton.EnterColor);
        }

        protected virtual void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            Body.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.RoundButton.EnterColor);
        }

        protected virtual void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            Body.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.RoundButton.Color);
        }
    }
}