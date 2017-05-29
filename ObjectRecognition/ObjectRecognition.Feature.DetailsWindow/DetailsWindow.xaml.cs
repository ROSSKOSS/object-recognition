using LiveCharts;
using LiveCharts.Wpf;
using ObjectRecognition.Foundation.UI;
using ObjectRecognition.Foundation.Utilities.Bitmap;
using ObjectRecognition.Foundation.Utilities.Converters;
using ObjectRecognition.Foundation.Windows;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Controls;
using Color = System.Windows.Media.Color;

namespace ObjectRecognition.Feature.DetailsWindow
{
    /// <summary>
    /// Interaction logic for DetailsWindow.xaml
    /// </summary>
    public partial class DetailsWindow : UserControl
    {
        public Func<ChartPoint, string> PointLabel { get; set; }
        private LoadingSign loadRed, loadGreen, loadBlue, loadPie;
        private Bitmap _source;
        public DetailsWindow(Bitmap bitmapSource, string title, string path)
        {
            InitializeComponent();
            _source = bitmapSource;

            loadRed = new LoadingSign();
            loadGreen = new LoadingSign();
            loadBlue = new LoadingSign();
            loadPie = new LoadingSign();

            redChartGrid.Children.Add(loadRed);
            greenChartGrid.Children.Add(loadGreen);
            blueChartGrid.Children.Add(loadBlue);
            detailsGrid.Children.Add(loadPie);

            SetUpWrokersColors(bitmapSource);
            SetUpPieChart(bitmapSource);

            image.Source = BitmapConverter.ToImageSource(bitmapSource);
            Title.Content = title;
            pathText.Text = $"file path: [{path}]";
            descriptionBlock.Text += $"Width: {bitmapSource.Width}px\n";
            descriptionBlock.Text += $"Height: {bitmapSource.Height}px\n";
            descriptionBlock.Text += $"Pixel amount: {bitmapSource.Width * bitmapSource.Height}px\n";
        }

        #region Charts

        private void SetUpWrokersColors(Bitmap bitmap)
        {
            var redColorWorker = new BackgroundWorker();
            redColorWorker.WorkerSupportsCancellation = true;
            redColorWorker.DoWork += new ColorStatistic().GetRedColors;
            redColorWorker.RunWorkerCompleted += RedColorsCollected;
            redColorWorker.RunWorkerAsync(bitmap.Clone());

            var greenColorWorker = new BackgroundWorker();
            greenColorWorker.WorkerSupportsCancellation = true;
            greenColorWorker.DoWork += new ColorStatistic().GetGreenColors;
            greenColorWorker.RunWorkerCompleted += GreenColorsCollected;
            greenColorWorker.RunWorkerAsync(bitmap.Clone());

            var blueColorWorker = new BackgroundWorker();
            blueColorWorker.WorkerSupportsCancellation = true;
            blueColorWorker.DoWork += new ColorStatistic().GetBlueColors;
            blueColorWorker.RunWorkerCompleted += BlueColorsCollected;
            blueColorWorker.RunWorkerAsync(bitmap.Clone());
        }

        private void RedColorsCollected(object sender, RunWorkerCompletedEventArgs e)
        {
            redChartGrid.Children.Remove(loadRed);
            redChart.UpdaterState = UpdaterState.Running;
            var result = e.Result as int[];
            var seriesCollection = new SeriesCollection
            {
                new LineSeries()
                {
                   Values = new ChartValues<int>(result),
                   PointGeometry = null,
                   LineSmoothness = 0,
                   Title = "Amount:"
                }
            };

            redChart.SeriesColors = new ColorsCollection { Color.FromRgb(255, 0, 0) };

            redChart.Series = seriesCollection;
            redChart.AxisX = new AxesCollection() { new Axis() { Title = "Brightness value", ShowLabels = true } };
            redChart.AxisY = new AxesCollection() { new Axis() { Title = "Amount", ShowLabels = true } };
        }

        private void GreenColorsCollected(object sender, RunWorkerCompletedEventArgs e)
        {
            greenChartGrid.Children.Remove(loadGreen);
            greenChart.UpdaterState = UpdaterState.Running;
            var result = e.Result as int[];
            var seriesCollection = new SeriesCollection
            {
                new LineSeries()
                {
                   Values = new ChartValues<int>(result),
                   PointGeometry = null,
                   LineSmoothness = 0,
                   Title = "Amount:"
                }
            };

            greenChart.SeriesColors = new ColorsCollection { Color.FromRgb(0, 255, 0) };

            greenChart.Series = seriesCollection;
            greenChart.AxisX = new AxesCollection() { new Axis() { Title = "Brightness value", ShowLabels = true } };
            greenChart.AxisY = new AxesCollection() { new Axis() { Title = "Amount", ShowLabels = true } };
        }

        private void BlueColorsCollected(object sender, RunWorkerCompletedEventArgs e)
        {
            blueChartGrid.Children.Remove(loadBlue);
            blueChart.UpdaterState = UpdaterState.Running;
            var result = e.Result as int[];
            var seriesCollection = new SeriesCollection
            {
                new LineSeries()
                {
                   Values = new ChartValues<int>(result),
                   PointGeometry = null,
                   LineSmoothness = 0,
                   Title = "Amount:"
                }
            };

            blueChart.SeriesColors = new ColorsCollection { Color.FromRgb(0, 0, 255) };

            blueChart.Series = seriesCollection;
            blueChart.AxisX = new AxesCollection() { new Axis() { Title = "Brightness value", ShowLabels = true } };
            blueChart.AxisY = new AxesCollection() { new Axis() { Title = "Amount", ShowLabels = true } };
        }

        #endregion Charts

        private void image_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            new ImageFullscreen(_source, Title.Content.ToString()).Show();
        }

        #region PieChart

        private void SetUpPieChart(Bitmap bitmap)
        {
            var colorDistributionWorker = new BackgroundWorker();
            colorDistributionWorker.WorkerSupportsCancellation = true;
            colorDistributionWorker.DoWork += new ColorStatistic().GetColorDistribution;
            colorDistributionWorker.RunWorkerCompleted += GetColorDistributionCompleted;
            colorDistributionWorker.RunWorkerAsync(bitmap.Clone());
        }

        private void GetColorDistributionCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            detailsGrid.Children.Remove(loadPie);
            var result = e.Result as int[];
            PointLabel = chartPoint =>
               string.Format("{0:P}", chartPoint.Participation);
            var seriesCollection = new SeriesCollection
            {
                new PieSeries()
                {
                   Values = new ChartValues<int>() {result[0]},
                   LabelPoint = PointLabel,
                   DataLabels = true,
                   Title = "Red"
                },
                new PieSeries()
                {
                   Values = new ChartValues<int>() {result[1]},
                   DataLabels = true,
                   LabelPoint = PointLabel,
                   Title = "Green"
                },
                new PieSeries()
                {
                   Values = new ChartValues<int>() {result[2]},
                   DataLabels = true,
                   LabelPoint = PointLabel,
                   Title = "Blue"
                }
            };
            pieChart.SeriesColors = new ColorsCollection() { Color.FromRgb(255, 0, 0), Color.FromRgb(0, 255, 0), Color.FromRgb(0, 0, 255) };
            pieChart.Series = seriesCollection;
            pieChart.HoverPushOut = 10;
        }

        #endregion PieChart
    }
}