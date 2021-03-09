﻿using Dapper;
using ElectionModels;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ElectionAPI.Service
{
    public interface IBallotService
    {
        Task<Ballot> Insert(IDbConnection context, Ballot ballot);
        Task<Ballot> GetByElection(IDbConnection context, Guid Id);
        Task<int> GetLastNonce(IDbConnection context, Guid Id);
    }
    public class BallotService : BaseService, IBallotService
    {

        public BallotService()
        {

        }

        public async Task<int> GetLastNonce(IDbConnection context, Guid Id)
        {
            try
            {
                Ballot ans = await GetByElection(context, Id);
                return (ans == null) ? 0 : ans.Nonce; 
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Ballot> GetByElection(IDbConnection context, Guid Id)
        {
            Ballot result = null;
            try
            {
                var p = new DynamicParameters();
                p.Add("@electionId", Id, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);

                var ans = await context.QueryAsync<Ballot>(sql: "Ballot_GetByElection", param: p,  commandType: System.Data.CommandType.StoredProcedure);
                return ans.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public async Task<Ballot> Insert(IDbConnection context, Ballot ballot)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@id", Guid.NewGuid(), DbType.Guid, ParameterDirection.Input);
                p.Add("@electionId", ballot.ElectionId, DbType.Guid, ParameterDirection.Input);
                p.Add("@nonce", ballot.Nonce, DbType.Int32, ParameterDirection.Input);
                p.Add("@ballotChain", ballot.BallotChain, DbType.String, ParameterDirection.Input);
    
                Ballot ans = await context.QuerySingleAsync<Ballot>(sql: "Ballot_Insert", param: p,  commandType: System.Data.CommandType.StoredProcedure);
                return ans;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
