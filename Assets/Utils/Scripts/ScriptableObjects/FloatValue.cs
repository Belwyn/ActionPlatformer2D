using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// ScriptableObject
// An scriptable object that stores a float value, with event functionalities to notify changes.
// Extends from BaseValue ScriptableObject

namespace Belwyn.Utils {

    [CreateAssetMenu(fileName = "NewFloatValue", menuName = "Game/Float Value")]
    public class FloatValue : BaseValue<float> {

        // Value change event
        private FloatEvent _onValueChange = new FloatEvent();
        protected override UnityEvent<float> onValueChange => _onValueChange;

    } 

}
