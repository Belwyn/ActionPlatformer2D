using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Belwyn.ActionPlatformer.Game {


    // Simple ground detection component

    [RequireComponent(typeof(Collider2D))]
    public class GroundDetector2D : MonoBehaviour {

        public bool isGrounded { get; private set; }

        [SerializeField]
        private Collider2D _collider;


        private void Awake() {
            if (_collider == null)
                _collider = GetComponent<Collider2D>();
            _collider.isTrigger = true;
        }



        // Trigger
        // FIXME trigger seems to register a few frames late??
        // maybe because this rb is moved by a parent rb, so the physics need more time to recalculate?
        // Other options -> raycast or areacast

        private void OnTriggerEnter2D(Collider2D collision) {
            isGrounded = true;
        }

        private void OnTriggerStay2D(Collider2D collision) {
            isGrounded = true;   
        }


        private void OnTriggerExit2D(Collider2D collision) {
            isGrounded = false;
        }

    }

}