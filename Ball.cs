using Godot;
using System;

public class Ball : KinematicBody2D
{
    public float ballSpeed = 300.0f;
    public Vector2 direction = Vector2.Zero;
    public Vector2 velocity = Vector2.Zero;
    public Vector2 ballStart;

    public override void _Ready()
    {
        ballStart = Position;
        direction.y = (new Random().Next(0, 2) == 0) ? -1 : 1;
        direction.x = (new Random().Next(0, 2) == 0) ? -1 : 1;
    }

    public override void _PhysicsProcess(float delta)
    {
        if (direction != Vector2.Zero)
        {
            velocity.y = direction.y * ballSpeed;
            velocity.x = direction.x * ballSpeed;
        }
        else
        {
            if (velocity.y > 0)
            {
                velocity.y -= ballSpeed * delta;
                if (velocity.y < 0)
                {
                    velocity.y = 0;
                }
            }
            else if (velocity.y < 0)
            {
                velocity.y += ballSpeed * delta;
                if (velocity.y > 0)
                {
                    velocity.y = 0;
                }
            }
        }

        MoveAndSlide(velocity);
    }
}
