using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Must import the namespace to make use of the events system
using CinderUtils.Events;


public class EventSubsciptor : MonoBehaviour {
    // Declare the binding for the events you want to interact with.
    //! IMPORTANT: Make sure to initialize them
    EventBinding<BareEvent> bareEvent = new();
    EventBinding<TestEvent> testEvent = new();



    // Connect your event handlers to your bindings, and register your bindings
    void OnEnable() {
        // Each event has 2 channels: OnEvent & OnEventNotify
        // NOTE: You can use whatever channels and hook as many handler methods as you need.

        // One recieves the event's data (the "event" struct/class):
        testEvent.OnEvent += TestEventHandler;
        // The other only get's notified that the event has been raised:
        testEvent.OnEventNotify += TestEventHandler2;
        bareEvent.OnEventNotify += BareEventHandler;

        // IMPORTANT: Make sure to REGISTER your bindings on the event bus
        EventBus.Register(bareEvent);
        EventBus.Register(testEvent);
    }

    //! IMPORTANT: Make sure to deregister your bindings.
    void OnDisable() {
        // Register the bindings on the EventBuses
        EventBus.Deregister(bareEvent);
        EventBus.Deregister(testEvent);
    }

    //? Handlers
    void BareEventHandler() {
        Debug.Log("EventSubsciptor.BareEventHandler(): Notified.");
    }

    void TestEventHandler(TestEvent data) {
        Debug.Log($"EventSubsciptor.TestEventHandler(): TestEvent {JsonUtility.ToJson(data, true)}");
    }
    void TestEventHandler2() {
        Debug.Log($"EventSubsciptor.TestEventHandler2(): Notified.");
    }

}
