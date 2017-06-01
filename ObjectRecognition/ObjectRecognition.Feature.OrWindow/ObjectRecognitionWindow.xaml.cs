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
using UserControl = System.Windows.Controls.UserControl;

namespace ObjectRecognition.Feature.OrWindow
{
    /// <summary>
    /// Interaction logic for ObjectRecognitionWindow.xaml
    /// </summary>
    public partial class ObjectRecognitionWindow : UserControl
    {
        public ObjectRecognitionWindow(Bitmap sourceBitmap)
        {
            InitializeComponent();
            SetUpButtons();
        }

        private void SetUpButtons()
        {
            var selectFolderButton = new Foundation.UI.Button("Select folder") { Margin = new Thickness(10, 10, 10, 10) };
            selectFolderButton.MouseLeftButtonUp += LoadImages;
            leftGrid.Children.Add(selectFolderButton);
        }

        private void LoadImages(object sender, MouseButtonEventArgs e)
        {
            List<string> files;
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    files = Directory.GetFiles(fbd.SelectedPath, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".bmp") || s.EndsWith(".jpg")|| s.EndsWith(".png")||s.EndsWith(".jpeg")).ToList(); 
                    var redColorWorker = new BackgroundWorker();
                    redColorWorker.WorkerSupportsCancellation = true;
                    redColorWorker.DoWork += new LoadImagesWorker().Load;
                    redColorWorker.RunWorkerCompleted += FilesLoaded;
                    redColorWorker.RunWorkerAsync(files);
                }
            }
            
        }

        private void FilesLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            var displays = e.Result as List<Bitmap>;
            
            foreach (var imageDisplay in displays)
            {
                displayHost.Children.Add(new ImageDisplay(" ",imageDisplay) {Margin = new Thickness(5,5,5,5)});
            }
        }
    }
}
