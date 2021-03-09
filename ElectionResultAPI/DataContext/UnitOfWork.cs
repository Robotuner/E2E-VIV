using System;
using System.Data;

namespace ElectionResultAPI.DataContext
{
    public interface IUnitOfWork
    {
        IDbConnection Context { get; set; }
        IDbTransaction Trans { get; set; }

        void BeginTransaction();
        void CloseTransaction();
        void SaveChanges();
    }

    public class UnitOfWork : IUnitOfWork
    {
        public IDbConnection Context { get; set; }
        public IDbTransaction Trans { get; set; }

        public UnitOfWork(IDbConnection connect)
        {
            this.Context = connect;
            this.Trans = null;
        }

        public void BeginTransaction()
        {
            if (this.Context.State == ConnectionState.Closed)
                this.Context.Open();

            this.Trans = this.Context.BeginTransaction();
        }

        public void SaveChanges()
        {
            if (this.Trans == null)
                return;

            try
            {
                this.Trans.Commit();
            }
            catch (Exception ex)
            {
                this.Trans.Rollback();
            }
        }

        public void CloseTransaction()
        {
            this.Trans.Dispose();
            this.Context.Close();
        }
    }
}
