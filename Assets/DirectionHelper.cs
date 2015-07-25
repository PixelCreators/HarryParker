using UnityEngine;

public class DirectionHelper
{
    public static Direction VecToDirection(Vector2 ldir)
    {
        Direction curr;
        if (IsHighest(ldir.x, ldir))
        {
            curr = Direction.Right;
        }
        else if (IsHighest(ldir.y, ldir))
        {
            curr = Direction.Up;
        }
        else if (IsHighest(-ldir.x, ldir))
        {
            curr = Direction.Left;
        }
        else if (IsHighest(-ldir.y, ldir))
        {
            curr = Direction.Down;
        }
        else
        {
            curr = Direction.Down;
        }
        return curr;
    }

    private static bool IsHighest(float val, Vector2 vec)
    {
        return val >= vec.x &&
               val >= vec.y &&
               val >= -vec.x &&
               val >= -vec.y;
    }
}