using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// Abstract base component to watch an BaseValue ScriptableObject
// Subscribes to a change event, and exposes its own events so other components can react to it

namespace Belwyn.Utils {

    public abstract class BaseValueObserver<T> : MonoBehaviour {

        // Value to watch
        protected abstract BaseValue<T> value { get; }

        // String formatted value
        protected abstract string formatted { get; }

        // Events exposed
        // The StringEvent sends the value as a String formatted with the value. This is a really useful convenience for UI components
        protected abstract UnityEvent<T> onValueChange { get; }
        protected abstract StringEvent onValueChangeAsString { get; }



        ///// Methods

        // Susbcribe on enable, and raise a change for setup
        private void OnEnable() {
            Debug.Assert(value != null, $"{GetType().Name} - No {typeof(BaseValue<T>)} assigned in {name}");
            value.AddListener(OnChange);
            OnChange(value.value);
        }

        // Unsuscribe on disable
        private void OnDisable() {
            Debug.Assert(value != null, $"{GetType().Name} - No {typeof(BaseValue<T>)} assigned in {name}");
            value.RemoveListener(OnChange);
        }

        // Invoke the events on change
        protected virtual void OnChange(T value) {
            onValueChange.Invoke(value);
            onValueChangeAsString.Invoke(string.Format(formatted, value));
        }

    } 

}
