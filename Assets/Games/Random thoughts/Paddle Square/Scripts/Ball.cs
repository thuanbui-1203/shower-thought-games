using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField, Min(0f)] 
    private float startXSpeed = 8f, constantYSpeed = 8f, extents = .5f, maxXSpeed = 20f;
    private Vector2 position, velocity;
    public float Extents => extents;
    public Vector2 Velocity => velocity;
    public Vector2 Position => position;
    public Vector3 UpdateVisualization() => transform.localPosition = new Vector3(position.x, 0f, position.y);

    public void BallMove() => position += velocity * Time.deltaTime;
    public void StartNewGame()
    {
        transform.localPosition = Vector3.zero;
        UpdateVisualization();
        velocity = new Vector2(startXSpeed, constantYSpeed);
    }

    public void BounceX(float boundary)
    {
        position.x = 2 * boundary - position.x;
        velocity.x = -velocity.x;
    }
    public void BounceY(float boundary)
    {
        position.y = 2 * boundary - position.y;
        velocity.y = -velocity.y;
    }

    public void SetXPositionSpeed(float start, float speedFactor, float deltaTime) 
    {
        velocity.x = maxXSpeed * speedFactor;
        position.x = start + velocity.x * deltaTime;
    }
}
