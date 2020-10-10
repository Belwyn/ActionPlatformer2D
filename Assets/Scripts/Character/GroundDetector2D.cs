using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Belwyn.ActionPlatformer.Game {


    // Simple ground detection component

    [RequireComponent(typeof(Collider2D))]
    public class GroundDetector2D : MonoBehaviour {

        public bool isGrounded => _contacts.Count > 0;

        [SerializeField]
        private Collider2D _collider;

        private List<Collider2D> _contacts;

        private void Awake() {
            if (_collider == null)
                _collider = GetComponent<Collider2D>();
            _collider.isTrigger = true;
            _contacts = new List<Collider2D>();
        }



        // Trigger

        private void OnTriggerEnter2D(Collider2D collision) {
            if (!_contacts.Contains(collision))
                _contacts.Add(collision);
        }

        private void OnTriggerStay2D(Collider2D collision) {

        }


        private void OnTriggerExit2D(Collider2D collision) {
            _contacts.Remove(collision);
        }

    }

}