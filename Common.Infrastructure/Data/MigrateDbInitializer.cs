using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Common.Infrastructure.Data
{
    public class MigrateDbInitializer<TContext> : IDatabaseInitializer
        where TContext : DbContext, new()
    {
        public void Initialze()
        {
            using (var db = new TContext())
            {
                db.Database.Initialize(false);
            }
        }
    }
}
