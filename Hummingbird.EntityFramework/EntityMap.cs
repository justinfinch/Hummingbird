using System.Data.Entity.ModelConfiguration;
using Hummingbird.Data;
using Hummingbird.Domain;

namespace Hummingbird.EntityFramework
{
    public class EntityMap<T> : EntityTypeConfiguration<T>
        where T : class, IVersionedEntity
    {
        public EntityMap ()
        {
            Ignore(e => e.CurrentObjectState);
            Property(e => e.Version).IsRowVersion();
        }

    }
}
