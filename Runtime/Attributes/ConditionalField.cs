using System;
using System.Linq;
using UnityEngine;

using CinderUtils.Extensions;


namespace CinderUtils.Attributes {

    /// <summary>
    /// Marks a field to be shown only if value of the designated field matches one of the provided values.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public abstract class ConditionalFieldAttribute : PropertyAttribute {
        public string targetFieldName;
        public object[] compareValues;
        public bool inverse = false;
        public bool IsSet => !targetFieldName.NullOrEmpty() && !compareValues.NullOrEmpty();

        /// <param name="targetFieldName">String name of field to check value.</param>
        /// <param name="inverse">If inverse is set, the field will be shown if the value doesn't match.</param>
        /// <param name="compareValues">Values to match the field against.</param>
        internal ConditionalFieldAttribute(string targetFieldName, bool inverse = false, params object[] compareValues) {
            this.targetFieldName = targetFieldName;
            this.inverse = inverse;
            this.compareValues = compareValues;
        }
    }

    public class ShowIfAttribute : ConditionalFieldAttribute {
        public ShowIfAttribute(string targetFieldName, params object[] compareValues) :
            base(targetFieldName, false, compareValues) { }
    }

    public class HideIfAttribute : ConditionalFieldAttribute {
        public HideIfAttribute(string targetFieldName, params object[] compareValues) :
            base(targetFieldName, true, compareValues) { }
    }

}