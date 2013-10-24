using Hummingbird.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Sample.DatabaseAccess
{
    public abstract class SampleBaseContext<TContext> : BaseContext<TContext>
        where TContext : DbContext
    {
        protected SampleBaseContext()
            : base("HummingbirdSample")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
