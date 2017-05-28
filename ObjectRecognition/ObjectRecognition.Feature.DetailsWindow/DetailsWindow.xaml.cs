using LiveCharts;
using LiveCharts.Wpf;
using ObjectRecognition.Foundation.Utilities.Bitmap;
using ObjectRecognition.Foundation.Utilities.Converters;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media;
using Color = System.Windows.Media.Color;

namespace ObjectRecognition.Feature.DetailsWindow
{
    /// <summary>
    /// Interaction logic for DetailsWindow.xaml
    /// </summary>
    public partial class DetailsWindow : UserControl
    {
        public DetailsWindow(Bitmap bitmapSource, string title)
        {
            InitializeComponent();
            SetUpWrokersColors(bitmapSource);
           //image.Source = BitmapConverter.ToImageSource(bitmapSource);
           // Title.Content = title;
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

     }
}