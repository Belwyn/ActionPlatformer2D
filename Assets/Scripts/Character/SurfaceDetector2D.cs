using Belwyn.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Belwyn.ActionPlatformer.Game {


    // Simple ground detection component

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



        // Store or expose normal and/or angle event of the surface




        // Contact information
        private Dictionary<Collider2D, ContactPoint2D[]> _collisions;

        private int _contactsCount = 5;


        private void Awake() {
            if (_collider == null)
                _collider = GetComponent<Collider2D>();
            _collisions = new Dictionary<Collider2D, ContactPoint2D[]>();

        }



        private void Invoke() {
            Invoke(_collisions.Keys.Count > 0);
        }

        private void Invoke(bool value) {
            _onSurfacedChange.Invoke(value);
        }




        ///// Physics

        private void OnCollisionEnter2D(Collision2D collision) {
            ContactPoint2D[] newContacts = new ContactPoint2D[_contactsCount];
            collision.GetContacts(newContacts);

            if (checkIfTarget(newContacts)) {
                if (!_collisions.ContainsKey(collision.collider))
                    _collisions.Add(collision.collider, newContacts);
                else
                    _collisions[collision.collider] = newContacts;                

                Invoke();
            }
        }

        private void OnCollisionExit2D(Collision2D collision) {
            if (_collisions.ContainsKey(collision.collider)) {
                // TODO Exit collision contactPoints do not have any normal information
                // Asume that it is correct if the collideris a key, for now.
                _collisions.Remove(collision.collider);
            }

            Invoke();
        }



        ///// Detection
        // Search in contactPoints for a normal whose angle with the target normal is smaller than tolerance
        private bool checkIfTarget(ContactPoint2D[] contacts) {
            bool isGround = false;
            int i = 0;
            while (!isGround && i < contacts.Length) {
                Vector2 normal = contacts[i].normal;
                // If it's valid contactPoint
                if (normal != Vector2.zero) {
                    float angle = Vector2.Angle(_normalTarget, normal);
                    isGround = angle <= _targetAngleTolerance;
                }
                i++;
            }
            return isGround;
        }

    }

}