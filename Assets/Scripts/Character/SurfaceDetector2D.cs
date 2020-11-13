using Belwyn.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Belwyn.ActionPlatformer.Game {


    // Simple Surface detection component
    // Uses a normal and a tolerance angle to detect if a contacted surface is a desired target

    [RequireComponent(typeof(Collider2D))]
    public class SurfaceDetector2D : MonoBehaviour {

        [SerializeField]
        private Collider2D _collider;        


        [Header("Surface detection")]
        [SerializeField]
        private Vector2 _normalTarget = Vector2.up;
        [SerializeField]
        private float _targetAngleTolerance = 45f;


        [Header("Events")]
        [SerializeField]
        private BoolEvent _onSurfacedChange;
        public BoolEvent onSurfacedChange => _onSurfacedChange;

        private Vector2 _currentNormal;
        public Vector2 currentNormal => _currentNormal;


        // TODO Improve the data structure
        // Contacts information
        private Dictionary<Collider2D, Vector2> _collisions;



        private void Awake() {
            if (_collider == null)
                _collider = GetComponent<Collider2D>();
            _collisions = new Dictionary<Collider2D, Vector2>();

        }



        private void InvokeSurfaced() {
            InvokeSurfaced(_collisions.Keys.Count > 0);
        }

        private void InvokeSurfaced(bool value) {
            _onSurfacedChange.Invoke(value);
        }



        private void CheckNormal() {
            // TODO This shouldn't care, but it is wrongly done. Think about it
            if (_collisions.Keys.Count > 0) {
                Vector2 normal = _collisions[_collisions.Keys.First()];
                _currentNormal = normal;
            }
            else {
                _currentNormal = Vector2.zero;
            }
        }


        ///// Physics

        private void OnCollisionEnter2D(Collision2D collision) {
            // TODO Maybe don't create an array here. This may generate garbage
            ContactPoint2D[] newContacts = new ContactPoint2D[collision.contactCount];
            collision.GetContacts(newContacts);
            Vector2 normal;
            if (checkIfTarget(newContacts, out normal)) {
                if (!_collisions.ContainsKey(collision.collider))
                    _collisions.Add(collision.collider, normal);
                else
                    _collisions[collision.collider] = normal;

                CheckNormal();
                InvokeSurfaced();
            }
        }

        private void OnCollisionExit2D(Collision2D collision) {
            if (_collisions.ContainsKey(collision.collider)) {
                // TODO Exit collision contactPoints do not have any normal information
                // Asume that it is correct if the collideris a key, for now.
                _collisions.Remove(collision.collider);
            }

            CheckNormal();
            InvokeSurfaced();
        }



        ///// Detection
        // Search in contactPoints for a normal whose angle with the target normal is smaller than tolerance
        private bool checkIfTarget(ContactPoint2D[] contacts, out Vector2 normal) {
            bool isTargetSurface = false;
            int i = 0;
            normal = Vector2.zero;

            while (!isTargetSurface && i < contacts.Length) {
                Vector2 currentnormal = contacts[i].normal;
                // If it's valid contactPoint
                if (currentnormal != Vector2.zero) {
                    // TODO Right now it asumes always the same normal, if any other shape than Box this may be wrong
                    normal = currentnormal;
                    float angle = Vector2.Angle(_normalTarget, normal);
                    isTargetSurface = angle <= _targetAngleTolerance;
                }
                i++;
            }

            return isTargetSurface;
        }

    }

}