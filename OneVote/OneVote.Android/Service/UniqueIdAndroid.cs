using OneVote.Droid.Service;
using OneVote.Models;

[assembly: Xamarin.Forms.Dependency(typeof(UniqueIdAndroid))]
namespace OneVote.Droid.Service
{
    public class UniqueIdAndroid : IPhoneDevice
    {
        public string GetIdentifier()
        { 
            var ans = Android.App.Application.Context.ApplicationContext.GetSystemService(Android.Content.Context.TelephonyService);
            Android.Telephony.TelephonyManager mTelephonyMgr = ans as Android.Telephony.TelephonyManager;
            return mTelephonyMgr.Line1Number;
        }
    }
}