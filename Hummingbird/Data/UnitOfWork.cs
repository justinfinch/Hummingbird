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
            _transaction = new TransactionScope();
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
