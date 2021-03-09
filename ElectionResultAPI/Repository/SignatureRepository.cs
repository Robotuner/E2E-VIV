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
    public interface ISignatureRepository
    {
        Task<Signature> Insert(IDbConnection context, Signature host);
        Task<List<Signature>> GetAll(IDbConnection context, Guid electionid, int offset = 0, int take = 1000, bool confirmed = true);
        Task<Signature> GetById(IDbConnection context, Guid id);
        Task<Signature> GetByBallotId(IDbConnection context, Guid id);
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

        public async Task<Signature> GetByBallotId(IDbConnection context, Guid Id)
        {
            Signature result = null;
            try
            {
                result = await this.signatureService.GetByBallotId(context, Id);
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public async Task<Signature> Insert(IDbConnection context, Signature vote)
        {
            Signature result = null;
            try
            {
                result = await this.signatureService.Insert(context, vote);
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }
    }
}
