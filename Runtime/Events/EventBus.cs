using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CinderUtils.Extensions;


namespace CinderUtils.Events {

    // Base interface used to declare event types.
    // An EventBus will be created for every type of event declared.
    public interface IEvent { }

    // Public Global EventBus for use by the user.
    public static class EventBus {
        public static void Register<T>(EventBinding<T> binding) where T : IEvent => EventBus<T>.Register(binding);
        public static void Deregister<T>(EventBinding<T> binding) where T : IEvent => EventBus<T>.Deregister(binding);
        public static void Raise<T>(T @event = default) where T : IEvent => EventBus<T>.Raise(@event);
    }

    // EventBus for a concrete event type
    internal static class EventBus<T> where T : IEvent {
        static readonly HashSet<EventBinding<T>> bindings = new();

        internal static void Register(EventBinding<T> binding) => bindings.Add(binding);
        internal static void Deregister(EventBinding<T> binding) => bindings.Remove(binding);

        internal static void Raise(T @event = default) {
            foreach (var binding in bindings) {
                binding.onEvent?.Invoke(@event);
                binding.onEventNotify?.Invoke();
            }
        }

        internal static void Clear() {
            bindings.Clear();
        }
    }

}
