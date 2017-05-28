using ObjectRecognition.Foundation.Utilities;
using ObjectRecognition.Foundation.Utilities.Helpers;
using System.Windows;

namespace ObjectRecognition.Foundation.MainWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CreateSideBarButtons();
            CreateSideMenuButtons();
        }

        private void CreateSideBarButtons()
        {
            var addsideBarButton = ButtonSpawner.CreateSideBarButton(Defaults.RoundButton.ButtonIcons.Add);
            SideBarStackPanel.Children.Add(addsideBarButton);
            addsideBarButton.HorizontalAlignment = HorizontalAlignment.Center;
            var helpSideBarButton = ButtonSpawner.CreateSideBarButton(Defaults.RoundButton.ButtonIcons.Help);
            SideBarStackPanel.Children.Add(helpSideBarButton);
            helpSideBarButton.HorizontalAlignment = HorizontalAlignment.Center;
        }

        private void CreateSideMenuButtons()
        {
            var detailsSideMenuButton = ButtonSpawner.CreateSideMenuButton(Defaults.CategoryButton.ButtonIcons.Details, "Image Details");
            SideMenuStackPanel.Children.Add(detailsSideMenuButton);
            var adjustementsSideMenuButton = ButtonSpawner.CreateSideMenuButton(Defaults.CategoryButton.ButtonIcons.Adjustements, "Image Adjustements");
            SideMenuStackPanel.Children.Add(adjustementsSideMenuButton);
            var objectRecognitionSideMenuButton = ButtonSpawner.CreateSideMenuButton(Defaults.CategoryButton.ButtonIcons.ObjectRecognition, "Object Recognition");
            SideMenuStackPanel.Children.Add(objectRecognitionSideMenuButton);
        }
    }
}