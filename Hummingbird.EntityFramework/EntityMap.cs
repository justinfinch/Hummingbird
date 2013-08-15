using System.Data.Entity.ModelConfiguration;
using Hummingbird.Data;
using Hummingbird.Domain;

namespace Hummingbird.EntityFramework
{
    public class EntityMap<T, TKey> : EntityTypeConfiguration<T>
        where T : Entity<TKey>
    {
        public EntityMap (bool mapVersion = true)
        {
            Ignore(e => e.CurrentObjectState);
            if(mapVersion) Property(e => e.Version).IsRowVersion();
        }

    }
}
