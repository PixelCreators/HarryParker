using UnityEngine;

public enum Direction
{
    Left,
    Right,
    Up,
    Down
}

[RequireComponent(typeof (Rigidbody2D))]
public class CharacterMotor : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public float Speed;
    public Direction CurrentDirection;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private bool IsHighest(float val, Vector2 vec)
    {
        return val >= vec.x &&
               val >= vec.y &&
               val >= -vec.x &&
               val >= -vec.y;
    }

    private void Update()
    {
        var direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _rigidbody.velocity = direction.normalized*Speed;
        if (IsHighest(direction.x, direction))
        {
            CurrentDirection = Direction.Right;
        }
        else if (IsHighest(direction.y, direction))
        {
            CurrentDirection = Direction.Up;
        }
        else if (IsHighest(-direction.x, direction))
        {
            CurrentDirection = Direction.Left;
        }
        else if (IsHighest(-direction.y, direction))
        {
            CurrentDirection = Direction.Down;
        }
        
    }
}