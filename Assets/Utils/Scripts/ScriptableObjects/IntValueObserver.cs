using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// Component to watch an IntValue ScriptableObject
// Extends from BaseValue component

namespace Belwyn.Utils {

    public class IntValueObserver : BaseValueObserver<int> {

        // IntValue to watch
        [Header("Value")]
        [SerializeField]
        private IntValue _value;
        protected override BaseValue<int> value => _value;

        // String formatted value
        [Header("String values")]
        [SerializeField]
        protected string _formatted = "{0}";
        protected override string formatted => _formatted;

        // Events exposed
        [Header("Events")]
        [SerializeField]
        private IntEvent _onValueChange;
        protected override UnityEvent<int> onValueChange => _onValueChange;

        [SerializeField]
        private StringEvent _onValueChangeAsString;
        protected override StringEvent onValueChangeAsString => _onValueChangeAsString;

    } 

}
