using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// ScriptableObject
// An scriptable object that stores an int value, with event functionalities to notify changes.
// Extends from BaseValue ScriptableObject

namespace Belwyn.Utils {

    [CreateAssetMenu(fileName = "NewIntValue", menuName = "Game/Int Value")]
    public class IntValue : BaseValue<int> {

        // Value change event
        private IntEvent _onValueChange = new IntEvent();
        protected override UnityEvent<int> onValueChange => _onValueChange;

    } 

}
