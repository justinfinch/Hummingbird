using System.Data.Entity;
using Hummingbird.Data;

namespace Hummingbird.EntityFramework
{
    public static class DbContextHelpers
    {
        public static void ApplyStateChanges(this DbContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries<IObjectWithState>())
            {
                IObjectWithState stateInfo = entry.Entity;
                entry.State = StateHelpers.ConvertState(stateInfo.CurrentObjectState);
            }
        }
    }
}
