using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Domain
{
    /// <summary>
    /// Entity object are objects that are compared using unique keys rather than their attributes.
    /// 
    /// Example:
    ///     Consider a bank accounting sytem. Each account has its own
    ///     number. An account can be precisely identified by its number.
    ///     This number remains unchanged throughout the life of the
    ///     system, and assures continuity. The account number can exist as
    ///     an object in the memory, or it can be destroyed in memory and
    ///     sent to the database. It can also be archived when the account is
    ///     closed, but it still exists somewhere as long as there is some
    ///     interest in keeping it around. It does not matter what
    ///     representation it takes, the number remains the same.
    /// </summary>
    public abstract class EntityObject<TKey>
    {
        public abstract TKey GetKey();

        public static bool operator ==(EntityObject<TKey> a, EntityObject<TKey> b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(EntityObject<TKey> a, EntityObject<TKey> b)
        {
            return !a.Equals(b);
        }

        public override bool  Equals(object other)
        {
 	        bool objectsAreEqual = false;

            if (other is EntityObject<TKey>)
            {
                objectsAreEqual = ((EntityObject<TKey>)other).GetKey().Equals(GetKey());
            }

            return objectsAreEqual;
        }

        public override int GetHashCode()
        {
            //TODO: Research and implement correctly
            return base.GetHashCode();
        }
    }
}
