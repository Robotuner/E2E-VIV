using AutoMapper;
using ElectionModels;
using OneVote.Services;
using OneVote.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace OneVote.Models
{
    public class Utils
    {        
        public static IMapper CreateMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryViewModel>()
                 .ForMember(dest => dest.CategoryType, src => src.MapFrom(s => s.CategoryTypeId))
                 .ForMember(dest => dest.Sequence, src => src.MapFrom(s => s.Sequence))
                 .ForMember(dest => dest.Tickets, src => src.Ignore());
                cfg.CreateMap<CategoryViewModel, Category>();
                cfg.CreateMap<Ticket, TicketViewModel>()
                .ForMember(dest => dest.Selected, src => src.Ignore());
                cfg.CreateMap<TicketViewModel, Ticket>();
                cfg.CreateMap<Party, PartyViewModel>();
                cfg.CreateMap<PartyViewModel, Party>();

                cfg.CreateMap<CategoryViewModel, Vote>()
                    .ForMember(dest => dest.SelectionId, src => src.MapFrom(s => s.Selection.Id));

            });
            return mapperConfiguration.CreateMapper();
        }

        public static (Guid electionId, string registration, int birthYear, Guid ballotId) DisectQR(string qrtext, string ssn = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(qrtext))
                {
                    string[] s = qrtext.Split('|');
                    if (s.Length == 4)
                    {
                        string registration = s[1];
                        if (Guid.TryParse(s[0], out Guid eid))
                        {
                            if (int.TryParse(s[2], out int birthYear))
                            {
                                if (!string.IsNullOrEmpty(s[3]))
                                {
                                    string ans = ElectionModels.Misc.Utils.Decrypt(s[3], ssn);
                                    Guid bid = string.IsNullOrEmpty(ans) ? Guid.Empty : Guid.Parse(ans);
                                    return (eid, registration, birthYear, bid);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return (Guid.Empty, null, DateTime.Today.Year, Guid.Empty);
        }

        public static string GetId()
        {
            string idName = "OneVoteDeviceId";
            string OneVoteDeviceId = Preferences.Get(idName, string.Empty);
            if (string.IsNullOrEmpty(OneVoteDeviceId))
            {
                OneVoteDeviceId = System.Guid.NewGuid().ToString("n");
                Preferences.Set(idName, OneVoteDeviceId);
            }

            return OneVoteDeviceId;
        }

        /// <summary>
        /// Saves ballot id to local storage as a record of it being used
        /// </summary>
        /// <param name="ballotGuid"></param>
        /// <param name="save"></param>
        /// <returns></returns>
        public static bool BallotHasBeenSubmitted(Guid ballotGuid, bool allowUpdates = false, bool save = false)
        {
            string ballotsName = "Ballots";
            string allballots = Preferences.Get(ballotsName, string.Empty);
            if (string.IsNullOrEmpty(allballots))
            {
                if (save)
                {
                    Preferences.Set(ballotsName, ballotGuid.ToString("n"));
                }
                // true means the ballot has not been submitted from this device.
                return false;
            }

            string[] ballots = allballots.Split(',');
            List<string> lst = new List<string>(ballots);
            if (!lst.Any(n => n == ballotGuid.ToString("n")))
            {
                if (save)
                {
                    Preferences.Set(ballotsName, ballotGuid.ToString("n"));
                }
                // true means the ballot has not been submitted from this device
                return false;
            }

            return allowUpdates ? false : true;   //the ballot has already been submitted;
        }

        static void EncryptAesManaged(string raw)
        {
            try
            {
                // Create Aes that generates a new key and initialization vector (IV).    
                // Same key must be used in encryption and decryption    
                using (AesManaged aes = new AesManaged())
                {
                    // Encrypt string    
                    byte[] encrypted = Encrypt(raw, aes.Key, aes.IV);
                    // Print encrypted string    
                    Console.WriteLine($"Encrypted data: {System.Text.Encoding.UTF8.GetString(encrypted)}");
                    // Decrypt the bytes to a string.    
                    string decrypted = Decrypt(encrypted, aes.Key, aes.IV);
                    // Print decrypted string. It should be same as raw data    
                    Console.WriteLine($"Decrypted data: {decrypted}");
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            Console.ReadKey();
        }

        static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            // Create a new AesManaged.    
            using (AesManaged aes = new AesManaged())
            {
                // Create encryptor    
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                // Create MemoryStream    
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create crypto stream using the CryptoStream class. This class is the key to encryption    
                    // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream    
                    // to encrypt    
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Create StreamWriter and write data to a stream    
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            // Return encrypted data    
            return encrypted;
        }

        static string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null;
            // Create AesManaged    
            using (AesManaged aes = new AesManaged())
            {
                // Create a decryptor    
                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                // Create the streams used for decryption.    
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    // Create crypto stream    
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Read crypto stream    
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }

        private static CancellationTokenSource cts;

        public static async Task<Location> GetDeviceLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                return location;
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }

            return null;
        }
    }
}
