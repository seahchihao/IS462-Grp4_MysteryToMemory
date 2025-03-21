using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace BNG {

    public class ControllerInput : MonoBehaviour {
      
        [Tooltip("Input Action used to initiate action")]
        public InputActionReference InputAction;

        public UnityEvent OnPress = null;
        public UnityEvent OnRelease = null;

        private bool released = true;

        void Update() {
            if (CheckInputDown()) {
                if (released) {
                    if (OnPress!=null) OnPress.Invoke();
                    released = false;
                }
            }
            else {
                if (!released) {
                    if (OnRelease != null) OnRelease.Invoke();
                    released = true;
                }
            }
        }

        /// <summary>
        /// Returns true if InputAction is being held down
        /// </summary>
        /// <returns></returns>
        public virtual bool CheckInputDown() {            
            // Check for Unity Input Action
            if (InputAction != null) {
                return InputAction.action.ReadValue<float>() > 0f;
            }
            return false;
        }
    }
}

