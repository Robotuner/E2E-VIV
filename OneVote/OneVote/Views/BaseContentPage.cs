using Xamarin.Forms;

namespace OneVote.Views
{
    public class BaseContentPage : ContentPage
    {
        private bool IsAccessibility = true;
        public BaseContentPage() : base()
        {
            Resources["defaultDynamicStyle"] = Resources["defaultStyle"];
            Resources["microDynamicStyle"] = Resources["microStyle"];
            Resources["smallDynamicStyle"] = Resources["smallStyle"];
            Resources["mediumDynamicStyle"] = Resources["mediumStyle"];
            Resources["largeDynamicStyle"] = Resources["largeStyle"];
            Resources["bodyDynamicStyle"] = Resources["bodyStyle"];
            Resources["headerDynamicStyle"] = Resources["headerStyle"];
            Resources["titleDynamicStyle"] = Resources["titleStyle"];
            Resources["subTitleDynamicStyle"] = Resources["subTitleStyle"];
            Resources["captionDynamicStyle"] = Resources["captionStyle"];
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (IsAccessibility)
            {
                Resources["defaultDynamicStyle"] = Resources["largeStyle"];
                Resources["microDynamicStyle"] = Resources["smallStyle"];
                Resources["smallDynamicStyle"] = Resources["mediumStyle"];
                Resources["mediumDynamicStyle"] = Resources["largeStyle"];
                Resources["largeDynamicStyle"] = Resources["largeStyle"];
                Resources["bodyDynamicStyle"] = Resources["mediumStyle"];
                Resources["headerDynamicStyle"] = Resources["headerStyle"];
                Resources["titleDynamicStyle"] = Resources["titleStyle"];
                Resources["subTitleDynamicStyle"] = Resources["titleStyle"];
                Resources["captionDynamicStyle"] = Resources["defaultStyle"];
            }

        }
    }
}
