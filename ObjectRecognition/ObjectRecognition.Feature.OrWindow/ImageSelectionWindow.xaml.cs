using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
using ObjectRecognition.Foundation.Utilities.Converters;
using HorizontalAlignment = System.Windows.HorizontalAlignment;
using Image = System.Windows.Controls.Image;
using Label = System.Windows.Controls.Label;
using UserControl = System.Windows.Controls.UserControl;

namespace ObjectRecognition.Feature.OrWindow
{
    /// <summary>
    /// Interaction logic for ImageSelectionWindow.xaml
    /// </summary>
    public partial class ImageSelectionWindow : UserControl
    {
        private LoadingSign _loadingSign;
        public ImageSelectionWindow()
        {
            InitializeComponent();
            SetUpButtons();
        }

        private void SetUpButtons()
        {
            var selectFolderButton = new Foundation.UI.Button("Select Folder", 150, 40) { Margin = new Thickness(10, 10, 10, 10) };
            selectFolderButton.MouseLeftButtonUp += LoadImages;
            baseGrid.Children.Add(selectFolderButton);

            var buildButton = new Foundation.UI.ActionButton("Build", 80, 30) { Margin = new Thickness(0, 5, 10, 10), HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Bottom };
            Grid.SetRow(buildButton, 1);
            buildButton.MouseLeftButtonUp += BuildHistogram;
            baseGrid.Children.Add(buildButton);
        }

        private void BuildHistogram(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void LoadImages(object sender, MouseButtonEventArgs e)
        {
            List<string> files;

            using (var fbd = new FolderBrowserDialog() { SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) })
            {
                DialogResult result = fbd.ShowDialog();
                
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    files = Directory.GetFiles(fbd.SelectedPath, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".bmp") || s.EndsWith(".jpg") || s.EndsWith(".png") || s.EndsWith(".jpeg")).ToList();
                    var redColorWorker = new BackgroundWorker();
                    redColorWorker.WorkerSupportsCancellation = true;
                    redColorWorker.DoWork += new LoadImagesWorker().Load;
                    redColorWorker.RunWorkerCompleted += FilesLoaded;
                    redColorWorker.RunWorkerAsync(files);
                    statusLabel.Content = $"Current Folder: {fbd.SelectedPath}";
                    _loadingSign = new LoadingSign();
                    Grid.SetRow(_loadingSign, 1);
                    baseGrid.Children.Add(_loadingSign);
                    infoLabel.Content = null;
                    
                }
            }

        }
        private void FilesLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            baseGrid.Children.Remove(_loadingSign);
            var displays = e.Result as List<Bitmap>;
            imagePanel.Children.Clear();
            foreach (var imageDisplay in displays)
            {
                imagePanel.Children.Add(new ImageDisplay(imageDisplay, imageDisplay.Width, imageDisplay.Height) { Margin = new Thickness(5, 5, 5, 5) });
            }
            amountLabel.Content = $"Images loaded: {imagePanel.Children.Count}";
        }

    }
}
