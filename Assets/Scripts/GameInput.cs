using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions inputAction;

    public void Awake()
    {
        inputAction = new PlayerInputActions();
        inputAction.Player.Enable();
    }

    public Vector2 GetInputVectorNormalized()
    {
        Vector2 input = inputAction.Player.Move.ReadValue<Vector2>().normalized;
        return input;
    }

    public bool IsEscapePressed()
    {
        bool isEscapePresed = inputAction.Player.Menu.WasPressedThisFrame();
        return isEscapePresed;
    }
}
