using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Must import the namespace to make usse of the custom extensions
using CinderUtils.Extensions;
using CinderUtils.Attributes;

public class SampleExtensionsComponent : MonoBehaviour {
    [Disabled, SerializeField] List<string> greetings = new List<string>(){ "Hello", "Hi", "Hey", "Howdy" };

    void Start() {
        Debug.Log("Starting Extension examples:");

        Debug.Log($"ICollection.GetRandom(): {greetings.GetRandom()}, how's your day going?");
        
        Debug.Log($"IEnumerable.NullOrEmpty(): The greetings list {(greetings.NullOrEmpty() ? "is" : "isn't'")} empty.");
        
        Debug.Log($"IEnumerable.ToDebugString(): greetings list contents: {greetings.ToDebugString()}");

        Debug.Log($"ICollection.ForEach(): Printing a greetings message for each greeting.");
        greetings.ForEach(g => {
            Debug.Log($"ICollection.ForEach(): {g}, how's your day going?");
        });
    }
}
