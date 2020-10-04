using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Belwyn.ActionPlatformer.Game.Character {

    public class CharController : MonoBehaviour {

        [Header("Components")]
        [SerializeField]
        private CharAnimator _animator;
        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        [SerializeField]
        private Rigidbody2D _rigidbody;



        [Header("GroundCheck")]
        [SerializeField]
        private GroundDetector2D _ground;

        [Header("Parameters")]
        public float speed = 1f;
        public float jumpSpeed = 1f;
        public float fallSpeed = 1f;

        private Vector2 _move;
        private bool _isMoving;
        private bool _isRight;

        private bool _isJumping;

        private void Awake() {
            
        }

        private void FixedUpdate() {

            Vector2 desiredPosition = transform.position;
            
            // FIXME downwards velocity check
            _animator.Grounded(_ground.isGrounded);           

            _animator.Walking(_isMoving);


            if (_isMoving) {
                Vector2 hMove = new Vector2(_move.x, 0);
                Vector2 delta = (hMove * Time.deltaTime * speed);
                //if (!_ground.isGrounded)
                //    position += ((Vector3)Physics2D.gravity * Time.deltaTime * _rigidbody.gravityScale);
                //_rigidbody.MovePosition(position);
                desiredPosition += delta;
                //_rigidbody.AddForce(hMove * speed);
                _spriteRenderer.flipX = _isRight;
            }

            _animator.Jump(_isJumping);
            _animator.JumpAscension(_isJumping);

            if (!_ground.isGrounded && !_isJumping) {
                //_rigidbody.MovePosition(transform.position + (Vector3.down * Time.deltaTime * fallSpeed));
                desiredPosition += (Vector2.down * Time.deltaTime * fallSpeed);
            }

            if (_isJumping) {
                //_rigidbody.MovePosition(transform.position + (Vector3.up * Time.deltaTime * jumpSpeed));
                desiredPosition += (Vector2.up * Time.deltaTime * jumpSpeed);
            }
            
            _rigidbody.MovePosition(desiredPosition);

            

        }

        public void Move(Vector2 move) {

            _isMoving = move.x != 0;
            _isRight = move.x > 0;
            _move = move;

        }


        public void Jump(bool jump) {
            _isJumping = jump;
        }


        public void Attack() {
            _animator.Attack();
        }


    }


}
