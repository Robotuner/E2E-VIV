using AutoMapper;
using ElectionModels;
using ElectionModels.Misc;
using Newtonsoft.Json;
using OneVote.Models;
using OneVote.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace OneVote.Services
{
    public static class DataService 
    {
        // ngrok http https://localhost:44365 -host-header="localhost:44365";
        private static readonly string electionUrl = "https://6f134493aed1.ngrok.io/api";
        private static readonly string electionResultUrl = "https://6f134493aed1.ngrok.io/api";

        //private static string electionUrl = "http://localhost/api";
        //private static string electionResultUrl = "http://localhost/api";

        private static HttpClient client { get; set; }

        public static string QRText { get; set; }
        public static Election Election { get; set; }
        public static List<CategoryType> CategoryTypes { get; set; }
        public static List<CategoryTypeItem> CategoryTypeItems { get; set; }
        public static List<CategoryViewModel> CategoryList = new List<CategoryViewModel>();

        public static bool CanCastBallot()
        {
            if (Election != null)
            {
                return DateTime.Now >= Election.StartDateLocal && DateTime.Now <= Election.EndDateLocal;
            }

            return false;
        }

        public static async Task InitElection(Guid electionId)
        {
            try
            {
                Location location = await Models.Utils.GetDeviceLocation();
                if (DataService.CategoryTypes == null)
                {
                    await DataService.InitCategoryTypes();
                }

                client = new HttpClient();
                string url = string.Format(@"{0}/Election/GetFullElection/{1}", electionUrl, electionId);
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string ans = await response.Content.ReadAsStringAsync();
                    ElectionModels.Misc.BlockChain electionChain = JsonConvert.DeserializeObject<ElectionModels.Misc.BlockChain>(ans);
                    if (electionChain != null)
                    {
                        if (electionChain.IsValid())
                        {
                            if (electionChain.GetLatestBlock().Data != null)
                            {
                                Election = JsonConvert.DeserializeObject<Election>(electionChain.GetLatestBlock().Data);
                                InitCategoryViewModel();
                                MessagingCenter.Send<BlankClass>(new BlankClass(), MessagingEvents.ElectionLoaded);
                            }
                        }
                    }
                }
                else
                {
                    MessagingCenter.Send<BlankClass, string>(new BlankClass(), MessagingEvents.ErrorLoadingElection, response.StatusCode.ToString());
                    Debug.WriteLine(response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public static async Task InitCategoryTypes()
        {
            try
            {
                client = new HttpClient();
                string url = string.Format(@"{0}/CategoryType", electionUrl);
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    CategoryTypes = JsonConvert.DeserializeObject<List<CategoryType>>(await response.Content.ReadAsStringAsync());
                    CategoryTypeItems = new List<CategoryTypeItem>();
                    foreach (CategoryType ctype in CategoryTypes)
                    {
                        switch(ctype.Id)
                        {
                            case (int)CategoryTypeEnum.measure:
                                CategoryTypeItems.Add(new CategoryTypeItem { Id = CategoryTypeEnum.measure, Text = "Measures / Propositions", Description = "Measures." });
                                break;
                            case (int)CategoryTypeEnum.federal:
                                CategoryTypeItems.Add(new CategoryTypeItem { Id = CategoryTypeEnum.federal, Text = "Federal Candidates", Description = "US Executive and Legislative Branch." });
                                break;
                            case (int)CategoryTypeEnum.state:
                                CategoryTypeItems.Add(new CategoryTypeItem { Id = CategoryTypeEnum.state, Text = "Statewide Candidates", Description = "State Executive Branch" });
                                break;
                            case (int)CategoryTypeEnum.legislative:
                                CategoryTypeItems.Add(new CategoryTypeItem { Id = CategoryTypeEnum.legislative, Text = "Legislative Candidates", Description = "State Legislative Branch" });
                                break;
                            case (int)CategoryTypeEnum.judicial:
                                CategoryTypeItems.Add(new CategoryTypeItem { Id = CategoryTypeEnum.judicial, Text = "Judical Candidates", Description = "State Judicial Branch" });
                                break;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public static void InitCategoryViewModel()
        {            
            CategoryList.Clear();
            IMapper mapper = Models.Utils.CreateMapper();
            try
            {
                foreach (CategoryType catType in CategoryTypes)
                {
                    Debug.WriteLine($"Category Type: {catType.Description}");
                    IEnumerable<Category> catList = Election.CategoryList.Where(n => (int)n.CategoryTypeId == catType.Id);
                    foreach (Category cat in catList.OrderBy(n => n.Sequence))
                    {
                        Debug.WriteLine($"    Category: {cat.Sequence}:{cat.Heading}");
                        CategoryViewModel cvm = mapper.Map<Category, CategoryViewModel>(cat);
                        cvm.Category = cat;
                        cvm.Tickets = new ObservableCollection<TicketViewModel>();
                        foreach (Ticket ticket in cat.Tickets?.OrderBy(n => n.Sequence))
                        {
                            Debug.WriteLine($"        Choice: {ticket.Description}" );
                            TicketViewModel tvm = mapper.Map<TicketViewModel>(ticket);
                            tvm.Ticket = ticket;
                            tvm.Party = ticket.PartyId != null ? Election.PartyList.FirstOrDefault(n => n.Id == ticket.PartyId.Value)?.Description : null;
                            tvm.SelectedTicket = cvm.TickedSelected;
                            cvm.Tickets.Add(tvm);
                        }
                        cvm.Category = cat;
                        cvm.SetTicketViewHeight((CategoryTypeEnum)catType.Id);
                        CategoryList.Add(cvm);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public static async Task<List<Vote>> PutElection(List<Vote> votes)
        {
            try
            {
                string url = string.Format(@"{0}/Vote/Election", electionResultUrl);
                var json = JsonConvert.SerializeObject(votes);                
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                using (client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PostAsync(url, data);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        List<Vote> result = JsonConvert.DeserializeObject<List<Vote>>(await response.Content.ReadAsStringAsync());
                        return result;
                    }
                    else
                    {  
                        Debug.WriteLine(response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return new List<Vote>();
        }

        public static async Task<Signature> PutSignature(Signature signature)
        {
            try
            {
                string url = string.Format(@"{0}/Signature", electionResultUrl);
                var json = JsonConvert.SerializeObject(signature);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                using (client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PostAsync(url, data);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Signature result = JsonConvert.DeserializeObject<Signature>(await response.Content.ReadAsStringAsync());
                        return result;
                    }
                    else
                    {
                        Debug.WriteLine(response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return null;
        }

        public static async Task<SignatureNotice> NotifyPendingSubmittal(int nonce, Guid ballotId)
        {
            SignatureNotice notice = new SignatureNotice()
            {
                BallotId = ballotId,
                Nonce = nonce
            };
            try
            {
                string url = string.Format(@"{0}/Signature/NotifyPending", electionResultUrl);
                var json = JsonConvert.SerializeObject(notice);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                using (client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PostAsync(url, data);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        SignatureNotice result = JsonConvert.DeserializeObject<SignatureNotice>(await response.Content.ReadAsStringAsync());
                        return result;
                    }
                    else
                    {
                        Debug.WriteLine(response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return null;
        }

        public static async Task<Signature> PutSignature(BlockChain signature)
        {
            try
            {
                string url = string.Format(@"{0}/Signature", electionResultUrl);
                var json = JsonConvert.SerializeObject(signature);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                using (client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PostAsync(url, data);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Signature result = JsonConvert.DeserializeObject<Signature>(await response.Content.ReadAsStringAsync());
                        return result;
                    }
                    else
                    {
                        Debug.WriteLine(response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return null;
        }

        public static List<Vote> Validate(Guid? ballotId = null)
        {
            List<Vote> votes = new List<Vote>();

            foreach (CategoryViewModel cvm in CategoryList)
            {
                if (cvm.Selection != null)
                {
                    Vote vote = new Vote()
                    {
                        ElectionId = Election.Id,
                        BallotId = ballotId??Guid.Empty,
                        CategoryId = cvm.Id,
                        CategoryTypeId = (int)cvm.CategoryType,
                        SelectionId = cvm.Selection.Id,
                        VoteStatus = (int)VoteStatusEnum.isValidVote  // this is always valid, server side will figure out 
                                                                      // if this vote replaces a previous choice.
                    };
                    votes.Add(vote);                   
                }
            }
            return votes;
        }

        public static void ClearVotes()
        {
            foreach (CategoryViewModel cvm in CategoryList)
            {
                cvm.Selection = null;
                foreach(TicketViewModel tvm in cvm.Tickets)
                {
                    tvm.Selected = false;
                }
            }
        }

        public static (int count,int total) GetCategoryStatus(CategoryTypeItem cti)
        {
            List<CategoryViewModel> cvmList = DataService.CategoryList.Where(n => n.CategoryType == cti.Id)?.OrderBy(n => n.Sequence)?.ToList();
            int selected = cvmList.Count(n => n.Selection != null);
            return (selected, cvmList.Count);
        }

        //public async static Task<List<VoteResult>> GetElectionSummary(Guid electionId)
        //{
        //    try
        //    {
        //        client = new HttpClient();
        //        string url = string.Format(@"{0}/Vote/ElectionSummary/{1}", electionId);
        //        HttpResponseMessage response = await client.GetAsync(url);
        //        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            string ans = await response.Content.ReadAsStringAsync();
        //            List<VoteResult> vrList = JsonConvert.DeserializeObject<List<VoteResult>>(ans);
        //            return vrList == null ? new List<VoteResult>() : vrList;
        //        }
        //        else
        //        {
        //            MessagingCenter.Send<BlankClass, string>(new BlankClass(), MessagingEvents.ErrorLoadingElection, response.StatusCode.ToString());
        //            Debug.WriteLine(response.StatusCode);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.Message);
        //    }
        //    return new List<VoteResult>();
        //}
    }
}
