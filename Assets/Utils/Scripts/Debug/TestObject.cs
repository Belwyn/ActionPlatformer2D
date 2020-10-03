using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Belwyn.Utils.Test {

    // Simple component that destroys its gameobject when TEST_BUILD is not defined

    public class TestObject : MonoBehaviour {

        [System.Diagnostics.Conditional("TEST_BUILD")]
        private void Awake() {
            Destroy(gameObject);
        }

    }

}