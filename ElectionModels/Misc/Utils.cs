using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ElectionModels.Misc
{
    public class Utils
    {
        // ngrok http https://localhost:44365 -host-header="localhost:44365";
        public static string ElectionUrl = "https://b06ace11bb0d.ngrok.io/api";

        public static void EncryptAesManaged(string raw)
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
                Debug.WriteLine(exp.Message);
            }
            //Debug.ReadKey();
        }

        public static string Encrypt(string plainText, string AesSeed)
        {
            (byte[] key, byte[] IV) = Utils.GetAesManaged(AesSeed);

            byte[] ans = Utils.Encrypt(plainText, key, IV);
            return Convert.ToBase64String(ans);
        }

        public static string Decrypt(string cipherText, string AesSeed)
        {
            if (string.IsNullOrEmpty(AesSeed))
                return null;

            (byte[] key, byte[] IV) = Utils.GetAesManaged(AesSeed);

            byte[] ans = Convert.FromBase64String(cipherText);
            return Utils.Decrypt(ans, key, IV);
        }

        static (byte[] key, byte[] IV) GetAesManaged(string AesSeed)
        {
            if (string.IsNullOrEmpty(AesSeed) || AesSeed.Length != 9)
                return (null,null);

            // create a block of 16 characters based on SSN
            string extendedssn = AesSeed + AesSeed;
            extendedssn = extendedssn.Substring(0, 16);

            byte[] key = Encoding.UTF8.GetBytes(extendedssn);
            //use the key as the initialization vector too!
            byte[] IV = key; 
            return (key, IV);
        }

        static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            // Create a new AesManaged.    
            using (AesManaged aes = new AesManaged())
            {
                aes.Key = Key;
                aes.IV = IV;
                // Create encryptor    
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
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
                aes.Key = Key;
                aes.IV = IV;
                // Create a decryptor    
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);               
                // Create the streams used for decryption.    
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    // Create crypto stream    
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Read crypto stream    
                        using (StreamReader reader = new StreamReader(cs))
                        {
                            try
                            {
                                plaintext = reader.ReadToEnd();
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(ex.Message);
                            }
                        }
                    }
                }
            }
            return plaintext;
        }

        // https://lukealderton.com/blog/posts/2016/october/xamarin-forms-working-with-threads/
        public static async Task<BlockChain> ConvertToBlockChain<T>(T obj)
        {
            BlockChain blockChain = new BlockChain();
            try
            {
                Block block = new Block(DateTime.Now, null, JsonConvert.SerializeObject(obj));
                await blockChain.AddBlock(block);
            }
            finally
            {
            }
            return blockChain;
        }
    }
}
