using ElectionModels;
using ElectionAPI.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ElectionAPI.DataContext;

namespace ElectionAPI.Repository
{
    public interface ISignatureRepository
    {
        Task<Signature> Insert(IUnitOfWork uow, Signature host);
        Task<List<Signature>> GetAll(IDbConnection context, Guid electionid, int offset = 0, int take = 1000, bool confirmed = true);
        Task<Signature> GetById(IDbConnection context, Guid id);
        Task<Signature> GetById(IUnitOfWork uow, Guid Id);
        Task<Signature> GetByBallotId(IUnitOfWork uow, Guid id);
        Task<Signature> UpdateBallotVotes(IUnitOfWork uow, Signature previousSignature, Signature signature);
        Task<SignatureNotice> NotifyPendingSubmittal(IDbConnection context, SignatureNotice notice);
        Task<(int, Guid)> GetExpectedNonce(IUnitOfWork uow, Guid ballotId);
    }

    public class SignatureRepository : BaseRepository, ISignatureRepository
    {
        private readonly ILogger<SignatureRepository> _logger;
        private readonly ISignatureService signatureService;        
        public SignatureRepository(ILogger<SignatureRepository> logger, ISignatureService signatureService)
        {
            this._logger = logger;
            this.signatureService = signatureService;
        }

        public async Task<List<Signature>> GetAll(IDbConnection context, Guid electionid, int offset = 0, int take = 1000, bool confirmed = true)
        {
            List<Signature> result = null;
            try
            {
                result = (await this.signatureService.GetAll(context, electionid, offset, take, confirmed))?.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public async Task<Signature> GetById(IDbConnection context, Guid Id)
        {
            Signature result = null;
            try
            {
                result = await this.signatureService.GetById(context, Id);
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public async Task<Signature> GetById(IUnitOfWork uow, Guid Id)
        {
            Signature result = null;
            try
            {
                result = await this.signatureService.GetById(uow, Id);
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public async Task<Signature> GetByBallotId(IUnitOfWork uow, Guid Id)
        {
            Signature result = null;
            try
            {
                result = await this.signatureService.GetByBallotId(uow, Id);
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public async Task<(int,Guid)> GetExpectedNonce(IUnitOfWork uow, Guid ballotId)
        {
            (int,Guid) result = (0, Guid.Empty);
            try
            {
                result = await this.signatureService.GetExpectedNonce(uow, ballotId);
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public async Task<SignatureNotice> NotifyPendingSubmittal(IDbConnection context, SignatureNotice notice)
        {
            SignatureNotice result = null;
            try
            {
                result = await this.signatureService.NotifyPendingSubmittal(context, notice);
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public async Task<Signature> Insert(IUnitOfWork uow, Signature vote)
        {
            Signature result = null;
            try
            {
                Signature foundBallot = await GetByBallotId(uow, vote.BallotId);
                if (foundBallot == null)
                {
                    result = await this.signatureService.Insert(uow, vote);
                }
                else
                {
                    throw new InvalidOperationException("Ballot Id has already been used.");
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public async Task<Signature> UpdateBallotVotes(IUnitOfWork uow, Signature previousSignature, Signature signature)
        {
            Signature result = null;
            try
            {
                result = await this.signatureService.UpdateBallotVotes(uow, previousSignature, signature);
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }
    }
}
