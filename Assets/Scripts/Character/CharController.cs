using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Belwyn.Utils.Logger;


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
        public int jumpCount = 1;

        [Space()]
        public float jumpBufferTime = 0.1f;
        public float coyoteTime = 0.1f;
        
        [Space()]
        public float jumpSpeed  = 1f;
        public float maxFallSpeed = 1f;
        public float defaultGravity = 1f;
        public float breakJumpFactor = 1f;
        public float fallFactor  = 1f;



        private Vector2 _move;
        private bool _isMoving;
        private bool _isRight;

        private bool _tryJump;
        private bool _isJumping;
        private int _currentJumpCount;
        private float _currentJumpBuffer;
        private float _currentCoyote;


        private bool grounded { get { return _groundDetector.isGrounded && _rb.velocity.y <= .00001f; } }

        private float velx =>  _rb.velocity.x;
        private float velY =>  _rb.velocity.y;



        private void Awake() {
            ResetJumpBuffer();
        }

        private void Update() {
            if (_tryJump) {
                _currentJumpBuffer += Time.deltaTime;
            }
        }




        private void FixedUpdate() {

            HorizontalMovement();

            VerticalMovement();


            UpdateVisuals();

        }






        private void HorizontalMovement() {            

            if (_isMoving) {
                _rb.velocity = new Vector2(Mathf.Sign(_move.x) * moveSpeed, velY);
                //_rb.AddForce(new Vector2(Mathf.Sign(_move.x) * moveSpeed /* Time.deltaTime*/, 0));
            } else {
                _rb.velocity = new Vector2(0, velY);
            }
            
        }


        private void VerticalMovement() {

            if (grounded) {
                _currentJumpCount = 0;
            }

            HandleJumping();

            HandleFalling();

        }



        private void HandleJumping() {
            if (_currentJumpCount < jumpCount) {
                if (grounded) {
                    _currentCoyote = 0;
                } 
                else { 
                    _currentCoyote += Time.deltaTime;                    
                }
                if (_currentCoyote <= coyoteTime && _currentJumpBuffer <= jumpBufferTime) {
                    JumpAction();
                }
            }
        }



        private void HandleFalling() {

            // Fall speed tweak
            if (velY < -.00001f) {
                _rb.gravityScale = fallFactor;
                _isJumping = false;
            } else if (velY > 0 && !_tryJump) {
                _rb.gravityScale = breakJumpFactor;
                _isJumping = false;
            } else {
                _rb.gravityScale = defaultGravity;
            }

            // Fall speed cap
            if (_rb.velocity.y < -1 * maxFallSpeed) {
                _rb.velocity = new Vector2(_rb.velocity.x, -1 * maxFallSpeed);
            }

        }



        private void JumpAction() {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpSpeed);
            //_rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            _isJumping = true;
            _currentJumpCount++;
            ResetJumpBuffer();
        }


        private void ResetJumpBuffer() {
            _currentJumpBuffer = jumpBufferTime + 1;
        }


        private void UpdateVisuals() {
            // FIXME not only sprite flip
            if (_isMoving) {
                _spriteRenderer.flipX = _isRight;
            }

            _animator.Jump(_isJumping); // FIXME not just the input, but a valid jump action
            _animator.JumpAscension(_rb.velocity.y > 0);

            _animator.Grounded(grounded);
            _animator.Walking(_isMoving); // FIXME not just the input, but a valid move action
        }


        // Control

        public void Move(Vector2 move) {
            _isMoving = move.x != 0;
            _isRight = move.x > 0;
            _move = move;
        }


        public void Jump(bool jump) {
            _tryJump = jump;
            if (jump) {
                _currentJumpBuffer = 0f;
            } else {
                ResetJumpBuffer();
            }
        }





        public void Attack() {
            _animator.Attack();
        }

    }


}
