using Belwyn.ActionPlatformer.Game.Character;
using Belwyn.ActionPlatformer.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Belwyn.ActionPlatformer.Game.Player {

    public class PlayerController : MonoBehaviour, MainInput.IPlayerActions {

        [SerializeField]
        private CharController _charController;

        private bool _moving = false;
        private float _movement;

        private bool _grounded;
        private bool _jumping = false;

        private void Awake() {
            
        }


        private void FixedUpdate() {       
            

            if (_moving)
                _charController.Move(new Vector2(_movement, 0));


            //if (_jumping)
            _charController.Jump(_jumping);


        }



        ////// IPlayerActions interface

        public void OnMove(InputAction.CallbackContext context) {
            if (context.started || context.canceled || context.performed) {
                //_charController.Move(context.ReadValue<Vector2>());
                _movement = context.ReadValue<float>();
                _moving = true;
            } 
            else {
                _moving = false;
            }
        }

        public void OnJump(InputAction.CallbackContext context) {
            if (context.started || context.canceled || context.performed)
                //_charController.Jump(context.ReadValueAsButton());
                _jumping = context.ReadValueAsButton();
        }

        public void OnAttack(InputAction.CallbackContext context) {
            if (context.started)
                _charController.Attack();
        }
    }

}