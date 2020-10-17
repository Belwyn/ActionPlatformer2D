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
            _charEv.onGroundedChange.AddListener(OnGroundedChange);
            _charEv.onJumpingChange.AddListener(OnJumpingChange);
            _charEv.onAttackChange.AddListener(OnAttackChange);

        }




        private void CheckFacingDirection(Vector2 move) {
            //TODO improve
            _spriteRenderer.flipX = move.x > 0 || (move.x >= 0 && _spriteRenderer.flipX);
        }



        private void OnMovementChange(Vector2 movement) {
            _animator.Walking(movement.x != 0);
            _animator.JumpAscension(movement.y > 0);
            CheckFacingDirection(movement);
        }

        private void OnGroundedChange(bool grounded) {
            _animator.Grounded(grounded);
        }

        private void OnJumpingChange(bool jumping) {
            _animator.Jump(jumping);
        }

        private void OnAttackChange(bool attack) {
            //TODO
            _animator.Attack();
        }

    }

}