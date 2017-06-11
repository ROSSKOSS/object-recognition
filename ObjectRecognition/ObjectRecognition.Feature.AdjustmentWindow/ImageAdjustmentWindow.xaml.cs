using ImageProcessor;
using LiveCharts;
using LiveCharts.Wpf;
using ObjectRecognition.Foundation.UI;
using ObjectRecognition.Foundation.Utilities.Bitmap;
using ObjectRecognition.Foundation.Utilities.Converters;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Slider = ObjectRecognition.Foundation.UI.Slider;

namespace ObjectRecognition.Feature.AdjustmentWindow
{
    /// <summary>
    /// Interaction logic for ImageAdjustmentWindow.xaml
    /// </summary>
    public partial class ImageAdjustmentWindow : UserControl
    {
        private Bitmap _bitmap;
        public Bitmap ArchivedBitmap { get; set; }
        private SpecialButton _applyButton;
        private SpecialButton _resetButton;
        private Slider _brightnessSlider;
        private Slider _contrastSlider;
        private Slider _gammaSlider;
        private Slider _saturationSlider;
        private SeriesCollection _seriesCollection;

        public ImageAdjustmentWindow(Bitmap source)
        {
            InitializeComponent();
            Bitmap imageSource = new Bitmap(source, 420, Convert.ToInt32((420 / Convert.ToDouble(source.Width)) * source.Height));
            _bitmap = source;
            ArchivedBitmap = source;
            image.Source = BitmapConverter.ToImageSource(imageSource);
            imageBorder.Height = imageSource.Height + 10;
            image.Height = imageSource.Height;

            _seriesCollection = new SeriesCollection();
            colorChart.Series = _seriesCollection;
            colorChart.SeriesColors = new ColorsCollection {
                System.Windows.Media.Color.FromRgb(0, 130, 255) };

            CreateSliders();
            CreateButtons();
            SetUpWrokersColors();
        }

        #region InitialSetup

        private void CreateSliders()
        {
            _brightnessSlider = new Foundation.UI.Slider("Brightness", -100, 100, 0, 10) { Margin = new Thickness(5, 5, 5, 5), Width = 400 };
            adjustementPanel.Children.Add(_brightnessSlider);
            _brightnessSlider.LeftArrowButtonUp += SetBrightness;
            _brightnessSlider.RightArrowButtonUp += SetBrightness;

            _contrastSlider = new Foundation.UI.Slider("Contrast", -100, 100, 0, 10) { Margin = new Thickness(5, 5, 5, 5), Width = 400 };
            adjustementPanel.Children.Add(_contrastSlider);
            _contrastSlider.LeftArrowButtonUp += SetContrast;
            _contrastSlider.RightArrowButtonUp += SetContrast;

            _gammaSlider = new Foundation.UI.Slider("Gamma", -100, 100, 100, 10) { Margin = new Thickness(5, 5, 5, 5), Width = 400 };
            adjustementPanel.Children.Add(_gammaSlider);
            _gammaSlider.LeftArrowButtonUp += SetGamma;
            _gammaSlider.RightArrowButtonUp += SetGamma;

            _saturationSlider = new Foundation.UI.Slider("Saturation", -100, 100, 0, 10) { Margin = new Thickness(5, 5, 5, 5), Width = 400 };
            adjustementPanel.Children.Add(_saturationSlider);
            _saturationSlider.LeftArrowButtonUp += SetSaturation;
            _saturationSlider.RightArrowButtonUp += SetSaturation;
        }

        private void ResetSliders()
        {
            foreach (var adjustementPanelChild in adjustementPanel.Children)
            {
                (adjustementPanelChild as Slider).Reset();
            }
        }

        private void CreateButtons()
        {
            _applyButton = new SpecialButton(new BitmapImage(new Uri("pack://application:,,,/Icons/SpecialButtons/apply.png")), 40);
            imageDisplayGrid.Children.Add(_applyButton);
            _applyButton.HorizontalAlignment = HorizontalAlignment.Left;
            _applyButton.VerticalAlignment = VerticalAlignment.Top;
            _applyButton.Margin = new Thickness(10, 10, 0, 0);
            _applyButton.Visibility = Visibility.Hidden;
            _applyButton.MouseLeftButtonUp += Apply;

            _resetButton = new SpecialButton(new BitmapImage(new Uri("pack://application:,,,/Icons/SpecialButtons/reset.png")), 40);
            imageDisplayGrid.Children.Add(_resetButton);
            _resetButton.HorizontalAlignment = HorizontalAlignment.Left;
            _resetButton.VerticalAlignment = VerticalAlignment.Top;
            _resetButton.Visibility = Visibility.Hidden;
            _resetButton.Margin = new Thickness(60, 10, 0, 0);
            _resetButton.MouseLeftButtonUp += Reset;
        }

        #endregion InitialSetup

        private void Reset(object sender, MouseButtonEventArgs e)
        {
            image.Source = BitmapConverter.ToImageSource(new Bitmap(_bitmap, 420, Convert.ToInt32((420 / Convert.ToDouble(_bitmap.Width)) * _bitmap.Height)));
            ArchivedBitmap = new Bitmap(_bitmap);
            ResetSliders();
        }

        private void Apply(object sender, MouseButtonEventArgs e)
        {
            _bitmap = new Bitmap(ArchivedBitmap);

           // _seriesCollection.Clear();
           // colorChart.Series = _seriesCollection;
            // SetUpWrokersColors();
            //new ImageFullscreen(_bitmap, $"[{ArchivedBitmap.Width};{ArchivedBitmap.Height}]").Show();
        }

        #region Adjustments

