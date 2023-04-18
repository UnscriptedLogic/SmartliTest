using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private DefaultControls defaultControls;
    private DefaultControls.DefaultActions controls;

    public event Action<Vector2, Vector2> OnMouseMoving;
    public event Action<Vector2> OnDirectionalMovement;
    public event Action OnJumpPressed;

    public static InputManager instance { get; private set; }

    private void Awake()
    {
        instance = this;
        defaultControls = new DefaultControls();
        controls = defaultControls.Default;
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.MouseDelta.performed += MouseDelta_started;
        controls.MouseDelta.canceled += MouseDelta_started;
        controls.Move.performed += DirectionalMovement_started;
        controls.Move.canceled += DirectionalMovement_canceled;
        controls.Jump.performed += Jump_performed;
    }

    private void Jump_performed(InputAction.CallbackContext obj) => OnJumpPressed?.Invoke();

    private void DirectionalMovement_started(InputAction.CallbackContext obj) => OnDirectionalMovement?.Invoke(obj.ReadValue<Vector2>());
    private void DirectionalMovement_canceled(InputAction.CallbackContext obj) => OnDirectionalMovement?.Invoke(obj.ReadValue<Vector2>());

    private void MouseDelta_started(InputAction.CallbackContext obj)
    {
        Vector2 delta = obj.ReadValue<Vector2>();
        Vector2 position = controls.MousePosition.ReadValue<Vector2>();
        OnMouseMoving?.Invoke(position, delta);
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
