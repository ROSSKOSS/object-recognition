using ObjectRecognition.Foundation.Utilities.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ObjectRecognition.Feature.OrWindow.Worker;
using ObjectRecognition.Foundation.UI;
using ObjectRecognition.Foundation.Utilities.Bitmap;
using HorizontalAlignment = System.Windows.HorizontalAlignment;
using UserControl = System.Windows.Controls.UserControl;

namespace ObjectRecognition.Feature.OrWindow
{
    /// <summary>
    /// Interaction logic for ObjectRecognitionWindow.xaml
    /// </summary>
    public partial class ObjectRecognitionWindow : UserControl
    {
        private Bitmap SourceBitmap { get; set; }
        public ObjectRecognitionWindow(Bitmap sourceBitmap)
        {
            InitializeComponent();
            SourceBitmap = (Bitmap)sourceBitmap.Clone();
            SetUpButtons();

        }

        private void SetUpButtons()
        {
            var createHistogramButton = new Foundation.UI.Button("Create Histogram", 200, 50) { Margin = new Thickness(10, 50, 10, 10) };
            createHistogramButton.MouseLeftButtonUp += OpenCreateHistogramWindow;
            buttonGrid.Children.Add(createHistogramButton);
            createHistogramButton.VerticalAlignment = VerticalAlignment.Top;
            createHistogramButton.HorizontalAlignment = HorizontalAlignment.Center;

            var openHistogramButton = new Foundation.UI.Button("Open Histogram", 200, 50) { Margin = new Thickness(10, 10, 10, 50) };
            //openHistogramButton.MouseLeftButtonUp += LoadImages;
            buttonGrid.Children.Add(openHistogramButton);
            openHistogramButton.VerticalAlignment = VerticalAlignment.Bottom;
            openHistogramButton.HorizontalAlignment = HorizontalAlignment.Center;
        }

        private void OpenCreateHistogramWindow(object sender, MouseButtonEventArgs e)
        {
            parentGrid.Children.Clear();
            parentGrid.Children.Add(new ImageSelectionWindow((Bitmap)SourceBitmap.Clone()) {Width = Double.NaN, Height = Double.NaN});
        }

        
    }
}
