using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Belwyn.ActionPlatformer.Game.Character {

    public class CharAnimator : MonoBehaviour {

        private const string ATTACK         = "Slash";
        private const string JUMP           = "Jump";
        private const string JUMP_ASCENDING = "Jump_Ascending";
        private const string GROUNDED       = "Grounded";
        private const string WALKING        = "Walking";
        private const string DASHING        = "Dashing";
        private const string AIR_DASHING    = "Air_Dashing";
        private const string CLINGING       = "Clinging";


        [SerializeField]
        private Animator _animator;


        public void Jump(bool value) {
            _animator.SetBool(JUMP, value);
        }


        public void JumpAscension(bool value) {
            _animator.SetBool(JUMP_ASCENDING, value);
        }


        public void Grounded(bool value) {
            _animator.SetBool(GROUNDED, value);
        }


        public void Walking(bool value) {
            _animator.SetBool(WALKING, value);
        }

        public void Dash(bool dashing) {
            _animator.SetBool(DASHING, dashing);
        }

        public void AirDash(bool airDashing) {
            _animator.SetBool(AIR_DASHING, airDashing);
        }

        public void Cling(bool clinging) {
            _animator.SetBool(CLINGING, clinging);
        }


        // TODO
        public void Attack() {
            _animator.SetTrigger(ATTACK);
        }
    }

}