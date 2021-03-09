using ElectionAPI.DataContext;
using ElectionModels;
using ElectionAPI.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ElectionAPI.Repository
{
    public interface ITicketRepository
    {
        Task<Ticket> Delete(IDbConnection context, Guid id);
        Task<Ticket> Insert(IDbConnection context, Ticket host);
        Task<Ticket> Update(IDbConnection context, Ticket host);
        Task<List<Ticket>> GetByElection(IDbConnection context, Guid electionId);
        Task<Ticket> GetByID(IDbConnection context, Guid id);
    }

    public class TicketRepository : BaseRepository, ITicketRepository
    {
        private readonly ILogger<TicketRepository> _logger;
        private readonly ITicketService ticketService;
        public TicketRepository(ILogger<TicketRepository> logger, ITicketService ticketService)
        {
            this._logger = logger;
            this.ticketService = ticketService;
        }

        public async Task<List<Ticket>> GetByElection(IDbConnection context, Guid electionId)
        {
            List<Ticket> result = null;
            try
            {
                result = (await this.ticketService.GetByElection(context, electionId))?.ToList();
            }
            catch 
            {
                throw;
            }

            return result;
        }

        public async Task<Ticket> GetByID(IDbConnection context, Guid id)
        {
            Ticket result = null;
            try
            {

                result = await this.ticketService.GetByID(context, id);
            }
            catch 
            {
                throw;
            }

            return result;
        }

        public async Task<Ticket> Insert(IDbConnection context, Ticket ticket)
        {
            Ticket result = null;
            try
            {
                Ticket foundTicket = await this.GetByID(context, ticket.Id);
                if (foundTicket != null)
                {
                    result = await ticketService.Update(context, ticket);
                }
                else
                {
                    result = await this.ticketService.Insert(context, ticket);
                }
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<Ticket> Update(IDbConnection context, Ticket election)
        {
            Ticket result = null;
            try
            {
                result = await this.ticketService.Update(context, election);
            }
            catch 
            {
                throw;
            }

            return result;
        }

        public async Task<Ticket> Delete(IDbConnection context, Guid id)
        {
            _logger.LogInformation(string.Format("TicketRepository: Delete {0}", id));

            Ticket result = await this.ticketService.Delete(context, id);
            return result;
        }
    }
}
