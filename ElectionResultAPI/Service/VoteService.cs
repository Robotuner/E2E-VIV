using Dapper;
using ElectionModels;
using ElectionResultAPI.DataContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ElectionResultAPI.Service
{
    public interface IVoteService
    {
        Task<Vote> Insert(IUnitOfWork uow, Vote Host);
        Task<int> InsertElection(IUnitOfWork uow, List<Vote> votes);
        Task<IEnumerable<Vote>> GetAll(IDbConnection context, Guid electionId, int offset = 0, int take = 1000, bool confirmed = true);
    }

    public class VoteService : IVoteService
    {
        public VoteService()
        {

        }

        public async Task<IEnumerable<Vote>> GetAll(IDbConnection context, Guid electionId, int offset = 0, int take = 1000, bool confirmed = true) 
        {
            IEnumerable<Vote> result = new List<Vote>();
            try
            {
                var p = new DynamicParameters();
                p.Add("@offset", offset, DbType.Int32, ParameterDirection.Input);
                p.Add("@take", take, DbType.Int32, ParameterDirection.Input);
                p.Add("@electionid", electionId, DbType.Guid, ParameterDirection.Input);
                p.Add("@confirmed", confirmed, DbType.Boolean, ParameterDirection.Input);

                result = await context.QueryAsync<Vote>(sql: "Vote_GetAll", param: p, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch 
            {
                throw;
            }

            return result;
        }

        private async Task<Vote> GetByID(IDbConnection context, Guid id)
        {
            Vote result = null;
            try
            {
                var p = new DynamicParameters();
                p.Add("@id", id, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);

                result = await context.QuerySingleAsync<Vote>(sql: "Vote_GetById", param: p, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch 
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
                Guid newId = Guid.NewGuid();
                var p = new DynamicParameters();   
                p.Add("@id", newId, DbType.Guid, ParameterDirection.Input);
                p.Add("@electionId", vote.ElectionId, DbType.Guid, ParameterDirection.Input);
                p.Add("@ballotId", vote.BallotId, DbType.Guid, ParameterDirection.Input);
                p.Add("@categoryId", vote.CategoryId, DbType.Guid, ParameterDirection.Input);
                p.Add("@categoryTypeId", vote.CategoryTypeId, DbType.Boolean, ParameterDirection.Input);
                p.Add("@selectionId", vote.SelectionId, DbType.Guid, ParameterDirection.Input);
                // vote_Insert uses newId() to set Id field.
                result = await uow.Context.QuerySingleAsync<Vote>(sql: "Vote_Insert", param: p, commandType: System.Data.CommandType.StoredProcedure, transaction: uow.Trans);    

            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public async Task<int> InsertElection(IUnitOfWork uow, List<Vote> votes)
        {
            try
            {
                int cnt = 0;
                foreach(Vote vote in votes)
                {
                    cnt++;
                    await Insert(uow, vote);
                }
                return cnt;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
