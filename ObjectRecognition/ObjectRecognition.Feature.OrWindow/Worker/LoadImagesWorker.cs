using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectRecognition.Foundation.UI;

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
                Bitmap img = new Bitmap(image, new Size(148, 90));
                displays.Add(img);
            }
            e.Result = displays;
        }
    }
}
