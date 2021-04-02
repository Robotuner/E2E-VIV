using ElectionAPI.Repository;
using ElectionModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ElectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BallotController : BaseController
    {
        private readonly IBallotRepository ballotRepository;

        public BallotController(IConfiguration config, IBallotRepository ballotRepository) : base(config)
        {
            this.ballotRepository = ballotRepository;
        }

        [HttpGet("ByElection/{Id}")]
        public async Task<Ballot> GetByElection(Guid Id)
        {
            Ballot result = await this.ballotRepository.GetByElection(Context, Id);
            return result;
        }

        [HttpPost]
        public async Task<Ballot> Insert([FromBody] Ballot ballot)
        {
            Ballot result = null;
            try
            {
                result = await this.ballotRepository.Insert(Context, ballot);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
            finally
            {
            }
            return result;
        }

        [HttpPost("Request")]
        public async Task<BallotRequest> Insert([FromBody] BallotRequest ballotRequest)
        {
            BallotRequest result = null;
            try
            {
                result = await this.ballotRepository.BallotRequestInsert(Context, ballotRequest);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
            finally
            {
            }
            return result;
        }

        [HttpGet("Request/{Id}")]
        public async Task<BallotRequest> BallotRequestGetById(Guid Id)
        {
            BallotRequest result = await this.ballotRepository.BallotRequestGetById(Context, Id);
            return result;
        }
    }
}
