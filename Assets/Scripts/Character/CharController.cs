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
        private Rigidbody2D _rb;



        [Header("Ground Check")]
        [SerializeField]
        private GroundDetector2D _groundDetector;


        [Header("Parameters")]
        public float moveSpeed  = 1f;
        //public float stopDrag   = 1f;
        [Space()]
        public float jumpSpeed  = 1f;
        public float maxFallSpeed = 1f;
        public float breakJumpFactor = 1f;
        public float fallFactor  = 1f;



        private Vector2 _move;
        private bool _isMoving;
        private bool _isRight;

        private bool _isJumping;

        private bool grounded { get { return _groundDetector.isGrounded && _rb.velocity.y <= 0; } }



        private void Awake() {
            
        }


        private void FixedUpdate() {

            float moveX = _rb.velocity.x;
            float moveY = _rb.velocity.y;

            if (_isMoving) {
                moveX = Mathf.Sign(_move.x) * moveSpeed;
            } 
            else {
                moveX = 0;
            }


            // TODO fix jump recognition behaviour
            if (_isJumping) {
                moveY = jumpSpeed;
            }

            // Fall speed tweak
            if (moveY < 0) {
                //moveY += Physics2D.gravity.y * (fallFactor - 1) * Time.deltaTime;
                _rb.gravityScale = fallFactor;
            }
            else if (moveY > 0 && !_isJumping) {
                //moveY += Physics2D.gravity.y * (breakJumpFactor - 1) * Time.deltaTime;
                _rb.gravityScale = breakJumpFactor;
            } 
            else {
                _rb.gravityScale = 1f;
            }

            // Fall speed cap
            moveY = Mathf.Max(moveY, -1 * maxFallSpeed);


            // Apply velocity
            _rb.velocity = new Vector2(moveX, moveY);


            // Update visuals

            _spriteRenderer.flipX = _isRight;

            _animator.Jump(_isJumping); // FIXME not just the input, but a valid jump action
            _animator.JumpAscension(_rb.velocity.y > 0);

            _animator.Grounded(grounded);
            _animator.Walking(_isMoving); // FIXME not just the input, but a valid move action

        }


        // Control

        public void Move(Vector2 move) {
            // TODO receive integer movement
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
