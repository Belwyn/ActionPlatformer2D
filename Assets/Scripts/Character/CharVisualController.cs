using Belwyn.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Belwyn.ActionPlatformer.Game.Character {


    public class CharVisualController : MonoBehaviour {

        [Header("Components")]
        [SerializeField]
        private CharAnimator _animator;
        [SerializeField]
        private SpriteRenderer _spriteRenderer;



        public void Setup(ICharacterEvents _charEv) {

            _charEv.onMovementChange.AddListener(OnMovementChange);
            _charEv.onFacingRightChange.AddListener(OnFacingRightChange);
            _charEv.onGroundedChange.AddListener(OnGroundedChange);
            _charEv.onJumpingChange.AddListener(OnJumpingChange);
            _charEv.onAttackChange.AddListener(OnAttackChange);
            _charEv.onDashChange.AddListener(OnDashingChange);
            _charEv.onAirDashChange.AddListener(OnAirDashingChange);
            _charEv.onClingChange.AddListener(OnClingChange);
        }




        private void FaceDirection(bool isRight) {
            //TODO improve
            _spriteRenderer.flipX = isRight;
        }

        private void OnMovementChange(Vector2 movement) {
            _animator.Walking(movement.x < -.00001f || movement.x > .00001f);
            _animator.JumpAscension(movement.y > 0);
        }

        private void OnFacingRightChange(bool isRight) {
            FaceDirection(isRight);
        }

        private void OnGroundedChange(bool grounded) {
            _animator.Grounded(grounded);
        }

        private void OnJumpingChange(bool jumping) {
            _animator.Jump(jumping);
        }

        private void OnDashingChange(bool dashing) {
            _animator.Dash(dashing);
        }

        private void OnAirDashingChange(bool airDashing) {
            _animator.AirDash(airDashing);
        }       

        private void OnClingChange(bool cling) {
            _animator.Cling(cling);
        }



        private void OnAttackChange(bool attack) {
            //TODO
            _animator.Attack();
        }

    }

}