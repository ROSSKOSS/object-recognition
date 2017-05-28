using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ObjectRecognition.Foundation.UI
{
    /// <summary>
    /// Interaction logic for LoadingSign.xaml
    /// </summary>
    public partial class LoadingSign : UserControl
    {
        public LoadingSign()
        {
            InitializeComponent();
            var da = new DoubleAnimation(0, 360, new Duration(TimeSpan.FromMilliseconds(1000)));
            var rt = new RotateTransform();
            loadingBody.RenderTransform = rt;
            da.RepeatBehavior = RepeatBehavior.Forever;
            rt.BeginAnimation(RotateTransform.AngleProperty, da);
        }
    }
}
