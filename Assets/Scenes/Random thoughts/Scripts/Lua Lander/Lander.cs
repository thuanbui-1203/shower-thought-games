using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Lander : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2D;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Keyboard.current.upArrowKey.isPressed)
        {
            Debug.Log("up key pressed");
            Vector3 v3_up = Vector3.up;
            float force = 70f;
            rigidbody2D.AddForce(force * Time.deltaTime * v3_up, ForceMode2D.Impulse);
        }

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            Debug.Log("left key pressed");
            float turnSpeed = 100f;
            rigidbody2D.AddTorque(turnSpeed * Time.deltaTime);
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            Debug.Log("right key pressed");
            float turnSpeed = -100f;
            rigidbody2D.AddTorque(turnSpeed * Time.deltaTime);
        }
    }
    private void Update()
    {


    }
}
