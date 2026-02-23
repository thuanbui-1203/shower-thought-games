using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private InputActionAsset inputSystem;
    //assigning the input system in the inspector, make sure to set up the input actions properly for movement and rotation.
    private InputAction moveAction;
    private Vector2 moveInput;
    private void Start()
    {
        if (inputSystem != null)
        {
            inputSystem.Enable();
        }
        moveAction = inputSystem.FindAction("Move");
    }
    private void OnDisable()
    {
        if (inputSystem != null)
        {
            inputSystem.Disable();
        }
    }
    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();

        if (moveAction.IsPressed())
        {
            Vector2 moveDirection = new Vector2(moveInput.x, moveInput.y).normalized;
            transform.Translate(moveSpeed * Time.deltaTime * moveDirection, Space.World);
        }
        
    }
}
