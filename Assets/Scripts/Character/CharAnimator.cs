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


        [SerializeField]
        private Animator _animator;

        





        public void Attack() {
            _animator.SetTrigger(ATTACK);
        }


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


    }

}