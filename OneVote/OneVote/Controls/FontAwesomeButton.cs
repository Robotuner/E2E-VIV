using Xamarin.Forms;

namespace OneVote.Controls
{
    public class FontAwesomeButton : Button
    {
        public static readonly string FontAwesomeName = "Font Awesome 5 Free";
        public static readonly string FontAwesomeNameAndroid = "Font Awesome 5 Free-Regular-400.otf#Font Awesome 5 Free Regular";
        public FontAwesomeButton()
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

        public FontAwesomeButton(string fontAwesomeButtonText = null)
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
            Text = fontAwesomeButtonText;
        }
    }
}
