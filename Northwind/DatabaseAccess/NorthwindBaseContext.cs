using System.Data.Entity;

namespace Northwind.DatabaseAccess
{
    public class NorthwindBaseContext<TContext> : DbContext where TContext: DbContext
    {
        static NorthwindBaseContext()
        {
            Database.SetInitializer<TContext>(null);
        }

        protected NorthwindBaseContext()
            : base("Northwind")
        {

        }
    }
}
