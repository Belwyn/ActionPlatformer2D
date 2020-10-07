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



        ////// IPlayerActions interface

        public void OnMove(InputAction.CallbackContext context) {
            if (context.started || context.canceled || context.performed) {
                Vector2 movement = context.ReadValue<Vector2>();
                _charController.Move(movement);
            }
        }

        public void OnJump(InputAction.CallbackContext context) {
            if (context.started || context.canceled || context.performed) {
                bool jumping = context.ReadValueAsButton();
                _charController.Jump(jumping);
            }
        }

        public void OnAttack(InputAction.CallbackContext context) {
            if (context.started)
                _charController.Attack();
        }
    }

}