using System;

namespace Hummingbird.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Complete();
        void Rollback();
    }
}
