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
        public Button(string title)
        {
            InitializeComponent();
            TitleLabel = Title;
            Title.Content = title;
            Width = 100;
            Height = 50;
            Title.HorizontalAlignment = HorizontalAlignment.Center;
            Margin = new Thickness(0,0,0,0);
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