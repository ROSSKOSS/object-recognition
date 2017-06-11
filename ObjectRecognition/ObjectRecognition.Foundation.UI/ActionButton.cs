using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using ObjectRecognition.Foundation.Utilities;

namespace ObjectRecognition.Foundation.UI
{
    public class ActionButton : Button
    {
        public ActionButton(string title, double width, double height) : base(title, width, height)
        {
            Title.Foreground = (Brush)new BrushConverter().ConvertFrom(Foundation.Utilities.Defaults.ActionButton.FontColor);
            BodyRectangle.Fill = (Brush)new BrushConverter().ConvertFrom(Foundation.Utilities.Defaults.ActionButton.Color);
        }

        protected override void Body_MouseEnter(object sender, MouseEventArgs e)
        {
            BodyRectangle.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.ActionButton.EnterColor);
        }

        protected override void Body_MouseLeave(object sender, MouseEventArgs e)
        {
            BodyRectangle.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.ActionButton.Color);
        }

        protected override void Body_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BodyRectangle.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.ActionButton.DownColor);
        }

        protected override void Body_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BodyRectangle.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.ActionButton.EnterColor);
        }
    }
}
