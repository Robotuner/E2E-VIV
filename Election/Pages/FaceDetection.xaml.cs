using Election.ViewModels;
using ElectionModels.Misc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Election.Pages
{
    /// <summary>
    /// Interaction logic for FaceDetection.xaml
    /// </summary>
    public partial class FaceDetection : UserControl
    {
        public FaceDetection()
        {
            InitializeComponent();
        }

        private void ClearCanvas()
        {
            this.canvas.Children.Clear();
        }
        private void DrawOnImage(System.Drawing.Rectangle[] rects, Size size)
        {
            if (this.DataContext is FaceDetectionViewModel vm)
            {
                double ratio = size.Width / this.image.RenderSize.Width;
                this.canvas.Children.Clear();
                foreach (System.Drawing.Rectangle r in rects)
                {
                    DrawRect(r, ratio);
                }
            }
        }

        private void DrawDnnOnImage(List<ConfidenceRect> faces, Size size)
        {
            double ratio = size.Width / this.image.RenderSize.Width;
            if (this.DataContext is FaceDetectionViewModel vm)
            {
                this.canvas.Children.Clear();
                foreach (ConfidenceRect cr in faces)
                {
                    DrawRect(cr.AsRectangle(), ratio);
                }
            }
        }

        private void DrawRect(System.Drawing.Rectangle r, double ratio)
        {
            Debug.WriteLine($"{r.X} / {r.Y} / {r.Width} / {r.Height}");
            Line[] lines = new Line[4]
            {
                new Line(), new Line(), new Line(), new Line()
            };
            foreach (Line lne in lines)
            {
                lne.Stroke = new SolidColorBrush(Colors.Red);
                lne.Fill = new SolidColorBrush(Colors.Red);
                lne.StrokeLineJoin = System.Windows.Media.PenLineJoin.Miter;
            }

            lines[0].X1 = r.X / ratio;
            lines[0].Y1 = r.Y / ratio;
            lines[0].X2 = r.X / ratio + r.Width / ratio;
            lines[0].Y2 = r.Y / ratio;

            lines[1].X1 = lines[0].X2;
            lines[1].Y1 = lines[0].Y2;
            lines[1].X2 = lines[1].X1;
            lines[1].Y2 = lines[1].Y1 + r.Height / ratio;

            lines[2].X1 = r.X / ratio;
            lines[2].Y1 = r.Y / ratio;
            lines[2].X2 = r.X / ratio;
            lines[2].Y2 = r.Y / ratio + r.Height / ratio;

            lines[3].X1 = lines[2].X2;
            lines[3].Y1 = lines[2].Y2;
            lines[3].X2 = lines[1].X2;
            lines[3].Y2 = lines[1].Y2;

            foreach (Line lne in lines)
            {
                canvas.Children.Add(lne);
            }
        }
        
        private void On_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is FaceDetectionViewModel vm)
            {
                vm.InitOpenCVVideo();
                vm.DrawOnImage = DrawOnImage;
                vm.DrawDnnOnImage = DrawDnnOnImage;
                vm.ClearCanvas = ClearCanvas;
                vm.OnOpenFile(null);
            }
        }
    }
}
