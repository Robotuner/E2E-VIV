using ElectionAPI.Service;
using ElectionModels;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ElectionAPI.Repository
{
    public interface IBallotRepository
    {
        Task<Ballot> GetByElection(IDbConnection context, Guid Id);
        Task<int> GetLastNonce(IDbConnection context, Guid Id);
        Task<Ballot> Insert(IDbConnection context, Ballot ballot);
        Task<BallotRequest> BallotRequestInsert(IDbConnection context, BallotRequest ballot);
        Task<BallotRequest> BallotRequestGetById(IDbConnection context, Guid Id);
    }


    public class BallotRepository : BaseRepository, IBallotRepository
    {
        private readonly ILogger<CategoryRepository> _logger;
        private readonly IBallotService ballotService;

        public BallotRepository(ILogger<CategoryRepository> logger, IBallotService ballotService)
        {
            this._logger = logger;
            this.ballotService = ballotService;
        }

        public async Task<int> GetLastNonce(IDbConnection context, Guid Id)
        {
            int result = 0;
            try
            {
                result = await this.ballotService.GetLastNonce(context, Id);
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<Ballot> GetByElection(IDbConnection context, Guid Id)
        {
            Ballot result = null;
            try
            {
                result = await this.ballotService.GetByElection(context, Id);
            }
            catch 
            {
                throw;
            }

            return result;
        }

        public async Task<Ballot> Insert(IDbConnection context, Ballot ballot)
        {
            Ballot result = null;
            try
            {
                result = await this.ballotService.Insert(context, ballot);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

            return result;
        }

        public async Task<BallotRequest> BallotRequestInsert(IDbConnection context, BallotRequest ballotRequest)
        {
            BallotRequest result = null;
            try
            {
                result = await this.ballotService.BallotRequestInsert(context, ballotRequest);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

            return result;
        }

        public async Task<BallotRequest> BallotRequestGetById(IDbConnection context, Guid Id)
        {
            BallotRequest result = null;
            try
            {
                result = await this.ballotService.BallotRequestGetById(context, Id);
            }
            catch
            {
                throw;
            }

            return result;
        }
    }
}
