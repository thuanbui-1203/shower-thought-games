using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
    [SerializeField, Min(0f)] private float extents = 4f, speed = 10f;
    [SerializeField] bool isAI;

    public void Move(float target, float arenaExtents)
    {
        Vector3 p = transform.localPosition;
        p.x = isAI ? AdjustByAI(p.x, target) : AdjustByPlayer(p.x);
        float limit = arenaExtents - extents;
        p.x = Mathf.Clamp(p.x, -limit, limit);
        transform.localPosition = p;
    }

    float AdjustByAI(float x, float target)
    {
        if (x < target)
        {
            return Mathf.Min(x + speed * Time.deltaTime, target);
        }
        return Mathf.Max(x - speed * Time.deltaTime, target);
    }

    float AdjustByPlayer(float x)
    {
        bool goLeft = Keyboard.current.leftArrowKey.isPressed;
        bool goRight = Keyboard.current.rightArrowKey.isPressed;

        if (goLeft && !goRight)
        {
            return x + speed * Time.deltaTime;
        }

        if (!goLeft && goRight)
        {
            return x - speed * Time.deltaTime;
        }
        return x;
    }

    public bool HitBall(float ballX, float ballExtents, out float hitFactor)
    {
        hitFactor = (ballX - transform.localPosition.x) / (extents - ballExtents);
        return -1f <= hitFactor && hitFactor <= 1f;
    }
}
