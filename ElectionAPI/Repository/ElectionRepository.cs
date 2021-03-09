using Dapper;
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
    public interface IElectionRepository
    {
        Task<Election> Delete(IDbConnection context, Guid id);
        Task<Election> Insert(IDbConnection context, Election host);
        Task<Election> Update(IDbConnection context, Election host);
        Task<List<Election>> GetAll(IDbConnection context);
        Task<Election> GetByID(IDbConnection context, Guid id);
        Task<Election> GetFullElection(IDbConnection context, Guid id);
        Task<Election> SaveAllElection(IDbConnection uow, Election election);
    }

    public class ElectionRepository : BaseRepository, IElectionRepository
    {
        private readonly ILogger<ElectionRepository> _logger;
        private readonly IElectionService electionService;
        public ElectionRepository(ILogger<ElectionRepository> logger, IElectionService electionService)
        {
            this._logger = logger;
            this.electionService = electionService;
        }

        public async Task<List<Election>> GetAll(IDbConnection context)
        {
            List<Election> result = null;
            try
            {
                result = (await this.electionService.GetAll(context))?.ToList();
            }
            catch 
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

                result = await this.electionService.GetByID(context, id);
            }
            catch
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
                result = await this.electionService.GetFullElection(context, id);
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
            p.Add("@description", election.Description, DbType.String, ParameterDirection.Input);

            return p;
        }

        public async Task<Election> SaveAllElection(IDbConnection context, Election election)
        {
            Election result = null;
            try
            {
                result = await this.electionService.SaveAllElection(context, election);
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<Election> Insert(IDbConnection context, Election election)
        {
            Election result = null;
            try
            {
                result = await this.electionService.Insert(context, election);
            }
            catch 
            {
                throw;
            }

            return result;
        }

        public async Task<Election> Update(IDbConnection context, Election election)
        {
            Election result = null;
            try
            {
                result = await this.electionService.Update(context, election);
            }
            catch 
            {
                throw;
            }

            return result;
        }

        public async Task<Election> Delete(IDbConnection context, Guid id)
        {
            _logger.LogInformation(string.Format("ElectionRepository: Delete {0}", id));

            Election result = await this.electionService.Delete(context, id);
            return result;
        }
    }
}
