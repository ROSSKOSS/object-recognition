using System;
using FileOpener;
using ObjectRecognition.Feature.DetailsWindow;
using ObjectRecognition.Feature.OrWindow;
using ObjectRecognition.Foundation.Utilities;
using ObjectRecognition.Project.MainWindow.Helpers;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ObjectRecognition.Project.MainWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Bitmap SourceBitmap { get; set; }
        private UserControl _detailsWindow;
        private UserControl _adjustmentWindow;
        private UserControl _orWindow;
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
            detailsSideMenuButton.MouseLeftButtonUp += OpenDetails;
            var adjustementsSideMenuButton = ButtonSpawner.CreateSideMenuButton(Defaults.CategoryButton.ButtonIcons.Adjustements, "Image Adjustements");
            SideMenuStackPanel.Children.Add(adjustementsSideMenuButton);

            var objectRecognitionSideMenuButton = ButtonSpawner.CreateSideMenuButton(Defaults.CategoryButton.ButtonIcons.ObjectRecognition, "Object Recognition");
            SideMenuStackPanel.Children.Add(objectRecognitionSideMenuButton);
            objectRecognitionSideMenuButton.MouseLeftButtonUp += OpenOrWindow;
        }

        private void OpenDetails(object sender, MouseButtonEventArgs e)
        {
            if (_detailsWindow != null)
            {
                foreach (var child in pageHost.Children)
                {
                    Panel.SetZIndex((UIElement)child, 0);
                }
                Panel.SetZIndex((UIElement)_detailsWindow, Int32.MaxValue);
            }
        }

        private void OpenOrWindow(object sender, MouseButtonEventArgs e)
        {
            if (_orWindow == null)
            {
                _orWindow = new ObjectRecognitionWindow(SourceBitmap);
                pageHost.Children.Add(new ObjectRecognitionWindow(SourceBitmap)
                {
                    Width = double.NaN,
                    Height = double.NaN
                });
            }
            else
            {
                foreach ( var child in pageHost.Children)
                {
                    Panel.SetZIndex((UIElement)child, 0);
                }
                Panel.SetZIndex((UIElement)_orWindow, Int32.MaxValue);
            }
           
        }

        private void OpenBitmap(object sender, MouseButtonEventArgs e)
        {
            SourceBitmap = BitmapOpener.Open();
            pageHost.Children.Clear();
            _detailsWindow = new DetailsWindow(SourceBitmap, BitmapOpener.FileName, BitmapOpener.FilePath) { Width = double.NaN, Height = double.NaN };
            pageHost.Children.Add(_detailsWindow);
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/ROSSKOSS/object-recognition");
        }
    }
}