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
    public interface IElectionService
    {
        Task<Election> Delete(IDbConnection context, Guid id);
        Task<Election> Insert(IDbConnection context, Election Host);
        Task<Election> SaveAllElection(IDbConnection context, Election election);
        Task<Election> Update(IDbConnection context, Election Host);
        Task<IEnumerable<Election>> GetAll(IDbConnection context);
        Task<Election> GetByID(IDbConnection context, Guid id);
        Task<Election> GetFullElection(IDbConnection context, Guid id);
    }

    public class ElectionService : BaseService, IElectionService
    {
        private readonly ICategoryService categoryService;
        private readonly ICategoryTypeService categoryTypeService;
        private readonly IPartyService partyService;
        private readonly ITicketService ticketService;
        public ElectionService(ICategoryService categoryService, 
            ICategoryTypeService categoryTypeService, IPartyService partyService,
            ITicketService ticketService)
        {
            this.categoryService = categoryService;
            this.categoryTypeService = categoryTypeService;
            this.partyService = partyService;
            this.ticketService = ticketService;
        }

        public async Task<IEnumerable<Election>> GetAll(IDbConnection context) 
        {
            IEnumerable<Election> result = new List<Election>();
            try
            {
                var p = new DynamicParameters();
                p.Add("@date", DateTime.UtcNow, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);

                result = await context.QueryAsync<Election>(sql: "Election_Get", param: p, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public async Task<Election> GetByID(IDbConnection context, Guid id)
        {
            Election result = null;
            try
            {
                var p = new DynamicParameters();
                p.Add("@id", id, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);

                var ans = await context.QueryAsync<Election>(sql: "Election_GetById", param: p, 
                    commandType: System.Data.CommandType.StoredProcedure);
                if (ans.Count() > 0)
                    return ans.First();
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public async Task<Election> GetFullElection(IDbConnection context, Guid id)
        {
            Election result = null;
            try
            {
                List<Category> categories = (await this.categoryService.GetByElection(context, id))?.ToList();
                List<Ticket> tickets = (await this.ticketService.GetByElection(context, id))?.ToList();
                List<Party> parties = (await this.partyService.GetAll(context))?.ToList();
        
                foreach(Category category in categories.OrderBy(n => n.CategoryTypeId))
                {
                    category.Tickets = tickets.Where(n => n.CategoryId == category.Id)?.ToList();
                    foreach(Ticket ticket in category.Tickets.OrderBy(n => n.Sequence))
                    {
                        if (ticket.PartyId.HasValue)
                        {
                            ticket.Party = parties.SingleOrDefault(n => n.Id == ticket.PartyId.Value)?.Description;
                        }
                    }
                }

                result = await this.GetByID(context, id);
                if (result != null)
                {
                    result.PartyList = parties;
                    result.CategoryList = categories;
                }
            }
            catch 
            {
                throw;
            }

            return result;
        }


        private DynamicParameters SetParam(Election election)
        {
            if (election.Id == Guid.Empty)
            {
                election.Id = Guid.NewGuid();
            }
            var p = new DynamicParameters();
            p.Add("@id", election.Id, DbType.Guid, ParameterDirection.Input);
            p.Add("@date", election.Date, DbType.DateTime, ParameterDirection.Input);
            p.Add("@startdate", election.StartDateLocal, DbType.DateTime, ParameterDirection.Input);
            p.Add("@enddate", election.EndDateLocal, DbType.DateTime, ParameterDirection.Input);
            p.Add("@description", election.Description, DbType.String, ParameterDirection.Input);
            p.Add("@version", election.Version, DbType.String, ParameterDirection.Input);
            p.Add("@allowUpdates", election.AllowUpdates, DbType.Boolean, ParameterDirection.Input);
            return p;
        }

        public async Task<Election> SaveAllElection(IDbConnection context, Election election)
        {
            try
            {
                //await SaveParty(context, election.PartyList);
                await SaveElectionTable(context, election);
                await SaveCategorys(context, election.CategoryList);
                return election;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        public async Task<Election> Insert(IDbConnection context, Election election) 
        {
            Election result = null;
            try
            {
                result = await context.QuerySingleAsync<Election>(sql: "Election_Insert", param: SetParam(election), 
                    commandType: System.Data.CommandType.StoredProcedure);      
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public async Task<Election> Update(IDbConnection context, Election election) 
        {
            try
            {
                Election foundElection = await GetByID(context, election.Id);
                if (foundElection != null)
                {
                    this.Changes = changeLogService.GetChanges(election, foundElection);
                    if (Changes.Count > 0)
                    {
                        List<Election> result = (await context.QueryAsync<Election>(sql: "Election_Update", param: SetParam(election),
                                commandType: System.Data.CommandType.StoredProcedure)).ToList();
                        return result.FirstOrDefault();
                    }
                }
            }
            catch
            {
                throw;
            }

            return election;
        }

        /// <summary>
        /// This will delete all categorys and tickets associated with the election!
        /// </summary>
        public async Task<Election> Delete(IDbConnection context, Guid id)
        {
            Election result = null;
            try
            {
                var p = new DynamicParameters();
                p.Add("@id", id, DbType.Guid, ParameterDirection.Input);

                await context.QueryAsync<Election>(sql: "Election_Delete", param: p, commandType: System.Data.CommandType.StoredProcedure);
                result = await this.GetByID(context, id);
            }
            catch 
            {
                throw;
            }

            return result;
        }

        private async Task SaveElectionTable(IDbConnection context, Election election)
        {
            try
            {
                Election foundElection = await GetByID(context, election.Id);
                if (foundElection == null)
                {
                    await context.QuerySingleAsync<Election>(sql: "Election_Insert", param: SetParam(election),
                        commandType: System.Data.CommandType.StoredProcedure);
                }
                else
                {
                    await context.QueryAsync<Election>(sql: "Election_Update", param: SetParam(election),
                        commandType: System.Data.CommandType.StoredProcedure);

                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        private async Task SaveParty(IDbConnection context, List<Party> partyList)
        {
            if (partyList == null)
                return;

            foreach (Party party in partyList)
            {
                Party foundParty = await partyService.GetByID(context, party.Id);
                if (foundParty == null)
                {
                    await partyService.Insert(context, party);
                }
                else
                {
                    await partyService.Update(context, party);
                }
            }
        }

        private async Task SaveCategorys(IDbConnection context, List<Category> categorys)
        {
            foreach(Category cat in categorys)
            {
                Category foundCategory = null;
                foundCategory = await categoryService.GetByID(context, cat.Id);
                if (foundCategory == null)
                {
                    Category result = await this.categoryService.Insert(context, cat);
                }
                else
                {
                    await this.categoryService.Update(context, cat);
                }

                foreach (Ticket ticket in cat.Tickets)
                {
                    Ticket foundTicket = null;
                    foundTicket = await ticketService.GetByID(context, ticket.Id);
                    if (foundTicket == null)
                    {
                        await ticketService.Insert(context, ticket);
                    }
                    else
                    {
                        await ticketService.Update(context, ticket);
                    }
                }
            }

        }
    }
}
