using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Must import the namespace to make use of the custom attributes
using CinderUtils.Attributes;


public class SampleAttributesComponent : MonoBehaviour {
    // DisabledAttribute
    [Header("Always disabled")]
    [Disabled] public string someText = "Hello";

    // DisabledOnPlayAttribute
    [Header("Disabled on play mode")]
    [DisabledOnPlay] public bool someBool;
    [DisabledOnPlay] public List<string> otherTexts = new();

    // Basic ConditionalFieldAttribute usage (ShowIf & HideIf)
    [Header("Shown on true")]
    public bool check = false;
    [ShowIf(nameof(check), true)] public string shownIfTrue  = "Hi!";
    [HideIf(nameof(check), true)] public string hiddenIfTrue = "It's a secret";

    // Advanced ConditionalFieldAttribute usage (ShowIf & HideIf) with inheritage
    public List<TestData> testList = new();

    void Start() {

    }
}

public enum DataType {
    None = 0,
    Basic = 1,
    Advanced = 2,
}

[Serializable]
public struct TestData {
    public DataType dataTypee;

    [HideIf(nameof(dataTypee), DataType.None)]
    public bool check;

    // Test for more than one value
    [ShowIf(nameof(dataTypee), DataType.Basic, DataType.Advanced)]
    public bool check2;

    [ShowIf(nameof(dataTypee), DataType.Advanced)]
    public Advanced advanced;

    [Serializable]
    public struct Advanced {
        public string name;

        // String values
        [HideIf(nameof(name), "")]
        public string otherName;
    }
}
