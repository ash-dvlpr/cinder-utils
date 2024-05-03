using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

using CinderUtils.Extensions;
using CinderUtils.Reflection;


namespace CinderUtils.Services {

    // Base interface used to declare servies.
    public interface IService { }


    // Public Global EventBus for use by the user.
    public static class ServiceLocator {
        static readonly Dictionary<Type, object> Services = new();
        public static bool IsRegistered(Type t) => Services.ContainsKey(t);
        public static bool IsRegistered<TService>() => IsRegistered(typeof(TService));


        /// <summary>
        /// Takes all Services marked with the [<see cref="AutoRegisteredServiceAttribute">AutoRegisteredService</see>] Attribute and registers them 
        /// during the <see href="https://docs.unity3d.com/ScriptReference/RuntimeInitializeOnLoadMethodAttribute.html">SubsystemRegistration</see> initialization phase.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void Initialize() {
            foreach (var serviceType in GetAutoRegisteredServiceTypes()) {
                if (IsRegistered(serviceType)) continue;

                if (serviceType.IsMonoBehaviour()) {
                    FindOrCreateMonoBehaviourService(serviceType);
                }
                else {
                    CreateService(serviceType);
                }
            }
        }

        public static void Register<T>(T service, bool @unsafe = false) where T : IService, new() {
            var serviceType = typeof(T);
            if (IsRegistered<T>() && !@unsafe) {
                throw new CinderUtilsException($"ServiceLocator.Register(): Service '{serviceType.Name}' has already been registered.");
            }

            Services[typeof(T)] = service;
        }

        public static T Get<T>(bool forced = false) where T : IService, new() {
            var serviceType = typeof(T);
            // If it exists, return it
            if (IsRegistered<T>()) return (T) Services[serviceType];
            else {
                if (!forced)
                    throw new CinderUtilsException($"ServiceLocator.Get(): Service '{serviceType.Name}' was not registered.");

                // Create it if forced to do so
                var service = serviceType.IsMonoBehaviour() 
                    ? (T) FindOrCreateMonoBehaviourService(serviceType) 
                    : new T();

                return service;
            }
        }

        internal static IEnumerable<Type> GetAutoRegisteredServiceTypes() {
            return AssemblyUtils.GetTypesWithCustomAttribute<AutoRegisteredServiceAttribute>()
                .Where(service => service.Is<IService>());
        }

        static object FindOrCreateMonoBehaviourService(Type serviceType) {
            var serviceObj = GameObject.FindObjectOfType(serviceType);

            // Create a gameObject for the Service if not found
            if (!serviceObj) {
                var obj = new GameObject();
                obj.AddComponent(serviceType);
                obj.name = serviceType.Name;
                serviceObj = obj.GetComponent(serviceType);
            }

            // Register
            Services[serviceType] = serviceObj;
            return serviceObj;
        }

        static void CreateService(Type serviceType) {
            Services[serviceType] = Activator.CreateInstance(serviceType);
        }
    }
}