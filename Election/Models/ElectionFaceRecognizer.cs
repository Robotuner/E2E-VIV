using Emgu.CV.Face;

namespace Election.Models
{
    public class ElectionFaceRecognizer : BasicFaceRecognizer
    {
        public ElectionFaceRecognizer(string path) : base()
        {
            Read(path);
        }
    }
}
