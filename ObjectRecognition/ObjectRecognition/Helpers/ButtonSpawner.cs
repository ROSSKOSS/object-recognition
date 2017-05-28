using System;
using System.Drawing;
using System.Windows;
using ObjectRecognition.Feature.MainPageControls.SideBarControls;
using ObjectRecognition.Feature.MainPageControls.SideMenuControls;
using ObjectRecognition.Foundation.Utilities;
using ObjectRecognition.Foundation.Utilities.Converters;

namespace ObjectRecognition.Project.MainWindow.Helpers
{
    class ButtonSpawner
    {
        public static RoundButton CreateSideBarButton(string iconName)
        {
            try
            {
                var path = String.Concat(System.AppDomain.CurrentDomain.BaseDirectory, $"SideBarButtons\\{iconName}.png");
                var bitmap = Image.FromFile(path);
                return new RoundButton(BitmapConverter.ToImageSource((Bitmap)bitmap))
                {
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(Defaults.RoundButton.Margins.Left,
                    Defaults.RoundButton.Margins.Top,
                    Defaults.RoundButton.Margins.Right,
                    Defaults.RoundButton.Margins.Bottom)
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured", ex.ToString());
                return null;
            }
        }
        public static CategoryButton CreateSideMenuButton(string iconName, string title)
        {
            try
            {
                var path = String.Concat(System.AppDomain.CurrentDomain.BaseDirectory, $"SideMenuButtons\\{iconName}.png");
                var bitmap = Image.FromFile(path);
                return new CategoryButton(BitmapConverter.ToImageSource((Bitmap)bitmap), title)
                {
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(Defaults.CategoryButton.Margins.Left,
                    Defaults.CategoryButton.Margins.Top,
                    Defaults.CategoryButton.Margins.Right,
                    Defaults.CategoryButton.Margins.Bottom)
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured", ex.ToString());
                return null;
            }
        }
    }
}
