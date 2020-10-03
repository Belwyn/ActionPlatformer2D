using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Belwyn.Utils {

    // Abstract component with a default behaviour as a Singleton
    // Stores instance on Awake, it does not create an instance if accessed, the component must be in the scene first

    public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour {

        public static T instance { get; private set; }

        protected virtual void Awake() {
            if (instance != null) {
                Debug.LogWarning(name + " - Singleton instance already exists. This is not permitted ");
                Destroy(gameObject);
                return;
            }

            instance = GetComponent<T>();
        }


        // Delete the instance if it is removed from the scene
        protected virtual void OnDestroy() {

            if (instance != null && instance == this) {
                instance = null;
            }

        }

    }

}