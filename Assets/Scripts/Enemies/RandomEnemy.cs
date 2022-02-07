using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemy : Enemy
{
    public override void CalculateDirection()
    {
        float random = Random.Range(0f, 1f);
        Direction newDir;
        switch (direction)
        {
            case Direction.up:
                if (random <= 0.33f) newDir = Direction.left;
                else if (random <= 0.66f) newDir = Direction.down;
                else newDir = Direction.right;
                break;
            case Direction.down:
                if (random <= 0.33f) newDir = Direction.up;
                else if (random <= 0.66f) newDir = Direction.left;
                else newDir = Direction.right;
                break;
            case Direction.left:
                if (random <= 0.33f) newDir = Direction.right;
                else if (random <= 0.66f) newDir = Direction.up;
                else newDir = Direction.down;
                break;
            case Direction.right:
            default:
                if (random <= 0.33f) newDir = Direction.left;
                else if (random <= 0.66f) newDir = Direction.down;
                else newDir = Direction.up;
                break;
        }
        ChangeDirection(newDir);
    }
}
