using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Belwyn.ActionPlatformer.Game.Character {

    public class CharController : MonoBehaviour {


        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        

        public void Move(Vector2 move) {

            bool isMoving = move.x != 0;
            bool isRight = move.x > 0;

            _animator.SetBool("Walking", isMoving);
            if (isMoving) {
                _spriteRenderer.flipX = isRight;
            }


        }


        public void Jump(bool jump) {
            _animator.SetBool("Jump", jump);
            _animator.SetBool("Jump_Ascending", jump);
            _animator.SetBool("Grounded", !jump);
        }


        public void Attack() {
            _animator.SetTrigger("Slash");
        }


    }


}
