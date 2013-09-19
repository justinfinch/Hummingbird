using System.Data.Entity;

namespace Sample.DatabaseAccess
{
    public class BaseContext<TContext> : DbContext where TContext: DbContext
    {
        static BaseContext()
        {
            Database.SetInitializer<TContext>(null);
        }

        protected BaseContext()
            : base("HummingbirdSample")
        {

        }
    }
}
