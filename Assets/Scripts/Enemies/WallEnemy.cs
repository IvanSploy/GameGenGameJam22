using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEnemy : Enemy
{
    public bool clockWise = true;
    public override void CalculateDirection()
    {
        Direction newDir;
        switch (direction)
        {
            case Direction.up:
               if(clockWise) newDir = Direction.right;
               else newDir = Direction.left;
                break;
            case Direction.down:
                if (clockWise) newDir = Direction.left;
                else newDir = Direction.right;
                break;
            case Direction.left:
                if (clockWise) newDir = Direction.up;
                else newDir = Direction.down;
                break;
            case Direction.right:
            default:
                if (clockWise) newDir = Direction.down;
                else newDir = Direction.up;
                break;
        }
        ChangeDirection(newDir);
    }
}
