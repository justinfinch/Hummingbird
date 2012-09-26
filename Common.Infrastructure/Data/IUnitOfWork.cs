using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Infrastructure.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Start();
        void Complete();
        void Rollback();
        IRepository<T> GetRepository<T>() where T : DataRow, new();
    }
}
