using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// Component to watch an BoolValue ScriptableObject
// Extends from BaseValue component

namespace Belwyn.Utils {

    public class BoolValueObserver : BaseValueObserver<bool> {

        // IntValue to watch
        [Header("Value")]
        [SerializeField]
        private BoolValue _value;
        protected override BaseValue<bool> value => _value;

        // String formatted value
        [Header("String values")]
        [SerializeField]
        protected string _formatted = "{0}";
        protected override string formatted { 
            get { 
                return _formatted; 
            } 
        }

        [SerializeField]
        private string _trueStringValue = "true";
        [SerializeField]
        private string _falseStringValue = "false";

        // Events exposed
        [Header("Events")]
        [SerializeField]
        private BoolEvent _onValueChange;
        protected override UnityEvent<bool> onValueChange => _onValueChange;

        [SerializeField]
        private StringEvent _onValueChangeAsString;
        protected override StringEvent onValueChangeAsString => _onValueChangeAsString;


        // Override onchange to make easier the formatting with true/false string assigned values
        protected override void OnChange(bool value) {
            onValueChange.Invoke(value);
            onValueChangeAsString.Invoke(string.Format(formatted, value ? _trueStringValue : _falseStringValue));
        }

    } 

}
