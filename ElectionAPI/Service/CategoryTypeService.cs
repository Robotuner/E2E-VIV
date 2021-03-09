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
    public interface ICategoryTypeService
    {
        Task<CategoryType> Delete(IUnitOfWork uow, int id);
        Task<CategoryType> Insert(IUnitOfWork uow, CategoryType Host);
        Task<CategoryType> Update(IUnitOfWork uow, CategoryType Host);
        Task<IEnumerable<CategoryType>> GetAll(IDbConnection context);
        Task<CategoryType> GetByID(IDbConnection context, int id);
    }

    public class CategoryTypeService : ICategoryTypeService
    {
        public CategoryTypeService()
        {

        }

        public async Task<IEnumerable<CategoryType>> GetAll(IDbConnection context)
        {
            IEnumerable<CategoryType> result = new List<CategoryType>();
            try
            {
                var p = new DynamicParameters();
                p.Add("@active", true, System.Data.DbType.Boolean, System.Data.ParameterDirection.Input);

                result = await context.QueryAsync<CategoryType>(sql: "CategoryType_Get", commandType: System.Data.CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<CategoryType> GetByID(IDbConnection context, int id)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@id", id, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

                List<CategoryType> result = (await context.QueryAsync<CategoryType>(sql: "CategoryType_GetById", param: p,
                    commandType: System.Data.CommandType.StoredProcedure)).ToList();

                return result.FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }


        private DynamicParameters SetParam(CategoryType data)
        {

            var p = new DynamicParameters();
            p.Add("@id", data.Id, DbType.Int32, ParameterDirection.Input);
            p.Add("@description", data.Description, DbType.String, ParameterDirection.Input);
            p.Add("@active", data.Active, DbType.Boolean, ParameterDirection.Input);

            return p;
        }

        public async Task<CategoryType> Insert(IUnitOfWork uow, CategoryType election)
        {
            CategoryType result = null;
            try
            {
                var p = new DynamicParameters();      
                p.Add("@description", election.Description, DbType.String, ParameterDirection.Input);

                result = await uow.Context.QuerySingleAsync<CategoryType>(sql: "CategoryType_Insert", param: p, commandType: System.Data.CommandType.StoredProcedure, transaction: uow.Trans);
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public async Task<CategoryType> Update(IUnitOfWork uow, CategoryType categoryType)
        {
            CategoryType result = null;
            try
            {
                await uow.Context.QueryAsync<CategoryType>(sql: "CategoryType_Update", param: SetParam(categoryType), commandType: System.Data.CommandType.StoredProcedure, transaction: uow.Trans);
                result = await this.GetByID(uow.Context, categoryType.Id);
            }
            catch 
            {
                throw;
            }

            return result;
        }


        public async Task<CategoryType> Delete(IUnitOfWork uow, int id)
        {
            CategoryType result = null;
            try
            {
                var p = new DynamicParameters();
                p.Add("@id", id, DbType.Int32, ParameterDirection.Input);

                await uow.Context.QueryAsync<CategoryType>(sql: "CategoryType_Delete", param: p, commandType: System.Data.CommandType.StoredProcedure, transaction: uow.Trans);
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
