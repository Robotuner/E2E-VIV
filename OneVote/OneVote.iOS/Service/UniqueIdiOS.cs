using OneVote.iOS.Service;
using OneVote.Models;

[assembly: Xamarin.Forms.Dependency(typeof(UniqueIdiOS))]
namespace OneVote.iOS.Service
{
    public class UniqueIdiOS : IPhoneDevice
    {
        public string GetIdentifier()
        {
            return null;
        }
    }
}