        private void SetBrightness(object sender, MouseButtonEventArgs e)
        {
            var newBitmap = (Bitmap)_bitmap.Clone();
            using (MemoryStream inStream = new MemoryStream(newBitmap.ToMemoryStream(ImageFormat.Bmp).ToArray()))
            {
                using (MemoryStream outStream = new MemoryStream())
                {
                    using (ImageFactory imageFactory = new ImageFactory())
                    {
                        imageFactory.Load(inStream);
                        imageFactory.Brightness(Convert.ToInt32((sender as Slider).Value));
                        imageFactory.Save(outStream);
                        var bitmap = new Bitmap(outStream);
                        var displayBitmap = new Bitmap(bitmap, 420, Convert.ToInt32((420 / Convert.ToDouble(bitmap.Width)) * bitmap.Height));
                        ArchivedBitmap = new Bitmap(bitmap);
                        image.Source = BitmapConverter.ToImageSource((Bitmap)displayBitmap.Clone());
                        imageFactory.Dispose();
                    }
                    outStream.Dispose();
                }
                inStream.Dispose();
            }
        }

        private void SetGamma(object sender, MouseButtonEventArgs e)
        {
            var newBitmap = (Bitmap)_bitmap.Clone();
            using (MemoryStream inStream = new MemoryStream(newBitmap.ToMemoryStream(ImageFormat.Bmp).ToArray()))
            {
                using (MemoryStream outStream = new MemoryStream())
                {
                    using (ImageFactory imageFactory = new ImageFactory())
                    {
                        imageFactory.Load(inStream);
                        imageFactory.Gamma((float)(sender as Slider).Value / 100);
                        imageFactory.Save(outStream);
                        var bitmap = new Bitmap(outStream);
                        var displayBitmap = new Bitmap(bitmap, 420, Convert.ToInt32((420 / Convert.ToDouble(bitmap.Width)) * bitmap.Height));
                        ArchivedBitmap = new Bitmap(bitmap);

                        image.Source = BitmapConverter.ToImageSource((Bitmap)displayBitmap.Clone());
                        imageFactory.Dispose();
                    }
                    outStream.Dispose();
                }
                inStream.Dispose();
            }
        }

        private void SetContrast(object sender, MouseButtonEventArgs e)
        {
            var newBitmap = (Bitmap)_bitmap.Clone();
            using (MemoryStream inStream = new MemoryStream(newBitmap.ToMemoryStream(ImageFormat.Bmp).ToArray()))
            {
                using (MemoryStream outStream = new MemoryStream())
                {
                    using (ImageFactory imageFactory = new ImageFactory())
                    {
                        imageFactory.Load(inStream);
                        imageFactory.Contrast(Convert.ToInt32((sender as Slider).Value));
                        imageFactory.Save(outStream);
                        var bitmap = new Bitmap(outStream);
                        var displayBitmap = new Bitmap(bitmap, 420, Convert.ToInt32((420 / Convert.ToDouble(bitmap.Width)) * bitmap.Height));
                        ArchivedBitmap = new Bitmap(bitmap);

                        image.Source = BitmapConverter.ToImageSource(displayBitmap);
                        imageFactory.Dispose();
                    }
                    outStream.Dispose();
                }
                inStream.Dispose();
            }
        }

        private void SetSaturation(object sender, MouseButtonEventArgs e)
        {
            var newBitmap = (Bitmap)_bitmap.Clone();
            using (MemoryStream inStream = new MemoryStream(newBitmap.ToMemoryStream(ImageFormat.Bmp).ToArray()))
            {
                using (MemoryStream outStream = new MemoryStream())
                {
                    using (ImageFactory imageFactory = new ImageFactory())
                    {
                        imageFactory.Load(inStream);
                        imageFactory.Saturation(Convert.ToInt32((sender as Slider).Value));
                        imageFactory.Save(outStream);
                        var bitmap = new Bitmap(outStream);
                        var displayBitmap = new Bitmap(bitmap, 420, Convert.ToInt32((420 / Convert.ToDouble(bitmap.Width)) * bitmap.Height));
                        ArchivedBitmap = new Bitmap(bitmap);
                        image.Source = BitmapConverter.ToImageSource((Bitmap)displayBitmap.Clone());
                        imageFactory.Dispose();
                    }
                    outStream.Dispose();
                }
                inStream.Dispose();
            }
        }

        #endregion Adjustments

        private void imageDisplayGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            _applyButton.Visibility = Visibility.Visible;
            _resetButton.Visibility = Visibility.Visible;
        }

        private void imageDisplayGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            _applyButton.Visibility = Visibility.Hidden;
            _resetButton.Visibility = Visibility.Hidden;
        }

        private void SetUpWrokersColors()
        {
            var grayscaleWorker = new BackgroundWorker();
            grayscaleWorker.WorkerSupportsCancellation = true;
            grayscaleWorker.DoWork += new ColorStatistic().GrayScale;
            grayscaleWorker.RunWorkerCompleted += GrayscaleFinished;
            grayscaleWorker.RunWorkerAsync(_bitmap.Clone());
        }

        private void GrayscaleFinished(object sender, RunWorkerCompletedEventArgs e)
        {
            var redColorWorker = new BackgroundWorker();
            redColorWorker.WorkerSupportsCancellation = true;
            redColorWorker.DoWork += new ColorStatistic().GetRedColors;
            redColorWorker.RunWorkerCompleted += RedColorsCollected;
            redColorWorker.RunWorkerAsync(_bitmap.Clone());
        }

        private void RedColorsCollected(object sender, RunWorkerCompletedEventArgs e)
        {
            var result = e.Result as int[];
            _seriesCollection.Add(new LineSeries()
            {
                Values = new ChartValues<int>(result),
                PointGeometry = null,
                LineSmoothness = 0,
                Title = "Amount:"
            });
        }
    }
}