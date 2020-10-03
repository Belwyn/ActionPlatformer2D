using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// ScriptableObject
// An scriptable object that stores a string value, with event functionalities to notify changes.
// Extends from BaseValue ScriptableObject

namespace Belwyn.Utils {

    [CreateAssetMenu(fileName = "NewStringValue", menuName = "Game/String Value")]
    public class StringValue : BaseValue<string> {

        // Value change event
        private StringEvent _onValueChange = new StringEvent();
        protected override UnityEvent<string> onValueChange => _onValueChange;

    }

}