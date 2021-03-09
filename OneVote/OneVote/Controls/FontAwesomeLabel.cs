using Xamarin.Forms;

namespace OneVote.Controls
{
    public class FontAwesomeLabel : Label
    {
        public static readonly string FontAwesomeName = "Font Awesome 5 Free";
        public static readonly string FontAwesomeNameAndroid = "Font Awesome 5 Free-Regular-400.otf#Font Awesome 5 Free Regular";
        public FontAwesomeLabel()
        {
            FontFamily = FontAwesomeName;
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    FontFamily = FontAwesomeNameAndroid;
                    break;
                default:
                    FontFamily = FontAwesomeName;
                    break;
            }
        }

        public FontAwesomeLabel(string fontAwesomeLabelText = null)
        {
            FontFamily = FontAwesomeName;
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    FontFamily = FontAwesomeNameAndroid;
                    break;
                default:
                    FontFamily = FontAwesomeName;
                    break;
            }
            Text = fontAwesomeLabelText;
        }
    }
}
