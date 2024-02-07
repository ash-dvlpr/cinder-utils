using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Must import the namespace to make use of the events system
using CinderUtils.Events;


public class EventRaiser : MonoBehaviour {
    //! IMPORTANT: You don't need a binding to raise events on a bus.
    // But if you want to both raise and recieve events, you'll need to create the corresponding bindings.
    public bool _someInfo = true;

    void Start() {
        EventBus.Raise<BareEvent>();
        EventBus.Raise<TestEvent>(new() { name = gameObject.name, someInfo = _someInfo });
    }
}
