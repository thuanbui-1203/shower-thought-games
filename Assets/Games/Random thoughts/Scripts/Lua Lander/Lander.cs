using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Lander : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float turnSpeed = 100f;
    [SerializeField] private float force = 70f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed)
        {
            Debug.Log("up key pressed");
            _rigidbody2D.AddForce(force * Time.deltaTime * transform.up, ForceMode2D.Impulse);
        }

        if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
        {
            Debug.Log("left key pressed");
            _rigidbody2D.AddTorque(turnSpeed * Time.deltaTime);
        }
        if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
        {
            Debug.Log("right key pressed");
            _rigidbody2D.AddTorque(turnSpeed * -1 * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        // collision2D.relativeVelocity.magnitude
        float softLandingVelocityMagnitude = 4f;
        if (collision2D.relativeVelocity.magnitude > softLandingVelocityMagnitude)
        {
            Debug.Log("Landed too hard");
            return;
        }
        Debug.Log("Successful Landing");
        return;
    }
    private void Update()
    {


    }
}
