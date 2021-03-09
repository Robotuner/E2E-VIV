using ElectionAPI.Repository;
using ElectionModels;
using ElectionModels.Misc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ElectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectionController : BaseController
    {
        private readonly IElectionRepository electionRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly ITicketRepository ticketRepository;
        private readonly IBallotRepository ballotRepository;

        public ElectionController(IConfiguration config, IElectionRepository electionRepository,
            ICategoryRepository categoryRepository, ITicketRepository ticketRepository,
            IBallotRepository ballotRepository) : base(config)
        {
            this.electionRepository = electionRepository;
            this.categoryRepository = categoryRepository;
            this.ticketRepository = ticketRepository;
            this.ballotRepository = ballotRepository;
        }

        [HttpGet]
        public async Task<List<Election>> Get()
        {
            List<Election> result = await this.electionRepository.GetAll(Context);
            return result;
        }

        [HttpPost]
        public async Task<Election> Insert([FromBody] Election election)
        {
            Election result = null;
            try
            {
                await DeleteAllObjects(election);
                result = await CreateAllNewObjects(election);
                result = await UpdateAllObjects(election);

                BlockChain electionChain = await Utils.ConvertToBlockChain<Election>(result);
                Ballot ballot = new Ballot()
                {
                    Nonce = electionChain.GetLatestBlock().Nonce,
                    BallotChain = JsonConvert.SerializeObject(electionChain),
                    ElectionId = election.Id
                };
                await this.ballotRepository.Insert(Context, ballot);
               
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
            finally
            {
            }
            return result;
        }

        private async Task DeleteAllObjects(Election election)
        {
            if (election.Delete)
            {
                await electionRepository.Delete(Context, election.Id);
            }
            else
            {
                foreach(Category cat in election.CategoryList)
                {
                    if (cat.Delete)
                    {
                        await categoryRepository.Delete(Context, cat.Id);
                    }
                    else
                    {
                        foreach(Ticket ticket in cat.Tickets)
                        {
                            if (ticket.Delete)
                            {
                                await ticketRepository.Delete(Context, ticket.Id);
                            }
                        }
                    }
                }
            }
        }

        private async Task<Election> CreateAllNewObjects(Election election)
        {
            Election foundElection = await this.electionRepository.GetByID(Context, election.Id);
            if (foundElection == null)
            {
                await this.electionRepository.Insert(Context, election);
                election.HasBeenCreated = true;                
            }
            await AddNewCategories(election);
            return election;
        }

        private async Task AddNewCategories(Election election)
        {
            foreach (Category cat in election.CategoryList)
            {
                Category foundCategory = await this.categoryRepository.GetByID(Context, cat.Id);
                if (foundCategory == null)
                {
                    await this.categoryRepository.Insert(Context, cat);
                    cat.HasBeenCreated = true;
                }                 
                foreach(Ticket ticket in cat.Tickets)
                {
                    Ticket foundTicket = await ticketRepository.GetByID(Context, ticket.Id);
                    if (foundTicket == null)
                    {
                        await this.ticketRepository.Insert(Context, ticket);
                        ticket.HasBeenCreated = true;
                    }
                }
            }
        }

        private async Task<Election> UpdateAllObjects(Election election)
        {
            if (election.HasBeenCreated)
                return election;

            Election result = await Update(election);

            foreach (Category cat in election.CategoryList)
            {
                if (cat.Delete || cat.HasBeenCreated)
                    continue;

                await categoryRepository.Update(Context, cat);
                foreach (Ticket ticket in cat.Tickets)
                {
                    if (ticket.Delete || ticket.HasBeenCreated)
                        continue;

                    await ticketRepository.Update(Context, ticket);
                }
            }
            return election;
        }

        [HttpGet("InitElectionBallot/{Id}")]
        public async Task InitElectionBallotTable(Guid Id)
        {
            Election result = await this.electionRepository.GetFullElection(Context, Id);
            BlockChain electionChain = await Utils.ConvertToBlockChain<Election>(result);
            Ballot ballot = new Ballot()
            {
                Nonce = electionChain.GetLatestBlock().Nonce,
                BallotChain = JsonConvert.SerializeObject(electionChain),
                ElectionId = Id
            };
            await this.ballotRepository.Insert(Context, ballot);
        }

        [HttpGet("GetFullElection/{Id}")]
        public async Task<BlockChain> GetFullElection(Guid Id)
        {
            Ballot ballot = await this.ballotRepository.GetByElection(Context, Id);
            if (ballot != null)
            {
                BlockChain electionChain = JsonConvert.DeserializeObject<BlockChain>(ballot.BallotChain);
                if (electionChain.IsValid())
                {
                    return electionChain;
                }
            }

            return null;
        }

        [HttpGet("RequestFullElection/{Id}")]
        public async Task<int> RequestFullElection(Guid Id)
        {
            int nonce = await this.ballotRepository.GetLastNonce(Context, Id);
            return nonce;
        }

        [HttpDelete("{Id}")]
        public async Task<bool> Delete(Guid Id)
        {
            // deletes all categorys and tickets too!
            Election result = await this.electionRepository.Delete(Context, Id);
            return true;
        }

        [HttpPut("{Id}")]
        public async Task<Election> Update([FromBody] Election electionObj)
        {
            // updates only election table
            Election result = await this.electionRepository.Update(Context, electionObj);
            
            return result;
        }

        //private async Task<BlockChain> ConvertToBlockChain(Election election)
        //{
        //    BlockChain ElectionChain = new BlockChain();
        //    try
        //    {
        //        Block block = new Block(DateTime.Now, null, JsonConvert.SerializeObject(election));
        //        block.PropertyChanged += (s, a) =>
        //        {
        //            Debug.WriteLine($"Nonce: {block.Nonce}");
        //            //if (a.PropertyName == "Nonce")
        //            //{
        //            //    // https://lukealderton.com/blog/posts/2016/october/xamarin-forms-working-with-threads/
        //            //    Device.BeginInvokeOnMainThread(() =>
        //            //    {
        //            //        double ans = sw.ElapsedMilliseconds / 60000.0;
        //            //        string msg = string.Format("Elapsed Time: {0} min\n", Math.Round(ans, 2));
        //            //        string lne2 = string.Format(Resource.ItemsViewmodelProgressUpdate, block.Nonce);
        //            //        this.ElapsedTime = msg + lne2;
        //            //    });
        //            //}
        //        };
        //        await ElectionChain.AddBlock(block);
        //        //// simulate data that we send to server
        //        //var json = JsonConvert.SerializeObject(ElectionChain);
        //        //var data = new StringContent(json, Encoding.UTF8, "application/json");

        //        //// now simulate converting it back on the server side
        //        //Block currentBlock = ElectionChain.Chain.Last();
        //        //Signature sig = JsonConvert.DeserializeObject<Signature>(currentBlock.Data);
        //    }
        //    finally
        //    {
        //    }
        //    return ElectionChain;
        //}
    }
}
