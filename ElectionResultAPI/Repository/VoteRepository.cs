using ElectionModels;
using ElectionResultAPI.DataContext;
using ElectionResultAPI.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ElectionResultAPI.Repository
{
    public interface IVoteRepository
    {
        Task<Vote> Insert(IUnitOfWork uow, Vote host);
        Task<int> InsertElection(IUnitOfWork uow, List<Vote> votes);
        //Task<List<Vote>> GetAll(IDbConnection context, Guid electionid, int offset = 0, int take = 1000, bool confirmed = true);
    }

    public class VoteRepository : BaseRepository, IVoteRepository
    {
        private readonly ILogger<VoteRepository> _logger;
        private readonly IVoteService voteService;
        public VoteRepository(ILogger<VoteRepository> logger, IVoteService voteService)
        {
            this._logger = logger;
            this.voteService = voteService;
        }

        public async Task<List<Vote>> GetAll(IDbConnection context, Guid electionid, int offset = 0, int take = 1000, bool confirmed = true)
        {
            List<Vote> result = new List<Vote>();
            try
            {
                result = (await this.voteService.GetAll(context, electionid, offset, take, confirmed))?.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public async Task<Vote> Insert(IUnitOfWork uow, Vote vote)
        {
            Vote result = null;
            try
            {
                result = await this.voteService.Insert(uow, vote);
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public async Task<int> InsertElection(IUnitOfWork uow, List<Vote> votes)
        {
            int result;
            try
            {
                result = await this.voteService.InsertElection(uow, votes);
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }
    }
}
