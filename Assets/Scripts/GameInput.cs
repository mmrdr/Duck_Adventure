using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    [SerializeField] private GameStartedUI gameStartedUI;
    private PlayerInputActions inputAction;
    public event EventHandler OnInteractPerformed;
    
    public void Awake()
    {
        inputAction = new PlayerInputActions();
        inputAction.Player.Enable();
        inputAction.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractPerformed?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetInputVectorNormalized()
    {
        Vector2 input = inputAction.Player.Move.ReadValue<Vector2>().normalized;
        return input;
    }

    public bool IsEscapePressed()
    {
        bool isEscapePresed = inputAction.Player.Menu.WasPressedThisFrame();
        return (isEscapePresed && gameStartedUI.CanPress());
    }
}
