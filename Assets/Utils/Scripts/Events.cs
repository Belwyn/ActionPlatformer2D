using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// This file contains classes of generic implementations of UnityEvent<>.
// This is needed because Unity can't serialize generic types.

namespace Belwyn.Utils {

    [System.Serializable] public class IntEvent : UnityEvent<int> { }
    [System.Serializable] public class StringEvent : UnityEvent<string> { }
    [System.Serializable] public class BoolEvent : UnityEvent<bool> { }
    [System.Serializable] public class FloatEvent : UnityEvent<float> { }
    [System.Serializable] public class Vector2Event : UnityEvent<Vector2> { }
    [System.Serializable] public class Vector3Event : UnityEvent<Vector3> { }
    [System.Serializable] public class GameObjectEvent : UnityEvent<GameObject> { }

}