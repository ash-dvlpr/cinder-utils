using System;
using UnityEngine;


namespace CinderUtils.Services {
    public class MonoBehaviourService : MonoBehaviour, IService {
        protected virtual void Awake() {
            DontDestroyOnLoad(gameObject);
        }

        protected virtual void Reset() { 
            gameObject.name = GetType().Name;
        }
    }
}
