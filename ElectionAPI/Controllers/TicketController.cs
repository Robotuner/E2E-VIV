using ElectionModels;
using ElectionAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : BaseController
    {
        private readonly ITicketRepository ticketRepository;
        public TicketController(IConfiguration config, ITicketRepository ticketRepository) : base(config)
        {
            this.ticketRepository = ticketRepository;
        }

        [HttpGet("ByElection/{Id}")]
        public async Task<List<Ticket>> GetByElection(Guid Id)
        {
            List<Ticket> result = await this.ticketRepository.GetByElection(Context,Id);
            return result;
        }

        [HttpPost]
        public async Task<Ticket> Create([FromBody] Ticket electionObj)
        {
            return await this.ticketRepository.Insert(Context, electionObj);
        }


        [HttpGet("{Id}")]
        public async Task<Ticket> GetById(Guid Id)
        {
            Ticket result = await this.ticketRepository.GetByID(Context, Id);
            return result;
        }


        [HttpDelete("{Id}")]
        public async Task<bool> Delete(Guid Id)
        {
            Ticket result = await this.ticketRepository.Delete(Context, Id);
            return true;
        }

        [HttpPut("{Id}")]
        public async Task<Ticket> Update([FromBody] Ticket categoryType)
        {
            Ticket result = await this.ticketRepository.Update(Context, categoryType);
            return result;
        }
    }
}
