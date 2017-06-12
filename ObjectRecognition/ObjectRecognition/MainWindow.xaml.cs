using FileOpener;
using ObjectRecognition.Feature.AdjustmentWindow;
using ObjectRecognition.Feature.DetailsWindow;
using ObjectRecognition.Feature.MainPageControls.SideBarControls;
using ObjectRecognition.Feature.MainPageControls.SideMenuControls;
using ObjectRecognition.Feature.OrWindow;
using ObjectRecognition.Foundation.Utilities;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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
            var addsideBarButton = new RoundButton(new BitmapImage(new Uri("pack://application:,,,/Icons/SideBar/add.png")))
            {
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(Defaults.RoundButton.Margins.Left,
                    Defaults.RoundButton.Margins.Top,
                    Defaults.RoundButton.Margins.Right,
                    Defaults.RoundButton.Margins.Bottom)
            };
            SideBarStackPanel.Children.Add(addsideBarButton);
            addsideBarButton.HorizontalAlignment = HorizontalAlignment.Center;
            addsideBarButton.MouseLeftButtonUp += OpenBitmap;
            var helpSideBarButton = new RoundButton(new BitmapImage(new Uri("pack://application:,,,/Icons/SideBar/help.png")))
            {
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(Defaults.RoundButton.Margins.Left,
                    Defaults.RoundButton.Margins.Top,
                    Defaults.RoundButton.Margins.Right,
                    Defaults.RoundButton.Margins.Bottom)
            };
            SideBarStackPanel.Children.Add(helpSideBarButton);
            helpSideBarButton.HorizontalAlignment = HorizontalAlignment.Center;
        }

        private void CreateSideMenuButtons()
        {
            var detailsSideMenuButton = new CategoryButton(new BitmapImage(new Uri("pack://application:,,,/Icons/SideMenu/details.png")), "Image details")
            {
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(Defaults.RoundButton.Margins.Left,
                    Defaults.RoundButton.Margins.Top,
                    Defaults.RoundButton.Margins.Right,
                    Defaults.RoundButton.Margins.Bottom)
            };
            SideMenuStackPanel.Children.Add(detailsSideMenuButton);
            detailsSideMenuButton.MouseLeftButtonUp += OpenDetails;
            var adjustmentsSideMenuButton = new CategoryButton(new BitmapImage(new Uri("pack://application:,,,/Icons/SideMenu/adjustements.png")), "Image Adjustments")
            {
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(Defaults.RoundButton.Margins.Left,
                    Defaults.RoundButton.Margins.Top,
                    Defaults.RoundButton.Margins.Right,
                    Defaults.RoundButton.Margins.Bottom)
            };
            SideMenuStackPanel.Children.Add(adjustmentsSideMenuButton);
            adjustmentsSideMenuButton.MouseLeftButtonUp += OpenAdjustmentWindow;

            var objectRecognitionSideMenuButton = new CategoryButton(new BitmapImage(new Uri("pack://application:,,,/Icons/SideMenu/or.png")), "Object Recognition")
            {
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(Defaults.RoundButton.Margins.Left,
                    Defaults.RoundButton.Margins.Top,
                    Defaults.RoundButton.Margins.Right,
                    Defaults.RoundButton.Margins.Bottom)
            };
            SideMenuStackPanel.Children.Add(objectRecognitionSideMenuButton);
            objectRecognitionSideMenuButton.MouseLeftButtonUp += OpenOrWindow;
        }

        private void OpenAdjustmentWindow(object sender, MouseButtonEventArgs e)
        {
            if (_detailsWindow != null)
            {
                if (_adjustmentWindow == null)
                {
                    _adjustmentWindow = new ImageAdjustmentWindow(SourceBitmap)
                    {
                        Width = double.NaN,
                        Height = double.NaN
                    };
                    pageHost.Children.Add(_adjustmentWindow);
                }
                else
                {
                    foreach (var child in pageHost.Children)
                    {
                        Panel.SetZIndex((UIElement)child, 0);
                    }
                    Panel.SetZIndex((UIElement)_adjustmentWindow, Int32.MaxValue);
                }
            }
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
                if (_adjustmentWindow != null)
                {
                    SourceBitmap = (Bitmap)(_adjustmentWindow as ImageAdjustmentWindow).ArchivedBitmap.Clone();
                    _orWindow = new ObjectRecognitionWindow(SourceBitmap)
                    {
                        Width = double.NaN,
                        Height = double.NaN
                    };
                    pageHost.Children.Add(_orWindow);
                }
            }
            else
            {
                foreach (var child in pageHost.Children)
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
            _detailsWindow = null;
            _orWindow = null;
            _adjustmentWindow = null;
            _detailsWindow = new DetailsWindow(SourceBitmap, BitmapOpener.FileName, BitmapOpener.FilePath) { Width = double.NaN, Height = double.NaN };
            pageHost.Children.Add(_detailsWindow);
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/ROSSKOSS/object-recognition");
        }
    }
}