using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Lander : MonoBehaviour
{
    public static Lander Instance { get; private set; }
    public event EventHandler OnUpForce;
    public event EventHandler OnLeftForce;
    public event EventHandler OnRightForce;
    public event EventHandler OnBeforeForce;
    public event EventHandler OnCoinPickup;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public event EventHandler<OnLandedEventArgs> OnLanded;
    public enum LandingType
    {
        Success,
        WrongLandingArea,
        TooSteepAngle,
        TooFastLanding
    }
    public enum State
    {
        WaitingToStart,
        Normal,
        GameOver
    }
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }
    public class OnLandedEventArgs : EventArgs
    {
        public int score;
        public LandingType landingType;
        public float dotVector;
        public float landingSpeed;
        public float scoreMultiplier;
    }

    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float turnSpeed = 100f;
    [SerializeField] private float force = 70f;
    const float GRAVITY_NORMAL = 0.7f;
    private State state;
    private float fuelAmount = 10f;
    private float maxFuelAmount = 10f;
    public float GetMaxFuelAmount => fuelAmount / maxFuelAmount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Instance = this;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.gravityScale = 0f;
        state = State.WaitingToStart;
    }
    private void Start()
    {

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        OnBeforeForce?.Invoke(this, EventArgs.Empty);
        switch (state)
        {
            default:
            case State.WaitingToStart:

                if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed || Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed || Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
                {
                    ConsumeFuel();
                    _rigidbody2D.gravityScale = GRAVITY_NORMAL;
                    SetState(State.Normal);
                }
                break;
            case State.Normal:
                if (fuelAmount <= 0f)
                {
                    return;
                }
                if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed || Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed || Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
                {
                    ConsumeFuel();
                }
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
                break;
            case State.GameOver:
                break;
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
                OnLanded?.Invoke(this, new OnLandedEventArgs
                {
                    score = 0,
                    dotVector = 0,
                    landingSpeed = relativeVelocityMagnitude,
                    scoreMultiplier = landingPad.GetScoreMultiplier,
                    landingType = LandingType.TooFastLanding
                });
                SetState(State.GameOver);
                return;
            }

            float dotVector = Vector2.Dot(Vector2.up, transform.up);
            float minDotVector = 0.90f;
            if (dotVector < minDotVector)
            {
                Debug.Log("Landed on a too steep angle");
                OnLanded?.Invoke(this, new OnLandedEventArgs
                {
                    score = 0,
                    dotVector = dotVector,
                    landingSpeed = relativeVelocityMagnitude,
                    scoreMultiplier = landingPad.GetScoreMultiplier,
                    landingType = LandingType.TooSteepAngle
                });
                SetState(State.GameOver);
                return;
            }

            float maxScoreAmountLandingAngle = 100;
            float scoreDotVectorMultiplier = 10f;
            float landingAngleScore = maxScoreAmountLandingAngle - Mathf.Abs(dotVector - 1) * scoreDotVectorMultiplier * maxScoreAmountLandingAngle;

            float maxScoreAmountLandingSpeed = 100;
            float landingSpeed = softLandingVelocityMagnitude - relativeVelocityMagnitude;
            float landingSpeedScore = landingSpeed * maxScoreAmountLandingSpeed;

            Debug.Log($"Landing angle score: {landingAngleScore}");
            Debug.Log($"Landing speed score: {landingSpeedScore}");

            int score = Mathf.RoundToInt((landingAngleScore + landingSpeedScore) * landingPad.GetScoreMultiplier);

            Debug.Log($"Total score: {score}");
            OnLanded?.Invoke(this, new OnLandedEventArgs
            {
                score = score,
                dotVector = dotVector,
                landingSpeed = relativeVelocityMagnitude,
                scoreMultiplier = landingPad.GetScoreMultiplier,
                landingType = LandingType.Success
            });
            SetState(State.GameOver);
        }
        else
        {
            Debug.Log("Landed on wrong area");
            OnLanded?.Invoke(this, new OnLandedEventArgs
            {
                score = 0,
                dotVector = 0f,
                landingSpeed = 0f,
                scoreMultiplier = 0,
                landingType = LandingType.WrongLandingArea
            });
            SetState(State.GameOver);
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.TryGetComponent(out FuelPickup fuelPickup))
        {
            float addFuelAmount = 10f;
            fuelAmount += addFuelAmount;
            if (fuelAmount >= maxFuelAmount)
            {
                fuelAmount = 10f;
            }
            fuelPickup.DestroySelf();
        }

        if (collider2D.gameObject.TryGetComponent(out CoinPickup coinPickup))
        {
            OnCoinPickup?.Invoke(this, EventArgs.Empty);
            coinPickup.DestroySelf();
        }
        return;
    }

    private void SetState(State state)
    {
        this.state = state;
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
        {
            state = this.state
        });
    }

    private void ConsumeFuel()
    {
        float fuelConsumptionAmount = 1f;
        fuelAmount -= fuelConsumptionAmount * Time.deltaTime;
    }
    public float GetFuelAmount()
    {
        return fuelAmount;
    }
    public float GetSpeedX()
    {
        return Mathf.Abs(_rigidbody2D.linearVelocityX);
    }

    public float GetSpeedY()
    {
        return Mathf.Abs(_rigidbody2D.linearVelocityY);
    }
}
