using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hummingbird.Events
{
    public interface IEventStream
    {
        void Push<TEvent>(TEvent @event) where TEvent : IDomainEvent;
        IObservable<TEvent> Of<TEvent>() where TEvent : IDomainEvent;
    }
}
