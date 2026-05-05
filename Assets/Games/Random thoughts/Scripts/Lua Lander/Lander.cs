using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Lander : MonoBehaviour
{
    public event EventHandler OnUpForce;
    public event EventHandler OnLeftForce;
    public event EventHandler OnRightForce;
    public event EventHandler OnBeforeForce;
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
        OnBeforeForce?.Invoke(this, EventArgs.Empty);
        if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed)
        {
            _rigidbody2D.AddForce(force * Time.deltaTime * transform.up, ForceMode2D.Impulse);
            OnUpForce?.Invoke(this, EventArgs.Empty);
        }

        if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
        {
            _rigidbody2D.AddTorque(turnSpeed * Time.deltaTime);
            OnLeftForce?.Invoke(this, EventArgs.Empty);
        }
        if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
        {
            _rigidbody2D.AddTorque(turnSpeed * -1 * Time.deltaTime);
            OnRightForce?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.TryGetComponent(out LandingPad landingPad))
        {
            // collision2D.relativeVelocity.magnitude
            float softLandingVelocityMagnitude = 4f;
            float relativeVelocityMagnitude = collision2D.relativeVelocity.magnitude;
            if (relativeVelocityMagnitude > softLandingVelocityMagnitude)
            {
                Debug.Log("Landed too hard");
                return;
            }

            float dotVector = Vector2.Dot(Vector2.up, transform.up);
            float minDotVector = 0.90f;
            if (dotVector < minDotVector)
            {
                Debug.Log("Landed on a too steep angle");
                return;
            }

            float maxScoreAmountLandingAngle = 100;
            float scoreDotVectorMultiplier = 10f;
            float landingAngleScore = maxScoreAmountLandingAngle - Mathf.Abs(dotVector - 1) * scoreDotVectorMultiplier * maxScoreAmountLandingAngle;

            float maxScoreAmountLandingSpeed = 100;
            float landingSpeedScore = (softLandingVelocityMagnitude - relativeVelocityMagnitude) * maxScoreAmountLandingSpeed;

            Debug.Log($"Landing angle score: {landingAngleScore}");
            Debug.Log($"Landing speed score: {landingSpeedScore}");

            int score = Mathf.RoundToInt((landingAngleScore + landingSpeedScore) * landingPad.GetScoreMultiplier);

            Debug.Log($"Total score: {score}");
        }
    }
    private void Update()
    {


    }
}
