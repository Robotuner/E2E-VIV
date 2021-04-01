using ElectionModels;
using ElectionAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElectionModels.Misc;
using System.Linq;
using Newtonsoft.Json;
using ElectionAPI.DataContext;

namespace ElectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignatureController : BaseController
    {
        private readonly ISignatureRepository signatureRepository;
        public SignatureController(IConfiguration config, ISignatureRepository signatureRepository) : base(config)
        {
            this.signatureRepository = signatureRepository;
        }

        [HttpGet]
        public async Task<List<Signature>> Get(Guid electionid, int offset = 0, int take = 1000, bool confirmed = true)
        {
            List<Signature> result = await this.signatureRepository.GetAll(Context, electionid, offset, take, confirmed);
            return result;
        }

        [HttpGet("{Id}")]
        public async Task<Signature> Get(Guid Id)
        {
            Signature result = await this.signatureRepository.GetById(Context, Id);
            return result;
        }

        [HttpGet("BallotId/{Id}")]
        public async Task<Signature> GetByBallotId(Guid Id)
        {
            Signature result = await this.signatureRepository.GetByBallotId(UOW, Id);
            return result;
        }

        [HttpPost("NotifyPending")]
        public async Task<SignatureNotice> NotifyPending([FromBody] SignatureNotice notice)
        {
            SignatureNotice result = null;
            try
            {
                result = await this.signatureRepository.NotifyPendingSubmittal(Context, notice);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            finally
            {  
            }
            return result;
        }

        [HttpPost]
        public async Task<Signature> Create([FromBody] BlockChain electionChain)
        {
            Signature result = null;
            try
            {                
                UOW.BeginTransaction();
                Signature signature = this.GetSignatureFromBlockChain(electionChain);
                // now check to make sure the nonce matches the expected nonce
                (int expectedNonce, string deviceId) = await this.signatureRepository.GetExpectedNonce(UOW, signature.BallotId);

                // make sure the nonce and device id are correct
                if (signature == null || expectedNonce != electionChain.GetLatestBlock().Nonce ||
                    signature.DeviceId != deviceId)
                    return null;

                Signature existingSignature = await this.GetByBallotId(signature.BallotId);
                if (existingSignature != null)
                {
                    result =  await signatureRepository.UpdateBallotVotes(UOW, existingSignature, signature);
                }
                else
                {
                    result = await this.InsertNewSignatureBallot(UOW, signature);
                }
                UOW.SaveChanges();
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            finally
            {
                UOW.CloseTransaction();
            }
            return result;
        }

        private async Task<Signature> InsertNewSignatureBallot(IUnitOfWork uow, Signature signature)
        {
            Guid newGuid = Guid.NewGuid();
            signature.Id = newGuid;
            Signature result = await this.signatureRepository.Insert(uow, signature);
            if (signature.Confirmed && signature.Votes.Count == result.Votes.Count)
            {
                // note that the signature Guid is empty until the record is saved!
                result = await signatureRepository.GetById(uow,newGuid);
            }
            else
            { 
                // means that the signature was not saved! so reset to null;
                result = null;
            }

            return result;
        }

        private Signature GetSignatureFromBlockChain(BlockChain electionChain)
        {
            if (electionChain == null)
                return null;

            Block lastBlock = electionChain.Chain?.Last();
            bool blockResult = electionChain.IsValid();
            if (!blockResult)
                return null;

            if (lastBlock != null)
            {
                return JsonConvert.DeserializeObject<Signature>(lastBlock.Data);
            }

            return null;
        }


    }
}
