using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Hummingbird.Events
{
    public class EventResourceManager<TEvent> : IEnlistmentNotification
        where TEvent : IDomainEvent
    {
        private TEvent _event;

        public EventResourceManager(TEvent @event)
        {
            _event = @event;
        }

        public void Commit(Enlistment enlistment)
        {
            EventStream.Instance.Push(_event);
        }

        public void InDoubt(Enlistment enlistment)
        {
            //Do Nothing
        }

        public void Prepare(PreparingEnlistment preparingEnlistment)
        {
            preparingEnlistment.Prepared();
        }

        public void Rollback(Enlistment enlistment)
        {
            //Do Nothing
        }
    }
}
