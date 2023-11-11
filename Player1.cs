using Godot;
using System;
using System.Data;

public class Player1 : KinematicBody2D
{
    private const float SPEED = 300.0f;
    public Vector2 velocity = Vector2.Zero;
    public Vector2 startingPosition;

    public override void _Ready()
    {
        startingPosition = Position;
    }


 public override void _Process(float delta)
 {
    int Ydirection = GetYAxis(KeyList.W, KeyList.S);
        if (Ydirection != 0)
        {
            velocity.y = Ydirection * SPEED;
        }
        else
        {
            if (velocity.y > 0)
            {
                velocity.y -= SPEED;
                if (velocity.y < 0)
                {
                    velocity.y = 0;
                }
            }
            else if (velocity.y < 0)
            {
                velocity.y += SPEED;
                if (velocity.y > 0)
                {
                    velocity.y = 0;
                }
            }
        }
    
    int Xdirection = GetXAxis(KeyList.A, KeyList.D);
        if (Xdirection != 0)
        {
            velocity.x = Xdirection * SPEED;
        }
        else
        {
            if (velocity.x > 0)
            {
                velocity.x -= SPEED;
                if (velocity.y < 0)
                {
                    velocity.x = 0;
                }
            }
            else if (velocity.x < 0)
            {
                velocity.x += SPEED;
                if (velocity.x > 0)
                {
                    velocity.x = 0;
                }
            }
        }

        MoveAndSlide(velocity);
 }

 private int GetYAxis(KeyList positiveKey, KeyList negativeKey)
    {
        if (Input.IsKeyPressed((int)positiveKey)) return -1;
        else if (Input.IsKeyPressed((int)negativeKey)) return 1;
        return 0;
    }

    
private int GetXAxis(KeyList positiveKey, KeyList negativeKey)
    {
        if (Input.IsKeyPressed((int)positiveKey)) return -1;
        else if (Input.IsKeyPressed((int)negativeKey)) return 1;
        return 0;
    }
}




