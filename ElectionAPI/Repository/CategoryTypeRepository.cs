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
    public interface ICategoryTypeRepository
    {
        Task<CategoryType> Delete(IUnitOfWork uow, int id);
        Task<CategoryType> Insert(IUnitOfWork uow, CategoryType host);
        Task<CategoryType> Update(IUnitOfWork uow, CategoryType host);
        Task<List<CategoryType>> GetAll(IDbConnection context);
        Task<CategoryType> GetByID(IDbConnection context, int id);
    }

    public class CategoryTypeRepository : BaseRepository, ICategoryTypeRepository
    {
        private readonly ILogger<CategoryTypeRepository> _logger;
        private readonly ICategoryTypeService categoryTypeService;
        public CategoryTypeRepository(ILogger<CategoryTypeRepository> logger, ICategoryTypeService categoryTypeService)
        {
            this._logger = logger;
            this.categoryTypeService = categoryTypeService;
        }

        public async Task<List<CategoryType>> GetAll(IDbConnection context)
        {
            List<CategoryType> result = null;
            try
            {
                result = (await this.categoryTypeService.GetAll(context))?.ToList();
            }
            catch 
            {
                throw;
            }

            return result;
        }

        public async Task<CategoryType> GetByID(IDbConnection context, int id)
        {
            CategoryType result;
            try
            {

                result = await this.categoryTypeService.GetByID(context, id);
            }
            catch 
            {
                throw;
            }

            return result;
        }

        private DynamicParameters SetParam(CategoryType categoryType)
        {
            var p = new DynamicParameters();
            p.Add("@id", categoryType.Id, DbType.Int32, ParameterDirection.Input);
            p.Add("@description", categoryType.Description, DbType.String, ParameterDirection.Input);
            p.Add("@active", categoryType.Active, DbType.Boolean, ParameterDirection.Input);

            return p;
        }

        public async Task<CategoryType> Insert(IUnitOfWork uow, CategoryType categoryType)
        {
            CategoryType result;
            try
            {
                result = await this.categoryTypeService.Insert(uow, categoryType);
            }
            catch 
            {
                throw;
            }

            return result;
        }

        public async Task<CategoryType> Update(IUnitOfWork uow, CategoryType categoryType)
        {
            CategoryType result;
            try
            {
                result = await this.categoryTypeService.Update(uow, categoryType);
            }
            catch 
            {
                throw;
            }

            return result;
        }

        public async Task<CategoryType> Delete(IUnitOfWork uow, int id)
        {
            _logger?.LogInformation(string.Format("CategoryTypeRepository: Delete {0}", id));

            CategoryType result = await this.categoryTypeService.Delete(uow, id);
            return result;
        }
    }
}
