using ElectionModels;
using ElectionResultAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectionResultAPI.Controllers
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
            Signature result = await this.signatureRepository.GetByBallotId(Context, Id);
            return result;
        }

        [HttpPost]
        public async Task<Signature> Create([FromBody] Signature electionObj)
        {
            Signature result = null;
            try
            {
                UOW.BeginTransaction();
                result = await this.signatureRepository.Insert(Context, electionObj);
                if (result.Id != Guid.Empty && result.Confirmed && result.Votes.Count == electionObj.Votes.Count)
                {
                    UOW.SaveChanges();
                }
            }
            finally
            {
                UOW.CloseTransaction();
            }

            return result;
        }
    }
}
