using LiveCharts;
using LiveCharts.Wpf;
using ObjectRecognition.Foundation.UI;
using ObjectRecognition.Foundation.Utilities.LocalBinaryPattern;
using ObjectRecognition.Foundation.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Documents;

namespace ObjectRecognition.Feature.OrWindow
{
    /// <summary>
    /// Interaction logic for LbpComparisonWindow.xaml
    /// </summary>
    public partial class LbpComparisonWindow : UserControl
    {
        private Bitmap SourceBitmap { get; set; }
        private LoadingSign _singleLbpLoadingSign;
        private LoadingSign _multiLbpLoadingSign;

        public LbpComparisonWindow(Bitmap sourceBitmap)
        {
            InitializeComponent();
            Width = Double.NaN;
            Height = Double.NaN;
            SourceBitmap = (Bitmap)sourceBitmap.Clone();

            _singleLbpLoadingSign = new LoadingSign();
            singleLbpGrid.Children.Add(_singleLbpLoadingSign);

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

            //var bitmapWorker = new BackgroundWorker();
            //bitmapWorker.WorkerSupportsCancellation = true;
            //bitmapWorker.DoWork += new LocalBinaryPattern().MakeBitmap;
            //bitmapWorker.RunWorkerCompleted += MadeBitmap;
            //bitmapWorker.RunWorkerAsync(new List<object> {result, SourceBitmap.Clone()});
        }

        private void MadeBitmap(object sender, RunWorkerCompletedEventArgs e)
        {
            new ImageFullscreen(e.Result as Bitmap, "Test");
        }

        private void CountDistinctFinished(object sender, RunWorkerCompletedEventArgs e)
        {
            var result = (Dictionary<byte, int>)e.Result;

            var seriesCollection = new SeriesCollection
            {
                new LineSeries()
                {
                    Values = new ChartValues<int>(result.Values),
                    PointGeometry = null,
                    LineSmoothness = 0,
                    Title = "Amount:"
                }
            };
            singleLbp.Series = seriesCollection;
            singleLbp.SeriesColors = new ColorsCollection() { System.Windows.Media.Color.FromRgb(0, 130, 255) };
            singleLbp.AxisX = new AxesCollection() { new Axis() { Title = "Brightness value", ShowLabels = true } };
            singleLbp.AxisY = new AxesCollection() { new Axis() { Title = "Amount", ShowLabels = true } };
            singleLbpGrid.Children.Remove(_singleLbpLoadingSign);
            singleLbp.UpdaterState = UpdaterState.Running;
        }
    }
}