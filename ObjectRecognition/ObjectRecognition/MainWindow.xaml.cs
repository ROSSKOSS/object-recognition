using ObjectRecognition.Foundation.Utilities;
using ObjectRecognition.Project.MainWindow.Helpers;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using FileOpener;
using ObjectRecognition.Feature.DetailsWindow;

namespace ObjectRecognition.Project.MainWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Bitmap SourceBitmap { get; set; }
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
            addsideBarButton.MouseLeftButtonUp += OpenBitmap;
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

        private void OpenBitmap(object sender, MouseButtonEventArgs e)
        {
            SourceBitmap = BitmapOpener.Open();
            pageHost.Children.Add(new DetailsWindow(SourceBitmap, BitmapOpener.FileName) { Width = double.NaN, Height = double.NaN });
        }
    }
}