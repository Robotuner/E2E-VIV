using Dapper;
using ElectionModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ElectionResultAPI.Service
{
    public interface ISignatureService
    {
        Task<Signature> Insert(IDbConnection context, Signature Host);
        Task<IEnumerable<Signature>> GetAll(IDbConnection context, Guid electionId, int offset = 0, int take = 1000, bool confirmed = true);
        Task<Signature> GetById(IDbConnection context, Guid id);
        Task<Signature> GetByBallotId(IDbConnection context, Guid id);
    }

    public class SignatureService : ISignatureService
    {
        public SignatureService()
        {

        }

        public async Task<IEnumerable<Signature>> GetAll(IDbConnection context, Guid electionId, int offset = 0, int take = 1000, bool confirmed = true)
        {
            IEnumerable<Signature> result = new List<Signature>();
            try
            {    
                var p = new DynamicParameters();   
                p.Add("@offset", offset, DbType.Int32, ParameterDirection.Input);
                p.Add("@take", take, DbType.Int32, ParameterDirection.Input);
                p.Add("@electionid", electionId, DbType.Guid, ParameterDirection.Input);
                p.Add("@confirmed", confirmed, DbType.Boolean, ParameterDirection.Input);

                result = await context.QueryAsync<Signature>(sql: "Signature_GetAll", param:p, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch 
            {
                throw;
            }

            return result;
        }

        public async Task<Signature> GetById(IDbConnection context, Guid id)
        {
            Signature result = null;
            try
            {
                var p = new DynamicParameters();
                p.Add("@id", id, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);

                result = await context.QuerySingleAsync<Signature>(sql: "Signature_GetById", param: p, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch 
            {
                throw;
            }

            return result;
        }

        public async Task<Signature> GetByBallotId(IDbConnection context, Guid id)
        {
            Signature result = null;
            try
            {
                var p = new DynamicParameters();
                p.Add("@ballotId", id, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);

                result = await context.QuerySingleAsync<Signature>(sql: "Signature_GetByBallotId", param: p, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch 
            {
                throw;
            }

            return result;
        }

        public async Task<Signature> Insert(IDbConnection context, Signature signature)
        {
            Signature result = null;
            try
            {
                Guid newGuid = Guid.NewGuid();
                var p = new DynamicParameters();
                p.Add("@id", newGuid, DbType.Guid, ParameterDirection.Input);
                p.Add("@name", signature.Name, DbType.String, ParameterDirection.Input);
                p.Add("@ballotId", signature.BallotId, DbType.Guid, ParameterDirection.Input);
                p.Add("@confirmed", signature.Confirmed, DbType.Boolean, ParameterDirection.Input);
                p.Add("@imageArray", signature.ImageArray, DbType.Binary, ParameterDirection.Input);
                // Signature_Insert uses NEWID() 
                result = await context.QuerySingleAsync<Signature>(sql: "Signature_Insert", param: p, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }
    }
}
