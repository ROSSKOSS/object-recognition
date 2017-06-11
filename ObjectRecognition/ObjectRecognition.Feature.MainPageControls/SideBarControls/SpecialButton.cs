using System.Windows.Input;
using ObjectRecognition.Feature.MainPageControls.SideBarControls;
using System.Windows.Media;
using ObjectRecognition.Foundation.Utilities;

namespace ObjectRecognition.Foundation.UI
{
    public class SpecialButton : RoundButton
    {
        public SpecialButton(ImageSource source, double opacity) : base(source)
        {
            Opacity = opacity;
            Body.Fill = (Brush)new BrushConverter().ConvertFrom(Utilities.Defaults.SpecialButton.Color);
        }
        protected override void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Body.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.SpecialButton.DownColor);
        }

        protected override void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Body.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.SpecialButton.EnterColor);
        }

        protected override void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            Body.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.SpecialButton.EnterColor);
        }

        protected override void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            Body.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.SpecialButton.Color);
        }
    }
}