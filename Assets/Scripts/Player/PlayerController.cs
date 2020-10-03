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


        private void Awake() {
            
        }


        ////// IPlayerActions interface

        public void OnMove(InputAction.CallbackContext context) {
            if (context.started || context.canceled)
                _charController.Move(context.ReadValue<Vector2>());
        }

        public void OnJump(InputAction.CallbackContext context) {
            if (context.started || context.canceled)
                _charController.Jump(context.ReadValueAsButton());
        }

        public void OnAttack(InputAction.CallbackContext context) {
            if (context.started)
                _charController.Attack();
        }
    }

}