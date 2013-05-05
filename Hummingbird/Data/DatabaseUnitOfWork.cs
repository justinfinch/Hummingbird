using System.Transactions;
using Hummingbird.Security;

namespace Hummingbird.Data
{
    public class DatabaseUnitOfWork : IUnitOfWork
    {
        private TransactionScope _transaction;
        private ISecurityContext _securityContext;

        public DatabaseUnitOfWork()
        {
            _transaction = new TransactionScope();
        }

        public void Complete()
        {
            _transaction.Complete();
        }

        public void Rollback()
        {
            
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }
    }
}
