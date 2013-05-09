using System.Data.Entity.ModelConfiguration;
using Hummingbird.Data;

namespace Hummingbird.EntityFramework
{
    public abstract class Map<T> : EntityTypeConfiguration<T>
        where T : class, IDataRow
    {

    }
}
