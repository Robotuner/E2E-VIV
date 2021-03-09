using Election.Models;
using ElectionModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Election.ViewModels.Views;

namespace Election.Services
{
    public static class DataService
    {
        //private static string electionId = "A13ACD4A-D415-4B27-AFE6-E2310AC71BC6";
        // ngrok http https://localhost:44365 -host-header="localhost:44365";
        private readonly static string electionUrl = "https://6f134493aed1.ngrok.io/api";

        private static HttpClient client { get; set; }

        public static ElectionModels.Election Election { get; set; }
        public static List<Party> Partys { get; set; }
        public static Guid ElectionId
        {
            get
            {
                return  Election == null ? Guid.NewGuid() : Election.Id;
            }
        }

        public static async Task<List<SelectGuidItem>> GetAllElections()
        {
            try
            {
                client = new HttpClient();
                string url = string.Format(@"{0}/Election", electionUrl);
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    // this returns all categorys, and tickets in the election Object!
                    List<ElectionModels.Election> Elections = JsonConvert.DeserializeObject<List<ElectionModels.Election>>(await response.Content.ReadAsStringAsync());
                    List<SelectGuidItem> items = new List<SelectGuidItem>();
                    foreach(ElectionModels.Election election in Elections)
                    {
                        items.Add(new SelectGuidItem()
                        {
                            Id = election.Id,
                            Name = election.Description
                        });
                    }

                    return items;
                }
                else
                {
                    Debug.WriteLine(response.StatusCode);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return null;
        }

        private static void TestAes(List<ElectionModels.Election> election)
        {
            var json = JsonConvert.SerializeObject(election);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            ElectionModels.Misc.Utils.EncryptAesManaged(json);
        }

        public static async Task<int> RequestFullElection(Guid electionId)
        {
            try
            {
                client = new HttpClient();
                string url = string.Format(@"{0}/Election/RequestFullElection/{1}", electionUrl, electionId);
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string ans = await response.Content.ReadAsStringAsync();
                    if (int.TryParse(ans, out int nonce))
                    {
                        return nonce;
                    }                    
                }
                else
                {
                    //MessagingCenter.Send<BlankClass, string>(new BlankClass(), MessagingEvents.ErrorLoadingElection, response.StatusCode.ToString());
                    Debug.WriteLine(response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return 0;
        }

        public static async Task<ElectionModels.Election> InitElection(Guid electionId)
        {
            try
            {
                int expectedNonce = await RequestFullElection(electionId);
                if (expectedNonce == 0)
                    return null;

                client = new HttpClient();
                string url = string.Format(@"{0}/Election/GetFullElection/{1}", electionUrl, electionId);
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string ans = await response.Content.ReadAsStringAsync();
                    ElectionModels.Misc.BlockChain electionChain = JsonConvert.DeserializeObject<ElectionModels.Misc.BlockChain>(ans);
                    // it appears that JsnConvert adds a blank block at the front of the returned chain!
                    // need to compensate for this in the IsValid method
                    if (electionChain != null)
                    {
                        if (expectedNonce == electionChain.GetLatestBlock().Nonce && electionChain.IsValid())
                        {
                            if (electionChain.IsValid())
                            {
                                if (electionChain.GetLatestBlock().Data != null)
                                {
                                    // this returns all categorys, and tickets in the election Object!
                                    Election = JsonConvert.DeserializeObject<ElectionModels.Election>(electionChain.GetLatestBlock().Data);
                                    Partys = await GetPartys();
                                    Election.PartyList = Partys;
                                    return Election;
                                    //MessagingCenter.Send<BlankClass>(new BlankClass(), MessagingEvents.ElectionLoaded);
                                }
                            }
                        }
                    }
                }
                else
                {
                    //MessagingCenter.Send<BlankClass, string>(new BlankClass(), MessagingEvents.ErrorLoadingElection, response.StatusCode.ToString());
                    Debug.WriteLine(response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return null;
        }

        public static async Task<ElectionModels.Election> PostElection(ElectionModels.Election election)
        {
            if (election == null)
                return null;

            using (client = new HttpClient())
            {
                string url = string.Format(@"{0}/Election", electionUrl);
                var json = JsonConvert.SerializeObject(election);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, data);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    // this returns all categorys, and tickets in the election Object!
                    election = JsonConvert.DeserializeObject<ElectionModels.Election>(await response.Content.ReadAsStringAsync());  
                    return Election;
                }
                else
                {
                    Debug.WriteLine(response.StatusCode);
                }
            }

            return null;
        }

        public async static Task<List<VoteResult>> GetElectionSummary(Guid electionId)
        {
            try
            {
                client = new HttpClient();
                string url = string.Format(@"{0}/Vote/ElectionSummary/{1}", electionUrl, electionId);
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string ans = await response.Content.ReadAsStringAsync();
                    List<VoteResult> vrList = JsonConvert.DeserializeObject<List<VoteResult>>(ans);
                    return vrList == null ? new List<VoteResult>() : vrList;
                }
                else
                {
                    Debug.WriteLine(response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return new List<VoteResult>();
        }

        private static async Task<List<Party>> GetPartys()
        {
            try
            {
                client = new HttpClient();
                string url = string.Format(@"{0}/Party", electionUrl);
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    // this returns all categorys, and tickets in the election Object!
                    List<Party> partyList = JsonConvert.DeserializeObject<List<Party>>(await response.Content.ReadAsStringAsync());
                    return partyList;
                    //MessagingCenter.Send<BlankClass>(new BlankClass(), MessagingEvents.ElectionLoaded);
                }
                else
                {
                    //MessagingCenter.Send<BlankClass, string>(new BlankClass(), MessagingEvents.ErrorLoadingElection, response.StatusCode.ToString());
                    Debug.WriteLine(response.StatusCode);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return null;
        }

        public static async Task<List<CategoryType>> InitCategoryTypes()
        {
            List<CategoryType> categoryTypes = null;
            try
            {
                client = new HttpClient();
                string url = string.Format(@"{0}/CategoryType", electionUrl);
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    categoryTypes = JsonConvert.DeserializeObject<List<CategoryType>>(await response.Content.ReadAsStringAsync());
                }
                return categoryTypes;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new List<CategoryType>();
        }

        public static async Task<List<Party>> InitPartyList()
        {
            if (Partys != null)
                return Partys;

            try
            {
                client = new HttpClient();
                string url = string.Format(@"{0}/Party", electionUrl);
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Partys = JsonConvert.DeserializeObject<List<Party>>(await response.Content.ReadAsStringAsync());
                }
                return Partys;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new List<Party>();
        }

        public static async Task<List<Signature>> GetSignatures(Guid Electionid, int skip, int take)
        {
            try
            {
                client = new HttpClient();
                string url = string.Format(@"{0}/Signature?ElectionId={1}&Offset={2}&Take={3}", electionUrl,Electionid.ToString(),skip, take);
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string ans = await response.Content.ReadAsStringAsync();
                    List<Signature> siglist = JsonConvert.DeserializeObject<List<Signature>>(ans);
                    return siglist;
                }
                else
                {
                    //MessagingCenter.Send<BlankClass, string>(new BlankClass(), MessagingEvents.ErrorLoadingElection, response.StatusCode.ToString());
                    Debug.WriteLine(response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return null;
        }
    }
}
