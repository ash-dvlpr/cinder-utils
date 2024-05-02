using CinderUtils.Services;
using System;


namespace CinderUtils.Services {
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoRegisteredServiceAttribute : Attribute { }
}