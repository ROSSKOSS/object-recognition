using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;
using ObjectRecognition.Foundation.Utilities.Bitmap;
using ObjectRecognition.Foundation.Utilities.LocalBinaryPattern;
using Color = System.Windows.Media.Color;

namespace ObjectRecognition.Feature.OrWindow
{
    /// <summary>
    /// Interaction logic for LbpComparisonWindow.xaml
    /// </summary>
    public partial class LbpComparisonWindow : UserControl
    {
        private Bitmap SourceBitmap { get; set; }
        public LbpComparisonWindow(Bitmap sourceBitmap)
        {
            InitializeComponent();
            Width = Double.NaN;
            Height = Double.NaN;
            SourceBitmap = (Bitmap)sourceBitmap.Clone();

            var lbpWorker = new BackgroundWorker();
            lbpWorker.WorkerSupportsCancellation = true;
            lbpWorker.DoWork += new LocalBinaryPattern().Calculate;
            lbpWorker.RunWorkerCompleted += LbpBuildFinished;
            lbpWorker.RunWorkerAsync(sourceBitmap.Clone());
        }

        private void LbpBuildFinished(object sender, RunWorkerCompletedEventArgs e)
        {
            var result = (List<int[,]>)e.Result;
            var makeIntArray = new BackgroundWorker();
            makeIntArray.WorkerSupportsCancellation = true;
            makeIntArray.DoWork += new LocalBinaryPattern().MakeByteArray;
            makeIntArray.RunWorkerCompleted += MakeByteArrayFinished;
            makeIntArray.RunWorkerAsync(result);
        }

        private void MakeByteArrayFinished(object sender, RunWorkerCompletedEventArgs e)
        {
            var result = (List<byte>)e.Result;
            var countWorker = new BackgroundWorker();
            countWorker.WorkerSupportsCancellation = true;
            countWorker.DoWork += new LocalBinaryPattern().CountDistinct;
            countWorker.RunWorkerCompleted += CountDistinctFinished;
            countWorker.RunWorkerAsync(result);
        }

        private void CountDistinctFinished(object sender, RunWorkerCompletedEventArgs e)
        {
            var result = (Dictionary<byte, int>)e.Result;
            singleLbp.UpdaterState = UpdaterState.Running;
            var lines = new LineSeries()
            {
                Values = new ChartValues<int>(result.Values),
                PointGeometry = null,
                LineSmoothness = 0,
                Title = "Amount:"
            };
            var seriesCollection = new SeriesCollection(lines);
            singleLbp.Series = seriesCollection;
            singleLbp.SeriesColors = new ColorsCollection() { Color.FromRgb(255, 0, 0) };
            singleLbp.AxisX = new AxesCollection() { new Axis() { Title = "Brightness value", ShowLabels = true } };
            singleLbp.AxisY = new AxesCollection() { new Axis() { Title = "Amount", ShowLabels = true } };
        }

        private void singleLbp_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
