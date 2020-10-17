using Belwyn.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Belwyn.ActionPlatformer.Game {


    // Simple ground detection component

    [RequireComponent(typeof(Collider2D))]
    public class GroundDetector2D : MonoBehaviour {

        [SerializeField]
        private Collider2D _collider;

        private List<Collider2D> _contacts;

        [SerializeField]
        private BoolEvent _onGroundChange;
        public BoolEvent onGroundChange => _onGroundChange;


        private void Awake() {
            if (_collider == null)
                _collider = GetComponent<Collider2D>();
            _collider.isTrigger = true;
            _contacts = new List<Collider2D>();
        }


        private void Invoke() {
            _onGroundChange.Invoke(_contacts.Count > 0);
        }

        // Trigger

        private void OnTriggerEnter2D(Collider2D collision) {
            if (!_contacts.Contains(collision)) {
                _contacts.Add(collision);
                Invoke();
            }
        }

        private void OnTriggerStay2D(Collider2D collision) {

        }


        private void OnTriggerExit2D(Collider2D collision) {
            if(_contacts.Remove(collision))
                Invoke();
        }

    }

}