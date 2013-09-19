using System;
using Hummingbird.Data;
using System.Threading;

namespace Hummingbird.Domain
{
    public abstract class Entity<TKey> : IVersionedEntity, IAuditableEntity
    {
        public TKey Id { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
        public DateTime LastModifiedDate { get; protected set; }
        public string CreatedBy { get; protected set; }
        public string LastModifiedBy { get; protected set; }
        public Byte[] Version { get; protected set; }
        public ObjectState CurrentObjectState { get; protected set; }

        protected Entity()
        {
            CurrentObjectState = ObjectState.Unchanged;
        }

        public void WasModified()
        {
            if (CurrentObjectState == ObjectState.Unchanged)
            {
                CurrentObjectState = ObjectState.Modified;
                LastModifiedDate = DateTime.Now;
                LastModifiedBy = Thread.CurrentPrincipal.Identity.Name;
            }
        }

        public void WasCreated()
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

        public object GetKey()
        {
            return Id;
        }

        //TODO: Add IComparer stuff
    }
}
