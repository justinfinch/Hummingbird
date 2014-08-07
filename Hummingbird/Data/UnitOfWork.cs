using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Hummingbird.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private TransactionScope _transaction;

        public UnitOfWork()
        {
            var options = new TransactionOptions();
            options.Timeout = TransactionManager.MaximumTimeout;
            options.IsolationLevel = IsolationLevel.ReadCommitted;
            _transaction = new TransactionScope(TransactionScopeOption.Required, options, TransactionScopeAsyncFlowOption.Enabled);
        }

        public void Commit()
        {
            _transaction.Complete();
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }
    }
}
