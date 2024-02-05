using System;
using UnityEngine;


namespace CinderUtils.Attributes {

    /// <summary>
    /// Marks a field / property to be disabled on the the Editor.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class DisabledAttribute : PropertyAttribute { }

}