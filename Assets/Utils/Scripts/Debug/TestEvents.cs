using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// These are a bunch of events taht inherit the EventDebug behaviour. Calling Invoke() on a DebugEvent will only trigger when TEST_BUILD is defined

namespace Belwyn.Utils.Test {

    [System.Serializable]
    public abstract class DebugEvent<T> {

        protected virtual UnityEvent<T> ev { get; set; }

        public DebugEvent(UnityEvent<T> e) {
            ev = e;
        }

        [System.Diagnostics.Conditional("TEST_BUILD")]
        public void Invoke(T arg) {
            ev?.Invoke(arg);
        }

    }




    // These implementations are needed so Unity can serialize the events correctly

    [System.Serializable] public class IntEventDebug : DebugEvent<int> {
        [SerializeField] private IntEvent _event;
        protected override UnityEvent<int> ev => _event;
        public IntEventDebug() : base(new IntEvent()) {}
    }

    [System.Serializable] public class StringEventDebug : DebugEvent<string> {
        [SerializeField] private StringEvent _event;
        protected override UnityEvent<string> ev => _event;
        public StringEventDebug() : base(new StringEvent()) {}
    }

    [System.Serializable] public class BoolEventDebug : DebugEvent<bool> {
        [SerializeField] private BoolEvent _event;
        protected override UnityEvent<bool> ev => _event;
        public BoolEventDebug() : base(new BoolEvent()) { }
    }

    [System.Serializable] public class FloatEventDebug : DebugEvent<float> {
        [SerializeField] private FloatEvent _event;
        protected override UnityEvent<float> ev => _event;
        public FloatEventDebug() : base(new FloatEvent()) { }
    }

    [System.Serializable] public class Vector2EventDebug : DebugEvent<Vector2> {
        [SerializeField] private Vector2Event _event;
        protected override UnityEvent<Vector2> ev => _event;
        public Vector2EventDebug() : base(new Vector2Event()) { }
    }

    [System.Serializable] public class Vector3EventDebug : DebugEvent<Vector3> {
        [SerializeField] private Vector3Event _event;
        protected override UnityEvent<Vector3> ev => _event;
        public Vector3EventDebug() : base(new Vector3Event()) { }
    }

    [System.Serializable] public class GameObjectEventDebug : DebugEvent<GameObject> {
        [SerializeField] private GameObjectEvent _event;
        protected override UnityEvent<GameObject> ev => _event;
        public GameObjectEventDebug() : base(new GameObjectEvent()) { }
    }

}