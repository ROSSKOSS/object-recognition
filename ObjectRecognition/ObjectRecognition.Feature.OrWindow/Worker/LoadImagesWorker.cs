using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ObjectRecognition.Foundation.UI;
using ObjectRecognition.Foundation.Utilities.Converters;
using Image = System.Drawing.Image;

namespace ObjectRecognition.Feature.OrWindow.Worker
{
    class LoadImagesWorker
    {
        public void Load(object sender, DoWorkEventArgs e)
        {
            var paths = e.Argument as List<string>;
            List<Bitmap> displays = new List<Bitmap>();
            foreach ( var path in paths)
            {
                var image = (Bitmap)Image.FromFile(path);
                Bitmap img = new Bitmap(image, new Size(200, Convert.ToInt32(200/Convert.ToDouble(image.Width)*image.Height)));
                
                displays.Add(img);
            }
            e.Result = displays;
        }
    }
}
