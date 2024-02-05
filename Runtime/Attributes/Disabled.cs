using System;
using UnityEngine;


namespace CinderUtils.Attributes {

    /// <summary>
    /// Marks a field to be disabled on the the Editor.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class DisabledAttribute : PropertyAttribute { }

}