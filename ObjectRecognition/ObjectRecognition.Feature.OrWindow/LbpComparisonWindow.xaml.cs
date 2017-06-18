using LiveCharts;
using LiveCharts.Wpf;
using ObjectRecognition.Foundation.UI;
using ObjectRecognition.Foundation.Utilities.LocalBinaryPattern;
using ObjectRecognition.Foundation.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

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
        private LoadingSign _dataLoadingSign;
        private List<Bitmap> _bitmaps;
        public Func<double, string> YFormatter { get; set; }
        public LbpComparisonWindow(Bitmap sourceBitmap, List<Bitmap> bitmaps)
        {
            InitializeComponent();
            Width = Double.NaN;
            Height = Double.NaN;
            _bitmaps = bitmaps;
            SourceBitmap = (Bitmap)sourceBitmap.Clone();
            YFormatter = value => $"{value}%";
            _singleLbpLoadingSign = new LoadingSign();
            singleLbpGrid.Children.Add(_singleLbpLoadingSign);
            DataContext = this;
            _multiLbpLoadingSign = new LoadingSign();
            multiLbpGrid.Children.Add(_multiLbpLoadingSign);

            _dataLoadingSign = new LoadingSign();
            dataGrid.Children.Add(_dataLoadingSign);

            var lbpWorker = new BackgroundWorker();
            lbpWorker.WorkerSupportsCancellation = true;
            lbpWorker.DoWork += new LocalBinaryPattern().Calculate;
            lbpWorker.RunWorkerCompleted += LbpBuildFinished;
            lbpWorker.RunWorkerAsync(sourceBitmap.Clone());

            var lbpWorker2 = new BackgroundWorker();
            lbpWorker2.WorkerSupportsCancellation = true;
            lbpWorker2.DoWork += new LocalBinaryPattern().Calculate;
            lbpWorker2.RunWorkerCompleted += LbpBuildFinishedRight;
            lbpWorker2.RunWorkerAsync(_bitmaps[0].Clone());
        }

        //Acquiring Lbp patterns
        private void LbpBuildFinished(object sender, RunWorkerCompletedEventArgs e)
        {
            var result = (List<int[,]>)e.Result;
            var makeIntArray = new BackgroundWorker();
            makeIntArray.WorkerSupportsCancellation = true;
            makeIntArray.DoWork += new LocalBinaryPattern().MakeByteArray;
            makeIntArray.RunWorkerCompleted += MakeByteArrayFinished;
            makeIntArray.RunWorkerAsync(result);
            var makeArrays = new BackgroundWorker();
            makeArrays.WorkerSupportsCancellation = true;
            makeArrays.DoWork += new LocalBinaryPattern().MakeArrays;
            makeArrays.RunWorkerCompleted += MakeArraysFinished;
            makeArrays.RunWorkerAsync(result);
        }
        //Acquiring Lbp patterns
        private void LbpBuildFinishedRight(object sender, RunWorkerCompletedEventArgs e)
        {
            var result = (List<int[,]>)e.Result;
            var makeIntArray = new BackgroundWorker();
            makeIntArray.WorkerSupportsCancellation = true;
            makeIntArray.DoWork += new LocalBinaryPattern().MakeByteArray;
            makeIntArray.RunWorkerCompleted += MakeByteArrayFinishedRight;
            makeIntArray.RunWorkerAsync(result);
        }
        private void MakeArraysFinished(object sender, RunWorkerCompletedEventArgs e)
        {
            var result = (e.Result as Dictionary<int[], int>);
            var str = String.Empty;
            foreach (var i in result.Keys)
            {
                foreach (var item in i)
                {
                    str += $"{item} ";
                }
                str += $"[Value = {result[i]}]";
                listBox.Items.Add(str);
                str = String.Empty;
            }
            similarityGauge.Value = 89;
            dataGrid.Children.Remove(_dataLoadingSign);
        }

        //Converting them to bytes
        private void MakeByteArrayFinished(object sender, RunWorkerCompletedEventArgs e)
        {
            var result = (List<byte>)e.Result;
            var countWorker = new BackgroundWorker();
            countWorker.WorkerSupportsCancellation = true;
            countWorker.DoWork += new LocalBinaryPattern().CountDistinct;
            countWorker.RunWorkerCompleted += CountDistinctFinished;
            countWorker.RunWorkerAsync(result);
        }
        //Converting them to bytes
        private void MakeByteArrayFinishedRight(object sender, RunWorkerCompletedEventArgs e)
        {
            var result = (List<byte>)e.Result;
            var countWorker = new BackgroundWorker();
            countWorker.WorkerSupportsCancellation = true;
            countWorker.DoWork += new LocalBinaryPattern().CountDistinct;
            countWorker.RunWorkerCompleted += CountDistinctFinishedRight;
            countWorker.RunWorkerAsync(result);
        }
        private void MadeBitmap(object sender, RunWorkerCompletedEventArgs e)
        {
            new ImageFullscreen(e.Result as Bitmap, "Test");
        }
        //Creating histogram
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
        private void CountDistinctFinishedRight(object sender, RunWorkerCompletedEventArgs e)
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
            multiLbp.Series = seriesCollection;
            multiLbp.SeriesColors = new ColorsCollection() { System.Windows.Media.Color.FromRgb(0, 130, 255) };
            multiLbp.AxisX = new AxesCollection() { new Axis() { Title = "Brightness value", ShowLabels = true } };
            multiLbp.AxisY = new AxesCollection() { new Axis() { Title = "Amount", ShowLabels = true } };
            multiLbpGrid.Children.Remove(_multiLbpLoadingSign);
            multiLbp.UpdaterState = UpdaterState.Running;
        }

        private void UserControl_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                similarityGauge.Value-=1;
            }
            if (e.Key == Key.F2)
            {
                similarityGauge.Value+=1;
            }
        }

    }
}