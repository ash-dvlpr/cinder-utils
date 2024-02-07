using System;

namespace CinderUtils.Events {

    // Internal interface for reflexion
    internal interface IEventBinding<T> {
        public event Action<T> OnEvent;
        public event Action    OnEventNotify;
    }

    // Concrete binding class for external use
    public class EventBinding<T> : IEventBinding<T> where T : IEvent {
        internal Action<T> onEvent;
        internal Action    onEventNotify;

        public event Action<T> OnEvent {
            add    { onEvent += value; }
            remove { onEvent -= value; }
        }

        public event Action OnEventNotify {
            add    { onEventNotify += value; }
            remove { onEventNotify -= value; }
        }
    }

}
