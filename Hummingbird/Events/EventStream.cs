using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hummingbird.Events
{
    public class EventStream : IEventStream
    {
        private static Lazy<EventStream> _instance = new Lazy<EventStream>(() => new EventStream());
        public static EventStream Instance 
        { 
            get
            {
                return _instance.Value;
            }
        }

        private EventStream()
        {

        }

        public void Push<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            Debug.WriteLine("[EventStream.Push] - Pushing event to event stream");
        }

        public IObservable<TEvent> Of<TEvent>() where TEvent : IDomainEvent
        {
            throw new NotImplementedException();
        }
    }
}
