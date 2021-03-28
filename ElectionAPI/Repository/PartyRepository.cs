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
    public interface IPartyRepository
    {
        Task<Party> Delete(IUnitOfWork uow, int id);
        Task<Party> Insert(IUnitOfWork uow, Party host);
        Task<Party> Update(IUnitOfWork uow, Party host);
        Task<List<Party>> GetAll(IDbConnection context);
        Task<Party> GetByID(IDbConnection context, int id);
    }

    public class PartyRepository : BaseRepository, IPartyRepository
    {
        private readonly ILogger<PartyRepository> _logger;
        private readonly IPartyService categoryService;
        public PartyRepository(ILogger<PartyRepository> logger, IPartyService categoryService)
        {
            this._logger = logger;
            this.categoryService = categoryService;
        }

        public async Task<List<Party>> GetAll(IDbConnection context)
        {
            List<Party> result = null;
            try
            {
                result = (await this.categoryService.GetAll(context))?.ToList();
            }
            catch 
            {
                throw;
            }

            return result;
        }

        public async Task<Party> GetByID(IDbConnection context, int id)
        {
            Party result = null;
            try
            {

                result = await this.categoryService.GetByID(context, id);
            }
            catch 
            {
                throw;
            }

            return result;
        }

        public async Task<Party> Insert(IUnitOfWork uow, Party election)
        {
            Party result = null;
            try
            {
                result = await this.categoryService.Insert(uow, election);
            }
            catch 
            {
                throw;
            }

            return result;
        }

        public async Task<Party> Update(IUnitOfWork uow, Party election)
        {
            Party result = null;
            try
            {
                result = await this.categoryService.Update(uow, election);
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<Party> Delete(IUnitOfWork uow, int id)
        {
            _logger?.LogInformation(string.Format("CategoryRepository: Delete {0}", id));

            Party result = await this.categoryService.Delete(uow, id);
            return result;
        }
    }
}
