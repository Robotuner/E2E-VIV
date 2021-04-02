using Dapper;
using ElectionAPI.DataContext;
using ElectionModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ElectionAPI.Service
{
    public interface ISignatureService
    {
        Task<Signature> Insert(IUnitOfWork uow, Signature Host);
        Task<IEnumerable<Signature>> GetAll(IDbConnection context, Guid electionId, int offset = 0, int take = 1000, bool confirmed = true);
        Task<Signature> GetById(IDbConnection context, Guid id);
        Task<Signature> GetById(IUnitOfWork uow, Guid id);
        Task<Signature> GetByBallotId(IUnitOfWork uow, Guid id);
        Task<Signature> UpdateBallotVotes(IUnitOfWork uow, Signature previousSignature, Signature signature);
        Task<SignatureNotice> NotifyPendingSubmittal(IDbConnection context, SignatureNotice notice);
        Task<(int,Guid)> GetExpectedNonce(IUnitOfWork uow, Guid ballotId);
    }

    public class SignatureService : ISignatureService
    {
        private readonly IVoteService voteService;
        private readonly ISignatureNoticeService signatureNoticeService;

        public SignatureService(IVoteService voteService, ISignatureNoticeService signatureNoticeService)
        {
            this.voteService = voteService;
            this.signatureNoticeService = signatureNoticeService;
        }

        public async Task<IEnumerable<Signature>> GetAll(IDbConnection context, Guid electionId, int offset = 0, int take = 1000, bool confirmed = true)
        {
            IEnumerable<Signature> result = new List<Signature>();
            try
            {    
                var p = new DynamicParameters();   
                p.Add("@oset", offset, DbType.Int32, ParameterDirection.Input);
                p.Add("@take", take, DbType.Int32, ParameterDirection.Input);
                p.Add("@electionid", electionId, DbType.Guid, ParameterDirection.Input);
                p.Add("@confirmed", confirmed, DbType.Boolean, ParameterDirection.Input);

                result = await context.QueryAsync<Signature>(sql: "Signature_GetAll", param:p, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
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

        public async Task<Signature> GetById(IUnitOfWork uow, Guid id)
        {
            Signature result = null;
            try
            {
                var p = new DynamicParameters();
                p.Add("@id", id, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);

                result = await uow.Context.QuerySingleAsync<Signature>(sql: "Signature_GetById", param: p, 
                    commandType: System.Data.CommandType.StoredProcedure, transaction: uow.Trans);
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public async Task<Signature> GetByBallotId(IUnitOfWork uow, Guid id)
        {
            IEnumerable<Signature> result = null;
            try
            {
                var p = new DynamicParameters();
                p.Add("@id", id, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);

                result = await uow.Context.QueryAsync<Signature>(sql: "Signature_GetByBallotId", param: p,
                    commandType: System.Data.CommandType.StoredProcedure, transaction: uow.Trans);

                if (result.Count() > 0)
                    return result.First();
            }
            catch (Exception ex)
            {
                throw;
            }

            return null;
        }

        public async Task<(int, Guid)> GetExpectedNonce(IUnitOfWork uow, Guid ballotId)
        {
            try
            {
                var p = new DynamicParameters(); 
                p.Add("@ballotid", ballotId, DbType.Guid, ParameterDirection.Input);

                var ans = await uow.Context.QuerySingleAsync<SignatureNotice>(sql: "SignatureNotice_GetExpectedNonce", param: p,
                    commandType: System.Data.CommandType.StoredProcedure, transaction: uow.Trans);
                return ans == null ? (0, Guid.Empty) : (ans.Nonce, ans.BallotRequestId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<SignatureNotice> NotifyPendingSubmittal(IDbConnection context, SignatureNotice notice)
        {
            SignatureNotice result = null;
            try
            {
                var p = new DynamicParameters();
                p.Add("@id", Guid.NewGuid(), DbType.Guid, ParameterDirection.Input);
                p.Add("@ballotid", notice.BallotId, DbType.Guid, ParameterDirection.Input);
                p.Add("@nonce", notice.Nonce, DbType.Int32, ParameterDirection.Input);
                p.Add("@ballotrequestid", notice.BallotRequestId, DbType.Guid, ParameterDirection.Input);

                result = await context.QuerySingleAsync<SignatureNotice>(sql: "SignatureNotice_Insert", param: p,
                    commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public async Task<Signature> Insert(IUnitOfWork uow, Signature signature)
        {
            Signature result = null;
            try
            {
                List<Vote> voteResult = await voteService.InsertElection(uow, signature.Votes);
                if (voteResult == null || voteResult.Count == signature.Votes.Count)
                {
                    signature.Confirmed = true;

                    var p = new DynamicParameters();
                    p.Add("@id", signature.Id, DbType.Guid, ParameterDirection.Input);
                    p.Add("@ballotid", signature.BallotId, DbType.Guid, ParameterDirection.Input);
                    p.Add("@electionid", signature.ElectionId, DbType.Guid, ParameterDirection.Input);
                    p.Add("@name", signature.Name, DbType.String, ParameterDirection.Input);
                    p.Add("@birthyear", signature.BirthYear, DbType.Int32, ParameterDirection.Input);
                    p.Add("@confirmed", signature.Confirmed, DbType.Boolean, ParameterDirection.Input);
                    p.Add("@deviceid", signature.DeviceId, DbType.String, ParameterDirection.Input);
                    p.Add("@imagearray", signature.ImageArray, DbType.Binary, ParameterDirection.Input);
                    p.Add("@longitude", signature.Longitude, DbType.Double, ParameterDirection.Input);
                    p.Add("@latitude", signature.Latitude, DbType.Double, ParameterDirection.Input);   
                    p.Add("@platform", signature.Platform, DbType.Int32, ParameterDirection.Input);
                    p.Add("@previoussignature", signature.PreviousSignature, DbType.Guid, ParameterDirection.Input);
                    p.Add("@signaturestatus", signature.SignatureStatus, DbType.Int32, ParameterDirection.Input);
                    p.Add("@submitdate", signature.SubmitDate, DbType.DateTime, ParameterDirection.Input);

                    result = await uow.Context.QuerySingleAsync<Signature>(sql: "Signature_Insert", param: p, 
                        commandType: System.Data.CommandType.StoredProcedure, transaction: uow.Trans);

                    if (result != null && result.Id != Guid.Empty && result.Confirmed)
                    {                      
                        result.Votes = voteResult;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        private async Task<Signature> Update(IUnitOfWork uow, Signature signature)
        {
            Signature result = null;
            try
            {
                var p = new DynamicParameters();
                p.Add("@id", signature.Id, DbType.Guid, ParameterDirection.Input);
                p.Add("@signaturestatus", signature.SignatureStatus, DbType.Int32, ParameterDirection.Input);

                result = await uow.Context.QuerySingleAsync<Signature>(sql: "Signature_UpdateStatus", param: p,
                    commandType: System.Data.CommandType.StoredProcedure, transaction: uow.Trans);
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public async Task<Signature> UpdateBallotVotes(IUnitOfWork uow, Signature previousSignature, Signature signature)
        {
            // get all votes cast by previous signature.
            List<Vote> prevVotes = (await this.voteService.GetByBallotID(uow, previousSignature.BallotId)).ToList();
            
            // remove votes that are no longer valid
            foreach(Vote vote in prevVotes)
            {
                // get all candidates that he previously voted for                
                if (!signature.Votes.Any(n => n.SelectionId == vote.SelectionId.Value))
                {
                    // if that candidate isn't on the current ballot, set the choiceRejected flag
                    vote.VoteStatus = (int)VoteStatusEnum.choiceRejected;
                    await this.voteService.Update(uow, vote);
                }
            }
            // add votes to the ballot
            foreach(Vote newVote in signature.Votes)
            {
                // get all candidates that he wants to vote for
                if (!prevVotes.Any(n => n.SelectionId == newVote.SelectionId))
                { 
                    // if he didn't vote for that candidate last time, set add as a new vote.
                    await voteService.Insert(uow, newVote);
                }
            }
            // now update the previous signature
            previousSignature.SignatureStatus = (int)SignatureStatusEnum.hasBeenReplaced;
            await Update(uow, previousSignature);
            signature.PreviousSignature = previousSignature.Id;
            // we need to remove the votes from this insert because they have already been accounted for!
            List<Vote> lst = signature.Votes;
            signature.Votes = null;
            signature.Id = signature.Id == Guid.Empty ? Guid.NewGuid() : signature.Id;
            return await Insert(uow, signature);
        }
    }
}
