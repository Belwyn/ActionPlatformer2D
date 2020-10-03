using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// ScriptableObject
// An scriptable object that stores a bool value, with event functionalities to notify changes.
// Extends from BaseValue ScriptableObject

namespace Belwyn.Utils {

    [CreateAssetMenu(fileName = "NewBoolValue", menuName = "Game/Bool Value")]
    public class BoolValue : BaseValue<bool> {

        // Value change event
        private BoolEvent _onValueChange = new BoolEvent();
        protected override UnityEvent<bool> onValueChange => _onValueChange;

    } 

}
