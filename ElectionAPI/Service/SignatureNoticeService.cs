using Dapper;
using ElectionModels;
using System;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ElectionAPI.Service
{
    public interface ISignatureNoticeService
    {
        Task<SignatureNotice> Insert(IDbConnection context, SignatureNotice notice);
        Task<int> GetLastNonce(IDbConnection context, Guid Id);
    }
    public class SignatureNoticeService : BaseService, ISignatureNoticeService
    {

        public SignatureNoticeService()
        {

        }

        public async Task<int> GetLastNonce(IDbConnection context, Guid Id)
        {
            try
            {
                SignatureNotice ans = await GetByBallotId(context, Id);
                return (ans == null) ? 0 : ans.Nonce;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<SignatureNotice> GetByBallotId(IDbConnection context, Guid Id)
        {
            SignatureNotice result = null;
            try
            {
                var p = new DynamicParameters();
                p.Add("@ballotId", Id, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);

                result = await context.QuerySingleAsync<SignatureNotice>(sql: "SignatureNotice_GetByBallot", param: p, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public async Task<SignatureNotice> Insert(IDbConnection context, SignatureNotice notice)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@id", Guid.NewGuid(), DbType.Guid, ParameterDirection.Input);
                p.Add("@nonce", notice.Nonce, DbType.Int32, ParameterDirection.Input);
                p.Add("@ballotId", notice.BallotId, DbType.Guid, ParameterDirection.Input);

                SignatureNotice ans = await context.QuerySingleAsync<SignatureNotice>(sql: "SignatureNotice_Insert", param: p, commandType: System.Data.CommandType.StoredProcedure);
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
