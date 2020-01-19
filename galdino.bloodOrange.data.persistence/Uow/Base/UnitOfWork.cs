using galdino.bloodOrange.application.shared.Interfaces.IUnitOfWork;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace galdino.bloodOrange.data.persistence.Uow.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Methods

        private readonly string _connectionString;
        private IDbConnection Connection;
        private IDbTransaction Transaction;
        protected UnitOfWork(string conectionString)
        {
            _connectionString = conectionString;
        }
        public void Begin()
        {
            if (Connection == null)
            {
                CreateConnection();
            }

            Transaction = Connection.BeginTransaction();
        }
        private void CreateConnection()
        {
            Connection = new NpgsqlConnection(_connectionString);
            Connection.Open();
        }

        public void Commit()
        {
            try
            {
                Transaction.Commit();
            }
            catch
            {
                Transaction?.Rollback();
                throw;
            }
            finally
            {
                Transaction?.Dispose();
                Transaction = null;
            }
        }

        public void RollBack()
        {
            try
            {
                Transaction.Rollback();
            }
            finally
            {
                Transaction?.Dispose();
                Transaction = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Connection != null)
                {
                    try
                    {
                        Connection.Close();
                        Connection.Dispose();
                    }
                    catch
                    {
                        //Não tratar
                    }
                }
            }
        }

        ~UnitOfWork()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IDbConnection GetConnection()
        {
            AguardarComando();
            if (Connection != null) return Connection;
            CreateConnection();
            return Connection;
        }

        public IDbTransaction GetTransaction()
        {
            return Transaction;
        }

        public void Release()
        {
            AguardarComando();
        }

        private void AguardarComando()
        {
            if (Connection == null) return;
            while (Connection.State == ConnectionState.Executing || Connection.State == ConnectionState.Fetching)
            {
                System.Threading.Thread.Sleep(500);
            }
        }
    }
    #endregion
}
