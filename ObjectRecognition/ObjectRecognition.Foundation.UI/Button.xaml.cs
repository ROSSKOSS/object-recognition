using System.Windows;
using ObjectRecognition.Foundation.Utilities;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ObjectRecognition.Foundation.UI
{
    /// <summary>
    /// Interaction logic for Button.xaml
    /// </summary>
    public partial class Button : UserControl
    {
        public Label TitleLabel { get; set; }
        public Button(string title, double width, double height)
        {
            InitializeComponent();
            TitleLabel = Title;
            Title.Content = title;
            Width = width;
            Height = height;
            Title.HorizontalAlignment = HorizontalAlignment.Center;
            Title.VerticalAlignment = VerticalAlignment.Center;
            Margin = new Thickness(0,0,0,0);
         }

        protected virtual void Body_MouseEnter(object sender, MouseEventArgs e)
        {
            BodyRectangle.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.CategoryButton.EnterColor);
        }

        protected virtual void Body_MouseLeave(object sender, MouseEventArgs e)
        {
            BodyRectangle.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.CategoryButton.Color);
        }

        protected virtual void Body_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BodyRectangle.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.CategoryButton.DownColor);
        }

        protected virtual void Body_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BodyRectangle.Fill = Foundation.Utilities.Converters.ColorConverter.ConvertToBrush(Defaults.CategoryButton.EnterColor);
        }
    }
}