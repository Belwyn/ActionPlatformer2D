using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// Component to watch an FloatValue ScriptableObject
// Extends from BaseValue component

namespace Belwyn.Utils {

    public class FloatValueObserver : BaseValueObserver<float> {

        // IntValue to watch
        [Header("Value")]
        [SerializeField]
        private FloatValue _value;
        protected override BaseValue<float> value => _value;

        // String formatted value
        [Header("String values")]
        [SerializeField]
        protected string _formatted = "{0}";
        protected override string formatted => _formatted;

        // Events exposed
        [Header("Events")]
        [SerializeField]
        private FloatEvent _onValueChange;
        protected override UnityEvent<float> onValueChange => _onValueChange;

        [SerializeField]
        private StringEvent _onValueChangeAsString;
        protected override StringEvent onValueChangeAsString => _onValueChangeAsString;

    } 

}
