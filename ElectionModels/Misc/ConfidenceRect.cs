using System.Drawing;

namespace ElectionModels.Misc
{
    public class ConfidenceRect
    {
        public double Confidence { get; set; }
        public double X1 { get; set; }
        public double Y1 { get; set; }
        public double X2 { get; set; }
        public double Y2 { get; set; }

        public ConfidenceRect(object confidence, object x1, object y1, object x2, object y2, Size imageSize)
        {
            Confidence = ConvertToDouble(confidence);
            X1 = ConvertToDouble(x1) * imageSize.Width;
            Y1 = ConvertToDouble(y1) * imageSize.Height;
            X2 = ConvertToDouble(x2) * imageSize.Width;
            Y2 = ConvertToDouble(y2) * imageSize.Height;
        }

        public Rectangle AsRectangle()
        {
            int width = (int)(X2 - X1);
            int height = (int)(Y2 - Y1);
           
            Rectangle r = new Rectangle((int)X1, (int)Y1, width, height);
            return r;
        }

        private double ConvertToDouble(object val)
        {
            if (double.TryParse(val.ToString(), out double result))
            {
                return result;
            }

            return 0.0;
        }
    }
}
