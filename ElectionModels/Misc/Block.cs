using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ElectionModels.Misc
{
    // https://www.c-sharpcorner.com/article/blockchain-basics-building-a-blockchain-in-net-core/
    public class Block : INotifyPropertyChanged
    {
        public int Index { get; set; }
        public DateTime TimeStamp { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public string Data { get; set; }

        // this value is specific to an election, is just a random number.
        private int nonce;
        public int Nonce
        {
            get { return nonce; }
            set
            {
                if (nonce != value)
                {
                    nonce = value;
                    OnPropertyChanged("Nonce");
                }
            }
        }

        public Block()
        {
            Nonce = 0;
        }

        public Block(DateTime timeStamp, string previousHash, string data)
        {
            Index = 0;
            TimeStamp = timeStamp;
            PreviousHash = previousHash;
            Data = data;
            Hash = CalculateHash();
        }

        public string CalculateHash()
        {
            SHA256 sha256 = SHA256.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes($"{TimeStamp}-{PreviousHash ?? ""}-{Data}-{Nonce}");
            byte[] outputBytes = sha256.ComputeHash(inputBytes);
            return Convert.ToBase64String(outputBytes);
        }

        public async Task Mine(int difficulty)
        {

            var leadingZeros = new string('0', difficulty);
            await Task.Run(() =>
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                while (this.Hash == null || this.Hash.Substring(0, difficulty) != leadingZeros)
                {
                    this.Nonce++;
                    this.Hash = this.CalculateHash();
                }
                sw.Stop();
                Debug.WriteLine(string.Format($"Elapsed time: {sw.Elapsed.TotalMinutes.ToString()} minutes."));
                Debug.WriteLine(string.Format($"Hash: {Hash}"));
            });
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        #endregion
    }
}
