using System;
using Hummingbird.Data;
using System.Threading;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hummingbird.Domain
{
    public abstract class Entity<TKey> : IVersionedEntity, IAuditableEntity
    {
        public TKey Id { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
        public DateTime LastModifiedDate { get; protected set; }
        public string CreatedBy { get; protected set; }
        public string LastModifiedBy { get; protected set; }
        public Byte[] RowVersion { get; protected set; }

        [NotMapped] //TODO: Find a better way to do this through EF context 
        public ObjectState CurrentObjectState { get; protected set; }

        protected Entity()
        {
            CurrentObjectState = ObjectState.Unchanged;
        }

        protected void WasModified()
        {
            if (CurrentObjectState == ObjectState.Unchanged)
            {
                CurrentObjectState = ObjectState.Modified;
                LastModifiedDate = DateTime.Now;
                LastModifiedBy = Thread.CurrentPrincipal.Identity.Name;
            }
        }

        protected void WasCreated()
        {
            if (CurrentObjectState == ObjectState.Unchanged)
            {
                CurrentObjectState = ObjectState.Added;
                CreatedDate = DateTime.Now;
                LastModifiedDate = DateTime.Now;
                CreatedBy = Thread.CurrentPrincipal.Identity.Name;
                LastModifiedBy = Thread.CurrentPrincipal.Identity.Name;
            }
        }

        protected void WasDeleted()
        {
            if (CurrentObjectState == ObjectState.Added)
            {
                CurrentObjectState = ObjectState.Unchanged;
            }
            else
            {
                CurrentObjectState = ObjectState.Deleted;
            }
        }

        public void ResetState()
        {
            CurrentObjectState = ObjectState.Unchanged;
        }

        public object GetKey()
        {
            return Id;
        }

        public static bool operator ==(Entity<TKey> a, Entity<TKey> b)
        {
            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(Entity<TKey> a, Entity<TKey> b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            return CompareTo(obj) == 1;
        }

        public int CompareTo(object obj)
        {
            bool areEqual = false;

            if (obj != null)
            {
                var otherEntity = obj as Entity<TKey>;

                if (otherEntity != null)
                {
                    areEqual = this.GetKey() == otherEntity.GetKey();
                }
            }

            return areEqual ? 1 : 0;
        }

        public override int GetHashCode()
        {
            return this.GetKey().GetHashCode();
        }
    }
}
