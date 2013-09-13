using System.Data;
using Hummingbird.Data;

namespace Hummingbird.EntityFramework
{
    public sealed class StateHelpers
    {
        public static EntityState ConvertState(ObjectState state)
        {
            switch (state)
            {
                case ObjectState.Added:
                    return EntityState.Added;
                case ObjectState.Modified:
                    return EntityState.Modified;
                case ObjectState.Deleted:
                    return EntityState.Deleted;
                default:
                    return EntityState.Detached;
            }
        }
    }
}
