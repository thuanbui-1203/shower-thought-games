using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static GameInput Instance { get; private set; }
    public event EventHandler OnMenuButtonPressed;
    private InputActions inputActions;


    private void Awake()
    {
        Instance = this;
        inputActions = new InputActions();
        inputActions.Enable();

        inputActions.Player.MenuAction.performed += Menu_performed;
    }

    private void Menu_performed(InputAction.CallbackContext context)
    {
        OnMenuButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    public bool IsUpActionPressed()
    {
        return inputActions.Player.LanderUp.IsPressed();
    }

    public bool IsLeftActionPressed()
    {
        return inputActions.Player.LanderLeft.IsPressed();
    }

    public bool IsRightActionPressed()
    {
        return inputActions.Player.LanderRight.IsPressed();
    }
    private void OnDestroy()
    {
        inputActions.Disable();
    }


    // Update is called once per frame
    void Update()
    {

    }
}
