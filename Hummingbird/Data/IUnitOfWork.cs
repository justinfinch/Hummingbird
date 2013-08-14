using System;

namespace Hummingbird.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
