using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// Component to watch an StringValue ScriptableObject
// Extends from BaseValue component

namespace Belwyn.Utils {

    public class StringValueObserver : BaseValueObserver<string> {

        // Value to watch
        [Header("Value")]
        [SerializeField]
        private StringValue _value;
        protected override BaseValue<string> value => _value;

        // String formatted value
        [Header("String values")]
        [SerializeField]
        protected string _formatted = "{0}";
        protected override string formatted => _formatted;

        // Events exposed
        // The StringEvent sends the value as a String. This is a really useful convenience for UI components
        [Header("Events")]
        [SerializeField]
        private StringEvent _onValueChange;
        protected override UnityEvent<string> onValueChange => _onValueChange;

        [SerializeField]
        private StringEvent _onValueChangeAsString;
        protected override StringEvent onValueChangeAsString => _onValueChangeAsString;

    }

}