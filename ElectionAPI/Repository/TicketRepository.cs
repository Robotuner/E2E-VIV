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
        Task<Ticket> Delete(IUnitOfWork uow, Guid id);
        Task<Ticket> Insert(IUnitOfWork uow, Ticket host);
        Task<Ticket> Update(IUnitOfWork uow, Ticket host);
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

        public async Task<Ticket> Insert(IUnitOfWork uow, Ticket ticket)
        {
            Ticket result = null;
            try
            {
                Ticket foundTicket = await this.GetByID(uow.Context, ticket.Id);
                if (foundTicket != null)
                {
                    result = await ticketService.Update(uow, ticket);
                }
                else
                {
                    result = await this.ticketService.Insert(uow, ticket);
                }
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<Ticket> Update(IUnitOfWork uow, Ticket election)
        {
            Ticket result = null;
            try
            {
                result = await this.ticketService.Update(uow, election);
            }
            catch 
            {
                throw;
            }

            return result;
        }

        public async Task<Ticket> Delete(IUnitOfWork uow, Guid id)
        {
            _logger?.LogInformation(string.Format("TicketRepository: Delete {0}", id));

            Ticket result = await this.ticketService.Delete(uow, id);
            return result;
        }
    }
}
