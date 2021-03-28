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
    public interface ICategoryRepository
    {
        Task<Category> Delete(IUnitOfWork uow, Guid id);
        Task<Category> Insert(IUnitOfWork uow, Category host);
        Task<Category> Update(IUnitOfWork uow, Category host);

        Task<Category> GetByID(IDbConnection context, Guid id);
        Task<List<Category>> GetByElection(IDbConnection context, Guid electionId);
        Task<List<Category>> GetByType(IDbConnection context, Guid electionId, int type);
    }

    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        private readonly ILogger<CategoryRepository> _logger;
        private readonly ICategoryService categoryService;
        public CategoryRepository(ILogger<CategoryRepository> logger, ICategoryService categoryService)
        {
            this._logger = logger;
            this.categoryService = categoryService;
            
        }

        public async Task<Category> GetByID(IDbConnection context, Guid id)
        {
            Category result = null;
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

        public async Task<List<Category>> GetByElection(IDbConnection context, Guid electionId)
        {
            List<Category> result = null;
            try
            {
                result = (await this.categoryService.GetByElection(context, electionId))?.ToList();
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<List<Category>> GetByType(IDbConnection context, Guid electionId, int type)
        {
            List<Category> result = null;
            try
            {
                result = (await this.categoryService.GetByType(context, electionId, type))?.ToList();
            }
            catch 
            {
                throw;
            }

            return result;
        }

        public async Task<Category> Insert(IUnitOfWork uow, Category category)
        {
            Category result = null;
            try
            {
                Category foundCategory = await this.categoryService.GetByID(uow.Context, category.Id);
                if (foundCategory != null)
                {
                    result = await this.categoryService.Update(uow, category);
                }
                else
                {
                    result = await this.categoryService.Insert(uow, category);
                }
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<Category> Update(IUnitOfWork uow, Category election)
        {
            Category result = null;
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

        public async Task<Category> Delete(IUnitOfWork uow, Guid id)
        {
            _logger?.LogInformation(string.Format("CategoryRepository: Delete {0}", id));

            Category result = await this.categoryService.Delete(uow, id);
            return result;
        }
    }
}
