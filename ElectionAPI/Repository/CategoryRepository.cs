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
        Task<Category> Delete(IDbConnection context, Guid id);
        Task<Category> Insert(IDbConnection context, Category host);
        Task<Category> Update(IDbConnection context, Category host);
        Task<List<Category>> GetAll(IDbConnection context);
        Task<Category> GetByID(IDbConnection context, Guid id);
        Task<List<Category>> GetByElection(IDbConnection context, Guid electionId);
        Task<List<Category>> GetByType(IDbConnection context, Guid electionId, int type);
        //Task<List<Category>> GetBySlate(IDbConnection context, Guid electionId, Guid slateId);
        //Task<List<Category>> GetBySlateType(IDbConnection context, Guid electionId, Guid slateId, int type);
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

        public async Task<List<Category>> GetAll(IDbConnection context)
        {
            List<Category> result = null;
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

        public async Task<Category> Insert(IDbConnection context, Category category)
        {
            Category result = null;
            try
            {
                Category foundCategory = await this.categoryService.GetByID(context, category.Id);
                if (foundCategory != null)
                {
                    result = await this.categoryService.Update(context, category);
                }
                else
                {
                    result = await this.categoryService.Insert(context, category);
                }
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<Category> Update(IDbConnection context, Category election)
        {
            Category result = null;
            try
            {
                result = await this.categoryService.Update(context, election);
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<Category> Delete(IDbConnection context, Guid id)
        {
            _logger.LogInformation(string.Format("CategoryRepository: Delete {0}", id));

            Category result = await this.categoryService.Delete(context, id);
            return result;
        }
    }
}
