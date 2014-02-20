using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Hummingbird.Events
{
    public static class EventPublisher
    {
        public static void Raise<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            //Check to see if there is a current EventTransaction in progress, and if so add the event to it
            //Otherwise publish the event directly to the EventBus
            var currentTransaction = Transaction.Current;
            if(currentTransaction != null)
            {
                currentTransaction.EnlistVolatile(new EventResourceManager<TEvent>(@event), EnlistmentOptions.None);
            }
            else
            {
                EventStream.Instance.Push(@event);
            }

        }
    }
}
