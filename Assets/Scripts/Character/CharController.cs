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
        private SurfaceDetector2D _groundDetector;
        [SerializeField]
        private SurfaceDetector2D _leftWallDetector;
        [SerializeField]
        private SurfaceDetector2D _rightWallDetector;


        [Header("Parameters")]
        public float moveSpeed  = 1f;
        public int jumpCount = 1;
        public float dashFactor = 2f;
        public float dashTime = 1f;
        public int dashCount = 1;

        [Space()]
        public float jumpBufferTime = 0.1f;
        public float coyoteTime = 0.1f;
        
        [Space()]
        public float jumpSpeed  = 1f;
        public float maxFallSpeed = 1f;
        public float defaultGravity = 1f;
        public float breakJumpFactor = 1f;
        public float fallFactor  = 1f;

        [Space()]
        public PhysicsMaterial2D stopMaterial;
        public PhysicsMaterial2D movementMaterial;

        private Vector2 _move;

        private bool _isMoving;
        private bool _isRight;
        private bool _changedDirection;
        private bool _tryDashing;
        private bool _isDashing;
        private float _currentDashTime = 0f;
        private bool _aerialDash;
        private int _currentDashCount;

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

        private bool _isClinging;

        //private bool _isGroundDetected;
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

        private bool _againstLeftWall;
        private bool againstLeftWall {
            get { return _againstLeftWall; }
            set {
                if (value != _againstLeftWall) {
                    _againstLeftWall = value;
                    /// EVENT ?
                }
            }
        }

        private bool _againstRightWall;
        private bool againstRightWall {
            get { return _againstRightWall; }
            set {
                if (value != _againstRightWall) {
                    _againstRightWall = value;
                    /// EVENT ?
                }
            }
        }

        private float velx =>  _rb.velocity.x;
        private float velY =>  _rb.velocity.y;



        [Header("Logic Events")]
        [SerializeField]
        private Vector2Event _onMovementChange;
        [SerializeField]
        private BoolEvent _onFacingRightChange;
        [SerializeField]
        private BoolEvent _onGroundedChange;
        [SerializeField]
        private BoolEvent _onJumpingChange;
        [SerializeField]
        private BoolEvent _onAttackChange;
        [SerializeField]
        private BoolEvent _onDashChange;
        [SerializeField]
        private BoolEvent _onAirDashChange;
        [SerializeField]
        private BoolEvent _onClingChange;

        public Vector2Event onMovementChange => _onMovementChange;
        public BoolEvent onFacingRightChange => _onFacingRightChange;
        public BoolEvent onGroundedChange => _onGroundedChange;
        public BoolEvent onJumpingChange => _onJumpingChange;
        public BoolEvent onAttackChange => _onAttackChange;
        public BoolEvent onDashChange => _onDashChange;
        public BoolEvent onAirDashChange => _onAirDashChange;
        public BoolEvent onClingChange => _onClingChange;


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
            // Listen to surfaceDetectors
            _groundDetector.onSurfacedChange.AddListener(b => grounded = b);
            _leftWallDetector.onSurfacedChange.AddListener(b => againstLeftWall = b);
            _rightWallDetector.onSurfacedChange.AddListener(b => againstRightWall = b);
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
            // Update movement-related character state
            VerticalStateUpdate();

            HorizontalStateUpdate();

            // Prepare
            PreparePhysics();

            // Move-Related Logic
            VerticalMovement();

            HorizontalMovement();
        }



        ///// Movement State update     
        private void VerticalStateUpdate() {
            // Grounded
            //grounded = _isGroundDetected;// && _rb.velocity.y <= .00001f;

            if (grounded && !_isJumping) {
                _aerialDash = false;
                _currentDashCount = 0;
                _currentJumpCount = 0;
            }
        }

        private void HorizontalStateUpdate() {
            // Moving & facing
            _isMoving = _move.x < -.00001f || _move.x > .00001f;
            bool wasRight = _isRight;
            if (_move.x < -.00001f || _move.x > .00001f) {
                _isRight = _move.x > 0;
            }
            _changedDirection = wasRight != _isRight;

            _isClinging = !_isGrounded && !_isJumping && velY <= 0.0001f && isMovingAgainstWall();
            if (_isClinging) {
                _isDashing = false;
                _aerialDash = false;
            }
        }


        private bool isMovingAgainstWall() {
            bool againstWall = (_isMoving) && ( (_isRight && againstRightWall) || (!_isRight && againstLeftWall) );
            return againstWall;
        }


        ///// Assign physics materials for movement
        private void PreparePhysics() {
            // TODO _isDashing is changed after this, it's potentially one frame behing if no fixedUpdates remain in current frame
            if (_isGrounded && !_isJumping && !_isMoving && !_isDashing) {
                _rb.sharedMaterial = stopMaterial;
            }
            else {
                _rb.sharedMaterial = movementMaterial;
            }
        }




        ///// Movement logic
        private void HorizontalMovement() {

            // Dashing
            HandleDashing();

            // Movement
            if ((_isMoving || _isDashing) && !isMovingAgainstWall()) {
                float right = _isRight ? 1f : -1f;
                float dashing = _isDashing ? dashFactor : 1f;
                float aerialDash = _aerialDash ? 0f : velY;
                _rb.velocity = new Vector2(right * moveSpeed * dashing, aerialDash);
                //_rb.AddForce(new Vector2(Mathf.Sign(_move.x) * moveSpeed /* Time.deltaTime*/, 0));
            }
            else {
                _rb.velocity = new Vector2(0, velY);
            }
        }


        private void HandleDashing() {
            if (!_tryDashing || _changedDirection || _isClinging) {
                _isDashing = false;
            }
            else {
                // Dash count limit
                if (_isDashing || _currentDashCount < dashCount) {

                    if (!_isDashing && _tryDashing) {
                        _isDashing = true;
                        _aerialDash = !grounded;
                        _currentDashCount++;
                    }
                    else {
                        _isDashing = _tryDashing && (_currentDashTime <= dashTime || (_isDashing && !grounded && !_aerialDash));
                    }
                }
            }

            if (!_isDashing) {
                _aerialDash = false;
                _currentDashTime = 0f;
                _tryDashing = false;
            }

        }



        private void VerticalMovement() {
            // Jump and fall
            HandleJumping();

            HandleFalling();
        }



        private void HandleJumping() {
            // Jump count limit
            if (_currentJumpCount < jumpCount && !_isJumping || _isClinging) {

                UpdateCoyote();

                if ((_currentJumpCount > 0 || _currentCoyote <= coyoteTime) && _currentJumpBuffer <= jumpBufferTime) {
                    JumpAction();
                }
            }
        }



        private void HandleFalling() {
            // Fall speed tweak
            if (_aerialDash || _isClinging) {
                _rb.velocity = new Vector2(velx, 0f);
                _rb.gravityScale = 0f;
            }
            else if (velY < -.00001f) {
                _rb.gravityScale = fallFactor;
                isJumping = false;
            }
            else if (velY > 0 && !_tryJump) {
                _rb.gravityScale = breakJumpFactor;
                isJumping = false;
            }
            else {
                _rb.gravityScale = defaultGravity;
            }

            // Fall speed cap
            if (_rb.velocity.y < -1 * maxFallSpeed) {
                _rb.velocity = new Vector2(_rb.velocity.x, -1 * maxFallSpeed);
            }
        }



        private void JumpAction() {
            // If aerial withouth jumping and not coyote time, reduce one jump
            if (!grounded && _currentJumpCount == 0 && _currentCoyote > coyoteTime) {
                _currentJumpCount++;
            }

            _rb.velocity = new Vector2(_rb.velocity.x, jumpSpeed);
            //_rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);

            isJumping = true;
            // Wall jump is free
            if (_isClinging) {
                // TODO review this
                _currentDashCount = 0;
                _currentJumpCount = 1;
            }
            else {
                _currentJumpCount++;
            }

            _isClinging = false;
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
            //if (_changedDirection) 
                _onFacingRightChange.Invoke(_isRight);
            _onDashChange.Invoke(_isDashing);
            _onGroundedChange.Invoke(grounded);
            _onJumpingChange.Invoke(_isJumping);
            _onAirDashChange.Invoke(_aerialDash);
            _onClingChange.Invoke(_isClinging);
        }





        ///// Control

        public void Move(Vector2 move) {
            _move = move;
        }


        public void Jump(bool jump) {
            _tryJump = jump;

            // Jumping buffers
            if (_tryJump) {
                BeginJumpBuffer();
            }
            else {
                DisableJumpBuffer();
            }
        }


        public void Dash(bool dash) {
            _tryDashing = dash;
        }



        // TODO
        public void Attack() {
            _onAttackChange.Invoke(true);
        }


#if TEST_BUILD
        private void OnGUI() {
            GUILayout.Label($"Velocity: {_rb.velocity.ToString("E")}");
            GUILayout.Label($"IsRight: {_isRight}");
            GUILayout.FlexibleSpace();
            GUILayout.Label($"TryDashig: {_tryDashing}");
            GUILayout.Label($"IsDashing: {_isDashing}");
            GUILayout.Label($"AirDash:   {_aerialDash}");
            GUILayout.Label($"DashCount: {_currentDashCount}");
            GUILayout.Label($"DashTime:  {_currentDashTime}");
            GUILayout.FlexibleSpace();
            GUILayout.Label($"Grounded {grounded}");
            GUILayout.Label($"Jumping {_isJumping}");
            GUILayout.Label($"LeftWall {_againstLeftWall}");
            GUILayout.Label($"RightWall {_againstRightWall}");
            GUILayout.Label($"Clinging {_isClinging}");
        }
#endif


    }


}
