using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CinderUtils.Extensions;


namespace CinderUtils.Events {

    // Base interface used to declare event types.
    // An EventBus will be created for every type of event declared.
    public interface IEvent { }

    // Quality Of Life: Global EventBus
    public static class EventBus {
        public static void Register<T>(EventBinding<T> binding) where T : IEvent => EventBus<T>.Register(binding);
        public static void Deregister<T>(EventBinding<T> binding) where T : IEvent => EventBus<T>.Deregister(binding);
        public static void Raise<T>(T @event) where T : IEvent => EventBus<T>.Raise(@event);
    }

    // EventBus for a concrete event type
    public static class EventBus<T> where T : IEvent {
        static readonly HashSet<EventBinding<T>> bindings = new();

        public static void Register(EventBinding<T> binding) => bindings.Add(binding);
        public static void Deregister(EventBinding<T> binding) => bindings.Remove(binding);

        public static void Raise(T @event) {
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
