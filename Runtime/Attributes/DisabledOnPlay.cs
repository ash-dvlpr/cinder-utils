using System;
using UnityEngine;


namespace CinderUtils.Attributes { 

    /// <summary>
    /// Marks a field / property to be disabled on the the Editor while the application is running or the Editor is changing states.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class DisabledOnPlayAttribute : PropertyAttribute { }

}
