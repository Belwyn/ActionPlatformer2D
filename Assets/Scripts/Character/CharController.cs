using Belwyn.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Belwyn.Utils.Logger;


namespace Belwyn.ActionPlatformer.Game.Character {

    public class CharController : MonoBehaviour, ICharacterEvents {

        [Header("Components")]
        [SerializeField]
        private CharVisualController _visualController;
        [SerializeField]
        private Rigidbody2D _rb;

        [SerializeField]
        private GroundDetector2D _groundDetector;


        [Header("Parameters")]
        public float moveSpeed  = 1f;
        public int jumpCount = 1;
        public float dashFactor = 2f;
        public float dashTime = 1f;

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
        private Vector2 move { 
            set {
                _move = value;
                _onMovementChange.Invoke(value);
            }
        }

        private bool _isMoving;
        private bool _isRight;
        private bool _isDashing;
        private float _currentDashTime = 0f;

        private bool _tryJump;
        private bool _isJumping;
        private bool isJumping {
            set {
                _isJumping = value;
                _onJumpingChange.Invoke(value);
            }
        }
        private int _currentJumpCount;
        private float _currentJumpBuffer;
        private float _currentCoyote;

        private bool _isGroundDetected;
        private bool _isGrounded = false;
        private bool grounded { 
            get { return _isGrounded; }
            set { 
                if(value != _isGrounded) {
                    _isGrounded = value;
                    onGroundedChange.Invoke(value);
                }
            }
        }

        private float velx =>  _rb.velocity.x;
        private float velY =>  _rb.velocity.y;



        [Header("Logic Events")]
        [SerializeField]
        private Vector2Event _onMovementChange;
        [SerializeField]
        private BoolEvent _onGroundedChange;
        [SerializeField]
        private BoolEvent _onJumpingChange;
        [SerializeField]
        private BoolEvent _onAttackChange;
        [SerializeField]
        private BoolEvent _onDashChange;
        
        public Vector2Event onMovementChange => _onMovementChange;
        public BoolEvent onGroundedChange => _onGroundedChange;
        public BoolEvent onJumpingChange => _onJumpingChange;
        public BoolEvent onAttackChange => _onAttackChange;
        public BoolEvent onDashChange => _onDashChange;


        private void Awake() {
            Init();
        }


        public void Start() {
            RegisterListeners();
            _visualController.Setup(this);
        }


        private void Init() {
            DisableJumpBuffer();
        }

        private void RegisterListeners() {
            // Listen to groundDetector
            _groundDetector.onGroundChange.AddListener(b => _isGroundDetected = b);
        }





        private void Update() {
            // Process jump buffer time
            if (_tryJump) {
                _currentJumpBuffer += Time.deltaTime;
            }

            // Process dash
            if (_isDashing) {
                _currentDashTime += Time.deltaTime;
            }

            // Visuals
            UpdateVisuals();
        }


        private void FixedUpdate() {

            // Move-Related Logic
            VerticalMovement();

            HorizontalMovement();
        }




        ///// Movement logic

        private void HorizontalMovement() {
            // Dash
            _isDashing = (_isDashing && (_currentDashTime <= dashTime)) || (_isDashing && !grounded);
            if (!_isDashing) {
                _currentDashTime = 0f;
            }

            // Movement
            if (_isMoving || _isDashing) {
                _rb.velocity = new Vector2((_isRight ? 1f : -1f) * moveSpeed * (_isDashing ? dashFactor : 1f), velY);
                //_rb.AddForce(new Vector2(Mathf.Sign(_move.x) * moveSpeed /* Time.deltaTime*/, 0));
            }
            else {
                _rb.velocity = new Vector2(0, velY);
            }
        }


        private void VerticalMovement() {
            // Ground logic
            grounded = _isGroundDetected && _rb.velocity.y <= .00001f;

            if (grounded) {
                _currentJumpCount = 0;
            }

            // Jump and fall
            HandleJumping();

            HandleFalling();
        }



        private void HandleJumping() {
            // Jump count limit
            if (_currentJumpCount < jumpCount) {

                UpdateCoyote();

                if ((_currentJumpCount > 0 || _currentCoyote <= coyoteTime) && _currentJumpBuffer <= jumpBufferTime) {
                    JumpAction();
                }
            }
        }



        private void HandleFalling() {

            // Fall speed tweak
            if (velY < -.00001f) {
                _rb.gravityScale = fallFactor;
                isJumping = false;
            } else if (velY > 0 && !_tryJump) {
                _rb.gravityScale = breakJumpFactor;
                isJumping = false;
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

            isJumping = true;
            _currentJumpCount++;

            DisableJumpBuffer();
        }


        private void UpdateCoyote() {
            if (grounded) {
                _currentCoyote = 0;
            }
            else {
                _currentCoyote += Time.deltaTime;
            }
        }


        private void DisableJumpBuffer() {
            _currentJumpBuffer = jumpBufferTime + 1;
        }

        private void BeginJumpBuffer() {
            _currentJumpBuffer = 0f;
        }





        ///// Visuals
        
        private void UpdateVisuals() {
            _onMovementChange.Invoke(_rb.velocity);
            _onDashChange.Invoke(_isDashing);
            _onGroundedChange.Invoke(grounded);
            _onJumpingChange.Invoke(_isJumping);
        }





        ///// Control

        public void Move(Vector2 move) {
            _isMoving = move.x != 0;
            if (move.x != 0) {
                _isRight = move.x > 0;
            }
            _move = move;
        }


        public void Jump(bool jump) {
            _tryJump = jump;
            if (jump) {
                BeginJumpBuffer();
            } 
            else {
                DisableJumpBuffer();
            }
        }


        public void Dash(bool dash) {
            _isDashing = grounded ? dash : false;
        }



        // TODO
        public void Attack() {
            _onAttackChange.Invoke(true);
        }

    }


}
