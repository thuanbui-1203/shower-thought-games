using System;
using NUnit.Framework.Constraints;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Ball ball;
    [SerializeField] private Paddle topPaddle, bottomPaddle;

    [SerializeField, Min(0f)] Vector2 arenaExtents = new(10, 10);
    

    private void Awake()
    {
        ball.StartNewGame();
    }

    private void Update()
    {
        topPaddle.Move(ball.Position.x, arenaExtents.x);
        bottomPaddle.Move(ball.Position.x, arenaExtents.x);
        ball.BallMove();
        BounceYIfNeeded();
        BounceXIfNeeded(ball.Position.x);
        ball.UpdateVisualization();
    }

    private void BounceYIfNeeded()
    {
        float yExtents = arenaExtents.y - ball.Extents;
        if (ball.Position.y < -yExtents)
        {
            BounceY(-yExtents, bottomPaddle);
        }
        else if (ball.Position.y > yExtents)
        {
            BounceY(yExtents, topPaddle);
        }
    }

    private void BounceXIfNeeded(float x)
    {
        float xExtents = arenaExtents.x - ball.Extents;
        if (x < -xExtents)
        {
            ball.BounceX(-xExtents);
        }
        else if (x > xExtents)
        {
            ball.BounceY(xExtents);
        }
    }

    private void BounceY(float boundary, Paddle defender)
    {
        float durationAfterBounce = (ball.Position.y - boundary) / ball.Velocity.y;
        float bounceX = ball.Position.x - ball.Velocity.x * durationAfterBounce;

        BounceXIfNeeded(bounceX);
        bounceX = ball.Position.x - ball.Velocity.x * durationAfterBounce;
        ball.BounceY(boundary);

        if (defender.HitBall(bounceX, ball.Extents, out float hitFactor))
        {
            ball.SetXPositionSpeed(bounceX, hitFactor, durationAfterBounce);
        }
    }
}
