﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BNG {

    public class VREmulator : MonoBehaviour {

        [Header("Enable / Disable : ")]
        [Tooltip("Use Emulator if true and HMDIsActive is false")]
        public bool EmulatorEnabled = true;

        [Tooltip("Set to false if you want to use in standalone builds as well as the editor")]
        public bool EditorOnly = true;

        [Tooltip("If true the game window must have focus for the emulator to be active")]
        public bool RequireGameFocus = true;

        [Tooltip("Set to true if you want the cursor to be locked on Start. Otherwise the cursor will be locked with right click / lock screen input action")]
        public bool AutoLockCursor = false;
        private bool _cursorLocked = false;

        [Tooltip("AutoLockCursor must be enabled. If set to true, right click will act as right side grip. This way you can use right click for grab, and left click for trigger.")]
        public bool RightClickGrab = false;

        public bool emulatorBugFix = false; //added

        [Header("Input : ")]
        [SerializeField]
        [Tooltip("Action set used specifically to mimic or supplement a vr setup")]
        public InputActionAsset EmulatorActionSet;

        [Header("Player Teleportation")]
        [Tooltip("Will set the PlayerTeleport component's ForceStraightArrow = true while the emulator is active.")]
        public bool ForceStraightTeleportRotation = true;

        [Header("Move Player Up / Down")]
        [Tooltip("If true, move the player eye offset up / down whenever PlayerUpAction / PlayerDownAction is called.")]
        public bool AllowUpDownControls = true;

        [Tooltip("If true, the players height will be elevated in the BNGPlayerController")]
        public bool ElevatePlayerHeightOnDisconnect = true;

        [Tooltip("Unity Input Action used to move the player up")]
        public InputActionReference PlayerUpAction;

        [Tooltip("Unity Input Action used to move the player down")]
        public InputActionReference PlayerDownAction;

        [Tooltip("Minimum height in meters the player can shrink to when using the PlayerDownAction")]
        public float MinPlayerHeight = 0.2f;

        [Tooltip("Maximum height in meters the player can grow to when using the PlayerUpAction")]
        public float MaxPlayerHeight = 5f;

        [Header("Head Look")]
        [Tooltip("Unity Input Action used to lock the camera in game mode to look around")]
        public InputActionReference LockCameraAction;

        [Tooltip("Unity Input Action used to lock the camera in game mode to look around")]
        public InputActionReference CameraLookAction;

        [Tooltip("Multiply the CameraLookAction by this amount")]
        public float CameraLookSensitivityX = 0.1f;

        [Tooltip("Multiply the CameraLookAction by this amount")]
        public float CameraLookSensitivityY = 0.1f;

        [Tooltip("Minimum local Eulers degrees the camera can rotate")]
        public float MinimumCameraY = -90f;

        [Tooltip("Minimum local Eulers degrees the camera can rotate")]
        public float MaximumCameraY = 90f;

        [Header("Controller Emulation")]
        [Tooltip("Unity Input Action used to mimic holding the Left Grip")]
        public InputActionReference LeftGripAction;

        [Tooltip("Unity Input Action used to mimic holding the Left Trigger")]
        public InputActionReference LeftTriggerAction;

        [Tooltip("Unity Input Action used to mimic having your thumb near a button")]
        public InputActionReference LeftThumbNearAction;

        [Tooltip("Unity Input Action used to move mimic holding the Right Grip")]
        public InputActionReference RightGripAction;

        [Tooltip("Unity Input Action used to move mimic holding the Right Grip")]
        public InputActionReference RightTriggerAction;

        [Tooltip("Unity Input Action used to mimic having your thumb near a button")]
        public InputActionReference RightThumbNearAction;

        [Header("Remote Grabber Visualization")]
        [Tooltip("Enable this object when remote grabber is enabled and nothing held in LeftPlayerGrabber. Leave empty to disable this feature.")]
        public GameObject LeftRemoteGrabberPreviewObject;

        [Tooltip("Enable this object when remote grabber is enabled and nothing held in RightPlayerGrabber. Leave empty to disable this feature.")]
        public GameObject RightRemoteGrabberPreviewObject;

        float mouseRotationX;
        float mouseRotationY;

        Transform mainCameraTransform;
        Transform leftControllerTranform;
        Transform rightControllerTranform;

        Transform leftHandAnchor;
        Transform rightHandAnchor;

        BNGPlayerController player;
        SmoothLocomotion smoothLocomotion;
        PlayerTeleport playerTeleport;
        bool didFirstActivate = false;

        Grabber grabberLeft;
        Grabber grabberRight;

        private float _originalPlayerYOffset = 1.65f;

        [Header("Shown for Debug : ")]
        public bool HMDIsActive;

        public Vector3 LeftControllerPosition = new Vector3(-0.2f, -0.2f, 0.5f);
        public Vector3 RightControllerPosition = new Vector3(0.2f, -0.2f, 0.5f);

        bool priorStraightSetting;

        bool emulatorWasActive = false;
        bool emulatorActivatedBefore = false;

        void Start() {

            if (GameObject.Find("CameraRig")) {
                mainCameraTransform = GameObject.Find("CameraRig").transform;
            }
            // Oculus Rig Setup
            else if (GameObject.Find("OVRCameraRig")) {
                mainCameraTransform = GameObject.Find("OVRCameraRig").transform;
            }

            leftHandAnchor = GameObject.Find("LeftHandAnchor").transform;
            rightHandAnchor = GameObject.Find("RightHandAnchor").transform;

            leftControllerTranform = GameObject.Find("LeftControllerAnchor").transform;
            rightControllerTranform = GameObject.Find("RightControllerAnchor").transform;

            player = FindObjectOfType<BNGPlayerController>();

            if (player) {
                // Use this to keep our head up high
                player.ElevateCameraIfNoHMDPresent = ElevatePlayerHeightOnDisconnect;
                _originalPlayerYOffset = player.ElevateCameraHeight;

                smoothLocomotion = player.GetComponentInChildren<SmoothLocomotion>(true);

                // initialize component if it's currently disabled
                if (smoothLocomotion != null && !smoothLocomotion.isActiveAndEnabled) {
                    smoothLocomotion.CheckControllerReferences();
                }

                playerTeleport = player.GetComponentInChildren<PlayerTeleport>(true);
                if (playerTeleport) {
                    priorStraightSetting = playerTeleport.ForceStraightArrow;
                }

                if (smoothLocomotion == null) {
                    Debug.Log("No Smooth Locomotion component found. Will not be able to use SmoothLocomotion without calling it manually.");
                } else if (smoothLocomotion.MoveAction == null) {
                    Debug.Log("Smooth Locomotion Move Action has not been assigned. Make sure to assign this in the inspector if you want to be able to move around using the VR Emulator.");
                }

                //added
                if (emulatorBugFix) {
                    Vector3 pos = transform.position;
                    pos.y = 0.5f;
                    transform.position = pos;
                    playerTeleport.TeleportYOffset = 1.5f;
                }
            }
        }

        public virtual void LockCursor() {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            _cursorLocked = true;
        }

        public virtual void UnlockCursor() {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            _cursorLocked = false;
        }

        public void OnBeforeRender() {
            HMDIsActive = InputBridge.Instance.HMDActive;

            // Ready to go
            if (EmulatorEnabled && !HMDIsActive) {
                UpdateControllerPositions();
            }
        }

        void onFirstActivate() {
            UpdateControllerPositions();

            didFirstActivate = true;
        }

        void Update() {

            //// Considerd absent if specified or unknown status
            // bool userAbsent = XRDevice.userPresence == UserPresenceState.NotPresent || XRDevice.userPresence == UserPresenceState.Unknown;
            // Updated to show in Debug Settings
            HMDIsActive = InputBridge.Instance.HMDActive;

            if (emulatorWasActive != HMDIsActive) {
                OnEmulatorStateChange();
            }

            // Ready to go
            if (EmulatorEnabled && !HMDIsActive) {

                // Check cursor lock
                // Toggle cursor lock state when Escape is pressed
                if (_cursorLocked && Input.GetKeyDown(KeyCode.Escape)) {
                    UnlockCursor();
                } 
                // Lock cursor when clicked in again
                else if (AutoLockCursor && !_cursorLocked && Input.GetMouseButtonDown(0)) {
                    // Wait briefly so any UI events can fire
                    Invoke("LockCursor", 0.1f);
                }

                if (!didFirstActivate) {
                    onFirstActivate();
                }

                // Require focus
                if (HasRequiredFocus()) {
                    CheckHeadControls();

                    UpdateControllerPositions();

                    CheckPlayerControls();
                }
            }

            UpdateRemoteGrabberPreviews();

            // Device came online after emulator had started
            if (EmulatorEnabled && didFirstActivate && HMDIsActive) {
                ResetAll();
            }

            /// Update our state
            emulatorWasActive = HMDIsActive;
        }

        void OnEmulatorStateChange() {
            // Switched from vr back to flat screen
            if (!HMDIsActive) {
                OnSwitchToPC();
            }
            // Flat screen back to VR
            else {
                OnSwitchToVR();
            }
        }

        public virtual void OnSwitchToVR() {
            mainCameraTransform.localPosition = Vector3.zero;
            mainCameraTransform.localEulerAngles = Vector3.zero;

            // No longer elevate camera now that hmd is back online
            if(emulatorActivatedBefore || didFirstActivate) {
                player.ElevateCameraIfNoHMDPresent = false;
            }
        }

        public virtual void OnSwitchToPC() {
            emulatorActivatedBefore = true;
            mainCameraTransform.localEulerAngles = Vector3.zero;
            ResetHands();
        }

        public virtual bool HasRequiredFocus() {

            // No Focus Required
            if(EditorOnly == false || RequireGameFocus == false) {
                return true;
            }

            return Application.isFocused;
        }

        

        public void CheckHeadControls() {

            // Hold LockCameraAction (example : right mouse button down ) to move camera around
            if (LockCameraAction != null) {

                bool doMouseLook = (AutoLockCursor && _cursorLocked) || (!AutoLockCursor && LockCameraAction.action.ReadValue<float>() == 1);

                // Lock
                if (doMouseLook) {

                    // Lock Camera and cursor
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                   
                    // Make sure cursor is indeed locked / invisible
                    if(!Cursor.visible && Cursor.lockState == CursorLockMode.Locked) {
                        Vector3 mouseLook = Vector2.zero;
                        if (CameraLookAction != null) {
                            mouseLook = CameraLookAction.action.ReadValue<Vector2>();
                        }
                        // Fall back to mouse
                        else {
                            mouseLook = Mouse.current.delta.ReadValue();
                        }
                        // Rotation Y
                        mouseRotationY += mouseLook.y * CameraLookSensitivityY;

                        mouseRotationY = Mathf.Clamp(mouseRotationY, MinimumCameraY, MaximumCameraY);
                        mainCameraTransform.localEulerAngles = new Vector3(-mouseRotationY, mainCameraTransform.localEulerAngles.y, 0);

                        // Move PLayer on X Axis
                        player.transform.Rotate(0, mouseLook.x * CameraLookSensitivityX, 0);
                    }
                   
                }
                // Unlock Camera
                else if(!AutoLockCursor && LockCameraAction.action.ReadValue<float>() == 0) {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
        }        

        float prevVal;
        /// <summary>
        /// Overwrite InputBridge inputs with our own bindings
        /// </summary>
        public void UpdateInputs() {

            // Only override controls if no hmd is active and this script is enabled
            if (EmulatorEnabled == false || HMDIsActive) {
                return;
            }

            // Window doesn't have focus
            if(!HasRequiredFocus()) {
                return;
            }

            // Make sure grabbers are assigned
            checkGrabbers();

            // Simulate Left Controller states
            if (LeftTriggerAction != null) {
                prevVal = InputBridge.Instance.LeftTrigger;
                InputBridge.Instance.LeftTrigger = LeftTriggerAction.action.ReadValue<float>();
                InputBridge.Instance.LeftTriggerDown = prevVal < InputBridge.Instance.DownThreshold && InputBridge.Instance.LeftTrigger >= InputBridge.Instance.DownThreshold;
                InputBridge.Instance.LeftTriggerUp = prevVal > InputBridge.Instance.DownThreshold && InputBridge.Instance.LeftTrigger < InputBridge.Instance.DownThreshold;
            }

            if (LeftGripAction != null) {
                prevVal = InputBridge.Instance.LeftGrip;
                InputBridge.Instance.LeftGrip = LeftGripAction.action.ReadValue<float>();
                InputBridge.Instance.LeftGripDown = prevVal < InputBridge.Instance.DownThreshold && InputBridge.Instance.LeftGrip >= InputBridge.Instance.DownThreshold;
            }

            if(LeftThumbNearAction != null) {
                InputBridge.Instance.LeftThumbNear = LeftThumbNearAction.action.ReadValue<float>() == 1;
            }

            // Simulate Right Controller states
            if (RightTriggerAction!= null) {
                float rightTriggerVal = RightTriggerAction.action.ReadValue<float>();

                prevVal = InputBridge.Instance.RightTrigger;
                InputBridge.Instance.RightTrigger = RightTriggerAction.action.ReadValue<float>();
                InputBridge.Instance.RightTriggerDown = prevVal < InputBridge.Instance.DownThreshold && InputBridge.Instance.RightTrigger >= InputBridge.Instance.DownThreshold;
                InputBridge.Instance.RightTriggerUp = prevVal > InputBridge.Instance.DownThreshold && InputBridge.Instance.RightTrigger < InputBridge.Instance.DownThreshold;
            }

            if (RightGripAction != null) {
                prevVal = InputBridge.Instance.RightGrip;
                InputBridge.Instance.RightGrip = RightGripAction.action.ReadValue<float>();

                // Simulate grip with right click if option is enabled
                if(AutoLockCursor && RightClickGrab && Input.GetMouseButton(1)) {
                    InputBridge.Instance.RightGrip = 1f;
                }

                InputBridge.Instance.RightGripDown = prevVal < InputBridge.Instance.DownThreshold && InputBridge.Instance.RightGrip >= InputBridge.Instance.DownThreshold;
            }

            if(RightThumbNearAction) {
                InputBridge.Instance.RightThumbNear = RightThumbNearAction.action.ReadValue<float>() == 1;
            }
        }

        public void CheckPlayerControls() {

            // Require focus
            if(EditorOnly && !Application.isEditor) {
                return;
            }

            // Player Up / Down
            if(AllowUpDownControls) {
                if (PlayerUpAction != null && PlayerUpAction.action.ReadValue<float>() == 1) {
                    player.ElevateCameraHeight = Mathf.Clamp(player.ElevateCameraHeight + Time.deltaTime, MinPlayerHeight, MaxPlayerHeight);
                }
                else if (PlayerDownAction != null && PlayerDownAction.action.ReadValue<float>() == 1) {
                    player.ElevateCameraHeight = Mathf.Clamp(player.ElevateCameraHeight - Time.deltaTime, MinPlayerHeight, MaxPlayerHeight);
                }
            }

            // Force Forward Arrow
            if(ForceStraightTeleportRotation && playerTeleport != null && playerTeleport.ForceStraightArrow == false) {
                playerTeleport.ForceStraightArrow = true;
            }

            // Player Move Forward / Back, Snap Turn
            if (smoothLocomotion != null && smoothLocomotion.enabled == false) {
                // Manually allow player movement if the smooth locomotion component is disabled
                smoothLocomotion.CheckControllerReferences();
                smoothLocomotion.UpdateInputs();

                if(smoothLocomotion.ControllerType == PlayerControllerType.CharacterController) {
                    smoothLocomotion.MoveCharacter();
                }
                else if (smoothLocomotion.ControllerType == PlayerControllerType.Rigidbody) {
                    smoothLocomotion.MoveRigidCharacter();
                }
            }
        }

        //void FixedUpdate() {
        //    // Player Move Forward / Back, Snap Turn
        //    //if (smoothLocomotion != null && smoothLocomotion.enabled == false && smoothLocomotion.ControllerType == PlayerControllerType.Rigidbody) {
        //    //    smoothLocomotion.MoveRigidCharacter();
        //    //}
        //}

        public virtual void UpdateControllerPositions() {
            leftControllerTranform.transform.localPosition = LeftControllerPosition;
            leftControllerTranform.transform.localEulerAngles = Vector3.zero;

            rightControllerTranform.transform.localPosition = RightControllerPosition;
            rightControllerTranform.transform.localEulerAngles = Vector3.zero;
        }


        public virtual void UpdateRemoteGrabberPreviews() {

            // Only show preview in Emulator Mode
            if(HMDIsActive) {
                if(LeftRemoteGrabberPreviewObject != null && LeftRemoteGrabberPreviewObject.activeSelf) {
                    LeftRemoteGrabberPreviewObject.SetActive(false);
                }
                if (RightRemoteGrabberPreviewObject != null && RightRemoteGrabberPreviewObject.activeSelf) {
                    RightRemoteGrabberPreviewObject.SetActive(false);
                }
                return;
            }

            // Object was specified so we can check to activate it or not
            if(LeftRemoteGrabberPreviewObject != null && grabberLeft != null) {
                LeftRemoteGrabberPreviewObject.SetActive(!grabberLeft.HoldingItem && !grabberLeft.RemoteGrabbingItem);
            }
            if (RightRemoteGrabberPreviewObject != null && grabberRight != null) {
                RightRemoteGrabberPreviewObject.SetActive(!grabberRight.HoldingItem && !grabberRight.RemoteGrabbingItem);
            }
        }


        void checkGrabbers() {
            // Find Grabber Left
            if (grabberLeft == null || !grabberLeft.isActiveAndEnabled) {
                Grabber[] grabbers = FindObjectsOfType<Grabber>();

                for (var x = 0; x < grabbers.Length; x++) {
                    if (grabbers[x] != null && grabbers[x].isActiveAndEnabled && grabbers[x].HandSide == ControllerHand.Left) {
                        grabberLeft = grabbers[x];
                    }
                }
            }

            // Find Grabber Right
            if (grabberRight == null || !grabberRight.isActiveAndEnabled) {
                Grabber[] grabbers = FindObjectsOfType<Grabber>();
                for (var x = 0; x < grabbers.Length; x++) {
                    if (grabbers[x] != null && grabbers[x].isActiveAndEnabled && grabbers[x].HandSide == ControllerHand.Right) {
                        grabberRight = grabbers[x];
                    }
                }
            }
        }

        public virtual void ResetHands() {
            leftControllerTranform.transform.localPosition = Vector3.zero;
            leftControllerTranform.transform.localEulerAngles = Vector3.zero;

            rightControllerTranform.transform.localPosition = Vector3.zero;
            rightControllerTranform.transform.localEulerAngles = Vector3.zero;
        }

        public virtual void ResetAll() {

            ResetHands();

            // Reset Camera
            mainCameraTransform.localEulerAngles = Vector3.zero;

            // Reset Player
            if (player) {
                player.ElevateCameraHeight = _originalPlayerYOffset;
            }

            // Reset Teleport Status
            if(ForceStraightTeleportRotation && playerTeleport) {
                playerTeleport.ForceStraightArrow = priorStraightSetting;
            }

            didFirstActivate = false;
        }

        void OnEnable() {

            if (EmulatorActionSet != null) {
                foreach (var map in EmulatorActionSet.actionMaps) {
                    foreach (var action in map) {
                        if(action != null) {
                            action.Enable();
                        }
                    }
                }
            }

            // Subscribe to input events
            InputBridge.OnInputsUpdated += UpdateInputs;

            Application.onBeforeRender += OnBeforeRender;
        }

        void OnDisable() {

            // Disable Input Actions
            if (EmulatorActionSet != null) {
                foreach (var map in EmulatorActionSet.actionMaps) {
                    foreach (var action in map) {
                        if (action != null) {
                            action.Disable();
                        }
                    }
                }
            }

            Application.onBeforeRender -= OnBeforeRender;

            if (isQuitting) {
                return;
            }

            // Reset Hand Positions
            ResetAll();

            // Unsubscribe from input events
            InputBridge.OnInputsUpdated -= UpdateInputs;
        }

        bool isQuitting = false;
        void OnApplicationQuit() {
            isQuitting = true;
        }
    }
}
