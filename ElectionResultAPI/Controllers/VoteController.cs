using ElectionModels;
using ElectionResultAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectionResultAPI.Controllers
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

        //[HttpGet]
        //public async Task<List<Vote>> Get(Guid electionid, int offset = 0, int take = 1000, bool confirmed = true)
        //{
        //    List<Vote> result = await this.voteRepository.GetAll(Context, electionid, offset, take, confirmed);
        //    return result;
        //}

        [HttpPost]
        public async Task<Vote> Create([FromBody] Vote electionObj)
        {
            try
            {
                UOW.BeginTransaction();
                return await this.voteRepository.Insert(UOW, electionObj);
            }
            finally
            {
                UOW.CloseTransaction();
                UOW.SaveChanges();
            }
        }

        [HttpPost("Election")]
        public async Task<int> InsertElection([FromBody] List<Vote> electionObj)
        {
            try
            {
                UOW.BeginTransaction();                
                var result = await this.voteRepository.InsertElection(UOW, electionObj);
            }
            finally
            {
                UOW.SaveChanges();
                UOW.CloseTransaction();
            }
            return 0;
        }
    }
}
