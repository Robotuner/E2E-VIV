using ElectionModels;
using ElectionAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartyController : BaseController
    {
        private readonly IPartyRepository partyRepository;
        public PartyController(IConfiguration config, IPartyRepository partyRepository) : base(config)
        {
            this.partyRepository = partyRepository;
        }

        [HttpGet]
        public async Task<List<Party>> Get()
        {
            List<Party> result = await this.partyRepository.GetAll(Context);
            return result;
        }

        [HttpPost]
        public async Task<Party> Create([FromBody] Party electionObj)
        {
            return await this.partyRepository.Insert(Context, electionObj);
        }


        [HttpGet("{Id}")]
        public async Task<Party> GetById(int Id)
        {
            Party result = await this.partyRepository.GetByID(Context, Id);
            return result;
        }


        [HttpDelete("{Id}")]
        public async Task<bool> Delete(int Id)
        {
            Party result = await this.partyRepository.Delete(Context, Id);
            return true;
        }

        [HttpPut]
        public async Task<Party> Update([FromBody] Party categoryType)
        {
            Party result = await this.partyRepository.Update(Context, categoryType);
            return result;
        }
    }
}
