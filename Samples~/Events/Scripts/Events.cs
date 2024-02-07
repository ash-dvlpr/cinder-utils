using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CinderUtils.Events;


// Event with no info
public struct BareEvent : IEvent { }

// Event with atached additional information
public struct TestEvent : IEvent {
    public string name;
    public bool someInfo;
}