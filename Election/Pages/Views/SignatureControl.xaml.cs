using Election.ViewModels.Views;
using ElectionModels.Misc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Election.Pages.Views
{
    /// <summary>
    /// Interaction logic for SignatureControl.xaml
    /// </summary>
    public partial class SignatureControl : UserControl
    {
        public SignatureControl()
        {
            InitializeComponent();
        }

        private void ClearCanvas()
        {
            this.canvas.Children.Clear();
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

        private void DrawOnImage(System.Drawing.Rectangle[] rects, System.Drawing.Size size)
        {
            if (this.DataContext is SignatureViewModel vm)
            {
                double ratio = size.Width / this.image.RenderSize.Width;
                this.canvas.Children.Clear();
                foreach (System.Drawing.Rectangle r in rects)
                {
                    DrawRect(r, ratio);
                }
            }
        }

        private void DrawDnnOnImage(List<ConfidenceRect> faces, System.Drawing.Size size)
        {
            double ratio = size.Width / this.image.RenderSize.Width;
            if (this.DataContext is SignatureViewModel vm)
            {
                this.canvas.Children.Clear();
                foreach (ConfidenceRect cr in faces)
                {
                    DrawRect(cr.AsRectangle(), ratio);
                }
            }
        }

        private void SignatureControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is SignatureViewModel vm)
            {
                vm.DrawOnImage = this.DrawOnImage;
                vm.DrawDnnOnImage = this.DrawDnnOnImage;
                vm.ClearCanvas = this.ClearCanvas;
            }
        }
    }
}
