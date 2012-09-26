using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Transactions;
using Common.Security;

namespace Common.Infrastructure.Data
{
    public class DatabaseUnitOfWork : IUnitOfWork
    {
        private DbContext _database;
        private TransactionScope _transaction;
        private ISecurityContext _securityContext;

        public DatabaseUnitOfWork(DbContext database, ISecurityContext securityContext)
        {
            _database = database;
            _securityContext = securityContext;
        }

        public void Start()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }
            _transaction = new TransactionScope();
        }

        public void Complete()
        {
            _transaction.Complete();
        }

        public void Rollback()
        {
            
        }

        public IRepository<T> GetRepository<T>() 
            where T : DataRow, new()
        {
            return new DatabaseRepository<T>(_database, _securityContext);
        }

        public void Dispose()
        {
            _database.Dispose();
            _transaction.Dispose();
        }
    }
}
