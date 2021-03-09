using Dapper;
using ElectionModels;
using ElectionAPI.DataContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;

namespace ElectionAPI.Service
{
    public interface IVoteService
    {
        Task<Vote> Insert(IUnitOfWork uow, Vote Host);
        Task<List<Vote>> InsertElection(IUnitOfWork uow, List<Vote> votes);
        Task<IEnumerable<Vote>> GetAll(IDbConnection context, Guid electionId, Guid categoryId, int offset = 0, int take = 1000, bool confirmed = true);
        Task<IEnumerable<Vote>> GetByBallotID(IDbConnection context, Guid id);
        Task<IEnumerable<Vote>> GetByBallotID(IUnitOfWork uow, Guid id);
        Task<Vote> Update(IUnitOfWork uow, Vote vote);
        Task<IEnumerable<VoteResult>> GetVoteSummary(IDbConnection context, Guid electionId);
    }

    public class VoteService : IVoteService
    {
        public VoteService()
        {

        }

        public async Task<IEnumerable<Vote>> GetAll(IDbConnection context, Guid electionId, Guid categoryId, int offset = 0, int take = 1000, bool confirmed = true) 
        {
            IEnumerable<Vote> result = new List<Vote>();
            try
            {
                var p = new DynamicParameters();
                p.Add("@offset", offset, DbType.Int32, ParameterDirection.Input);
                p.Add("@take", take, DbType.Int32, ParameterDirection.Input);
                p.Add("@electionid", electionId, DbType.Guid, ParameterDirection.Input);
                p.Add("@categoryid", categoryId, DbType.Guid, ParameterDirection.Input);
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

        public async Task<IEnumerable<Vote>> GetByBallotID(IDbConnection context, Guid id)
        {
            IEnumerable<Vote> result = null;
            try
            {
                var p = new DynamicParameters();
                p.Add("@ballotId", id, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);

                result = await context.QueryAsync<Vote>(sql: "Vote_GetByBallotId", param: p, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<IEnumerable<Vote>> GetByBallotID(IUnitOfWork uow, Guid id)
        {
            IEnumerable<Vote> result = null;
            try
            {
                var p = new DynamicParameters();
                p.Add("@ballotId", id, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);

                result = await uow.Context.QueryAsync<Vote>(sql: "Vote_GetByBallotId", param: p, 
                    commandType: System.Data.CommandType.StoredProcedure, transaction: uow.Trans);
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<IEnumerable<VoteResult>> GetVoteSummary(IDbConnection context, Guid electionId)
        {
            IEnumerable<VoteResult> result = null;
            try
            {
                var p = new DynamicParameters();
                p.Add("@electionId", electionId, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);

                result = await context.QueryAsync<VoteResult>(sql: "VoteResult_GetByElectionId", param: p,
                    commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
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
                p.Add("@voteStatus", vote.VoteStatus, DbType.Int32, ParameterDirection.Input);
                p.Add("@approvalDate", vote.ApprovalDate, DbType.Date, ParameterDirection.Input);
                // vote_Insert uses newId() to set Id field.
                result = await uow.Context.QuerySingleAsync<Vote>(sql: "Vote_Insert", param: p, commandType: System.Data.CommandType.StoredProcedure, 
                    transaction: uow.Trans);    

            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public async Task<Vote> Update(IUnitOfWork uow, Vote vote)
        {
            Vote result = null;
            try
            {
                Guid newId = Guid.NewGuid();
                var p = new DynamicParameters();
                p.Add("@id", vote.Id, DbType.Guid, ParameterDirection.Input);
                p.Add("@electionId", vote.ElectionId, DbType.Guid, ParameterDirection.Input);
                p.Add("@ballotId", vote.BallotId, DbType.Guid, ParameterDirection.Input);
                p.Add("@categoryId", vote.CategoryId, DbType.Guid, ParameterDirection.Input);
                p.Add("@categoryTypeId", vote.CategoryTypeId, DbType.Boolean, ParameterDirection.Input);
                p.Add("@selectionId", vote.SelectionId, DbType.Guid, ParameterDirection.Input);
                p.Add("@voteStatus", vote.VoteStatus, DbType.Int32, ParameterDirection.Input);
                // vote_Insert uses newId() to set Id field.
                result = await uow.Context.QuerySingleAsync<Vote>(sql: "Vote_Update", param: p, commandType: System.Data.CommandType.StoredProcedure,
                    transaction: uow.Trans);

            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public async Task<List<Vote>> InsertElection(IUnitOfWork uow, List<Vote> votes)
        {
            if (!ValidateVotes(votes))
                return null;

            try
            {
                List<Vote> lst = new List<Vote>();
                foreach(Vote vote in votes)
                {
                    Vote result = await Insert(uow, vote);
                    if (result != null)
                    {
                        lst.Add(result);
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private bool ValidateVotes(List<Vote> allVotes)
        {
            if (allVotes == null || allVotes.Count == 0)
                return false;

            // now make sure that all votes have the same electionId and ballotId
            Guid electionId = allVotes[0].ElectionId;
            Guid ballotId = allVotes[0].BallotId;
            List<Guid> CategoryList = new List<Guid>();
            List<Guid> SelectionList = new List<Guid>();
            foreach(Vote vote in allVotes)
            {
                if (electionId != vote.ElectionId || ballotId != vote.BallotId)
                    return false;
                // make sure the categoryId isn't duplicated.
                if (!CategoryList.Any(n => n == vote.CategoryId))
                {
                    CategoryList.Add(vote.CategoryId);
                }
                else
                {
                    return false;
                }
                // make sure the selectionId isn't duplicated in ballot
                if (!SelectionList.Any(n => n == vote.SelectionId))
                {
                    SelectionList.Add(vote.SelectionId.Value);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }
}
