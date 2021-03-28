using Dapper;
using ElectionAPI.DataContext;
using ElectionModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ElectionAPI.Service
{
    public interface ICategoryService
    {
        Task<Category> Delete(IUnitOfWork uow, Guid id);
        Task<Category> Insert(IUnitOfWork uowt, Category Host);
        Task<Category> Update(IUnitOfWork uow, Category Host);

        Task<Category> GetByID(IDbConnection context, Guid id);

        Task<IEnumerable<Category>> GetByElection(IDbConnection context, Guid electionId);
        Task<IEnumerable<Category>> GetByType(IDbConnection context, Guid electionId, int type);

    }
    public class CategoryService : BaseService, ICategoryService
    {
        public CategoryService()
        {

        }

        public async Task<Category> GetByID(IDbConnection context, Guid id)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@id", id, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);

                List<Category> ans = (await context.QueryAsync<Category>(sql: "Category_GetById", param: p,
                    commandType: System.Data.CommandType.StoredProcedure)).ToList();

                return ans.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<IEnumerable<Category>> GetByElection(IDbConnection context, Guid electionId)
        {
            IEnumerable<Category> result = new List<Category>();
            try
            {
                var p = new DynamicParameters();
                p.Add("@electionid", electionId, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);
  
                result = await context.QueryAsync<Category>(sql: "Category_GetByElection", param: p, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

            return result;
        }

        private void CatMessage(Category c)
        {
            string ans = string.Format("{0} | {1} | {2}", c.Id, c.Heading, c.Title);
            Debug.WriteLine(ans);
        }

        public async Task<IEnumerable<Category>> GetByType(IDbConnection context, Guid electionId, int type)
        {
            IEnumerable<Category> result = new List<Category>();
            try
            {
                var p = new DynamicParameters();
                p.Add("@electionid", electionId, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);
                p.Add("@type", type, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

                result = await context.QueryAsync<Category>(sql: "Category_GetByType", param: p, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        private DynamicParameters SetParam(Category data)
        {
            if (data.Id == Guid.Empty)
            {
                data.Id = Guid.NewGuid();
            }
            var p = new DynamicParameters();
            p.Add("@id", data.Id, DbType.Guid, ParameterDirection.Input);
            p.Add("@categorytypeid", data.CategoryTypeId, DbType.Int32, ParameterDirection.Input);
            p.Add("@electionid",data.ElectionId, DbType.Guid, ParameterDirection.Input);
            p.Add("@heading", data.Heading, DbType.String, ParameterDirection.Input);
            p.Add("@title", data.Title, DbType.String, ParameterDirection.Input);
            p.Add("@judgeposition", data.JudgePosition, DbType.Int32, ParameterDirection.Input);
            p.Add("@information", data.Information, DbType.String, ParameterDirection.Input);
            p.Add("@subtitle", data.SubTitle, DbType.String, ParameterDirection.Input);
            p.Add("@sequence", data.Sequence, DbType.Int32, ParameterDirection.Input);
            p.Add("@selection", data.Selection, DbType.Guid, ParameterDirection.Input);
            return p;
        }

        public async Task<Category> Insert(IUnitOfWork uow, Category category)
        {
            try
            {
                List<Category> ans = (await uow.Context.QueryAsync<Category>(sql: "Category_Insert", param: SetParam(category), 
                            commandType: System.Data.CommandType.StoredProcedure, transaction: uow.Trans)).ToList();
                return ans.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Category> Update(IUnitOfWork uow, Category category)
        {
            try
            {
                CatMessage(category);
                Category foundCategory = await GetByID(uow.Context, category.Id);
                if (foundCategory != null)
                {
                    this.Changes = changeLogService.GetChanges(category, foundCategory);
                    if (Changes.Count > 0)
                    {
                        List<Category> result = (await uow.Context.QueryAsync<Category>(sql: "Category_Update", param: SetParam(category), 
                            commandType: System.Data.CommandType.StoredProcedure, transaction: uow.Trans)).ToList();
                        return result.FirstOrDefault();
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }

            return category;
        }


        public async Task<Category> Delete(IUnitOfWork uow, Guid id)
        {
            Category result = null;
            try
            {
                var p = new DynamicParameters();
                p.Add("@id", id, DbType.Guid, ParameterDirection.Input);

                await uow.Context.QueryAsync<Category>(sql: "Category_Delete", param: p, commandType: System.Data.CommandType.StoredProcedure, transaction: uow.Trans);
                result = await this.GetByID(uow.Context, id);
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

    }
}
