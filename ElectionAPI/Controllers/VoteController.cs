using ElectionModels;
using ElectionAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : BaseController
    {
        private readonly IVoteRepository voteRepository;    
        public VoteController(IConfiguration config, IVoteRepository voteRepository) : base(config)
        {
            this.voteRepository = voteRepository;
        }

        [HttpGet("ByElectionId")]
        public async Task<List<Vote>> Get(Guid electionid, int offset = 0, int take = 1000, bool confirmed = true)
        {
            List<Vote> result = await this.voteRepository.GetAllByElectionId(Context, electionid, offset, take, confirmed);
            return result;
        }

        [HttpGet("ByCategoryType")]
        public async Task<List<Vote>> Get(Guid electionid, int categorytype, int offset = 0, int take = 1000, bool confirmed = true)
        {
            List<Vote> result = await this.voteRepository.GetAllByCategoryType(Context, electionid, categorytype, offset, take, confirmed);
            return result;
        }

        [HttpGet("ByCategoryId")]
        public async Task<List<Vote>> Get(Guid electionid, Guid categoryid, int offset = 0, int take = 1000, bool confirmed = true)
        {
            List<Vote> result = await this.voteRepository.GetAll(Context, electionid, categoryid, offset, take, confirmed);
            return result;
        }

        [HttpGet("ElectionSummary/{Id}")]
        public async Task<List<VoteResult>> GetVoteSummary(Guid Id)
        {
            List<VoteResult> result = await this.voteRepository.GetVoteSummary(Context, Id);
            return result;
        }

        [HttpPost]
        public async Task<Vote> Create([FromBody] Vote vote)
        {
            try
            {
                UOW.BeginTransaction();
                return await this.voteRepository.Insert(UOW, vote);
            }
            finally
            {
                UOW.SaveChanges();
                UOW.CloseTransaction();
            }
        }

        [HttpPost("Election")]
        public async Task<List<Vote>> InsertElection([FromBody] List<Vote> votes)
        {
            List<Vote> result = new List<Vote>();
            try
            {
                UOW.BeginTransaction();
                result = await this.voteRepository.InsertElection(UOW, votes);
                if (votes.Count == result.Count)
                {
                    UOW.SaveChanges();
                }
                return result;
            }
            finally
            {
                UOW.CloseTransaction();
            }
        }

        [HttpPut("{Id}")]
        public async Task<Vote> Update([FromBody] Vote vote)
        {
            Vote result = new Vote();
            try
            {
                UOW.BeginTransaction();
                result = await this.voteRepository.Update(UOW, vote);
                UOW.SaveChanges();
                return result;
            }
            finally
            {
                UOW.CloseTransaction();
            }
        }
    }
}
