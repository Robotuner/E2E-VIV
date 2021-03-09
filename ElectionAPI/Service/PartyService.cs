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
    public interface IPartyService
    {
        Task<Party> Delete(IDbConnection context, int id);
        Task<Party> Insert(IDbConnection context, Party Host);
        Task<Party> Update(IDbConnection context, Party Host);
        Task<IEnumerable<Party>> GetAll(IDbConnection context);
        Task<Party> GetByID(IDbConnection context, int id);
    }

    public class PartyService : IPartyService
    {
        public PartyService()
        {

        }

        public async Task<IEnumerable<Party>> GetAll(IDbConnection context)
        {
            IEnumerable<Party> result = new List<Party>();
            try
            {
                var p = new DynamicParameters();
                p.Add("@active", true, System.Data.DbType.Boolean, System.Data.ParameterDirection.Input);

                result = await context.QueryAsync<Party>(sql: "Party_Get", commandType: System.Data.CommandType.StoredProcedure);
            }
            catch 
            {
                throw;
            }

            return result;
        }

        public async Task<Party> GetByID(IDbConnection context, int id)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@id", id, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

                List<Party> result = (await context.QueryAsync<Party>(sql: "Party_GetById", param: p, 
                    commandType: System.Data.CommandType.StoredProcedure)).ToList();
                return result.FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        private DynamicParameters SetParam(Party data)
        {
            var p = new DynamicParameters();
            p.Add("@id", data.Id, DbType.Int32, ParameterDirection.Input);           
            p.Add("@active", data.Active, DbType.Boolean, ParameterDirection.Input);
            p.Add("@description", data.Description, DbType.String, ParameterDirection.Input);

            return p;
        }

        public async Task<Party> Insert(IDbConnection context, Party party)
        {
            Party result = null;
            try
            {
                var p = new DynamicParameters();
                p.Add("@description", party.Description, DbType.String, ParameterDirection.Input);

                result = await context.QuerySingleAsync<Party>(sql: "Party_Insert", param: p, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public async Task<Party> Update(IDbConnection context, Party party)
        {
            Party result = null;
            try
            {
                await context.QueryAsync<Party>(sql: "Party_Update", param: SetParam(party), commandType: System.Data.CommandType.StoredProcedure);
                result = await this.GetByID(context, party.Id);
            }
            catch
            {
                throw;
            }

            return result;
        }


        public async Task<Party> Delete(IDbConnection context, int id)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@id", id, DbType.Int32, ParameterDirection.Input);

                await context.QueryAsync<Party>(sql: "Party_Delete", param: p, commandType: System.Data.CommandType.StoredProcedure);
                Party result = await this.GetByID(context, id);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
