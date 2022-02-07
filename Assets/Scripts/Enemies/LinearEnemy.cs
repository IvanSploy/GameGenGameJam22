using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este tipo de enemigo rebota contra las paredes ya sea vertical u horizontalmente.
public class LinearEnemy : Enemy
{
    public override void CalculateDirection()
    {
        Direction newDir;
        switch (direction)
        {
            case Direction.up:
                newDir = Direction.down;
                break;
            case Direction.down:
                newDir = Direction.up;
                break;
            case Direction.left:
                newDir = Direction.right;
                break;
            case Direction.right:
            default:
                newDir = Direction.left;
                break;
        }
        ChangeDirection(newDir);
    }
}
