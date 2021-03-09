using Dapper;
using ElectionAPI.DataContext;
using ElectionModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ElectionAPI.Service
{
    public interface ITicketService
    {
        Task<Ticket> Delete(IDbConnection context, Guid id);
        Task<Ticket> Insert(IDbConnection context, Ticket Host);
        Task<Ticket> Update(IDbConnection context, Ticket Host);
        Task<IEnumerable<Ticket>> GetByElection(IDbConnection context,Guid electionId);
        Task<Ticket> GetByID(IDbConnection context, Guid id);
    }

    public class TicketService : BaseService, ITicketService
    {
        public TicketService()
        {

        }

        public async Task<Ticket> GetByID(IDbConnection context, Guid id)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@id", id, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);

                List<Ticket> ans = (await context.QueryAsync<Ticket>(sql: "Ticket_GetById", param: p,
                    commandType: System.Data.CommandType.StoredProcedure)).ToList();
                return ans.FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Ticket>> GetByElection(IDbConnection context, Guid electionId)
        {
            IEnumerable<Ticket> result = null;
            try
            {
                var p = new DynamicParameters();
                p.Add("@electionid", electionId, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);

                result = await context.QueryAsync<Ticket>(sql: "Ticket_GetByElection", param: p, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch 
            {
                throw;
            }

            return result;
        }

        private DynamicParameters SetParam(Ticket ticket)
        {
            if (ticket.Id == Guid.Empty)
            {
                ticket.Id = Guid.NewGuid();
            }
            var p = new DynamicParameters();
            p.Add("@id", ticket.Id, DbType.Guid, ParameterDirection.Input);
            p.Add("@partyId", ticket.PartyId, DbType.Int32, ParameterDirection.Input);
            p.Add("@electionid", ticket.ElectionId, DbType.Guid, ParameterDirection.Input);
            p.Add("@categoryid", ticket.CategoryId, DbType.Guid, ParameterDirection.Input);
            p.Add("@information", ticket.Information, DbType.String, ParameterDirection.Input);
            p.Add("@description", ticket.Description, DbType.String, ParameterDirection.Input);
            p.Add("@ticketType", ticket.TicketType, DbType.Int32, ParameterDirection.Input);
            p.Add("@sequence", ticket.Sequence, DbType.Int32, ParameterDirection.Input);

            return p;
        }

        public async Task<Ticket> Insert(IDbConnection context, Ticket election)
        {
            Ticket result = null;
            try
            {
                if (election.Id == Guid.Empty)
                {
                    election.Id = Guid.NewGuid();
                }
                result = await context.QuerySingleAsync<Ticket>(sql: "Ticket_Insert", param: SetParam(election), 
                    commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public async Task<Ticket> Update(IDbConnection context, Ticket ticket)
        {
            try
            {
                Ticket foundTicket = await GetByID(context, ticket.Id);
                if (foundTicket != null)
                {
                    this.Changes = changeLogService.GetChanges(ticket, foundTicket);
                    if (Changes.Count > 0)
                    {
                        List<Ticket> result = (await context.QueryAsync<Ticket>(sql: "Ticket_Update", param: SetParam(ticket),
                            commandType: System.Data.CommandType.StoredProcedure)).ToList();
                        return result.FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return ticket;
        }


        public async Task<Ticket> Delete(IDbConnection context, Guid id)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@id", id, DbType.Guid, ParameterDirection.Input);

                List<Ticket> result = (await context.QueryAsync<Ticket>(sql: "Ticket_Delete", param: p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
                return result.FirstOrDefault();
            }
            catch 
            {
                throw;
            }
        }
    }
}
