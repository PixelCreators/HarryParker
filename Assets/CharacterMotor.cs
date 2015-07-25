using UnityEngine;

public enum Direction
{
    Left = 1,
    Right = 2,
    Up = 3,
    Down = 4
}

[RequireComponent(typeof (Rigidbody2D))]
[RequireComponent(typeof (PlayerAttack))]
public class CharacterMotor : MonoBehaviour
{
    private PlayerAttack _attack;
    private Rigidbody2D _rigidbody;

    [HideInInspector]
    public Direction CurrentDirection;

    public Animator MovementAnimator;
    public float Speed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _attack = GetComponent<PlayerAttack>();
    }

    private bool IsHighest(float val, Vector2 vec)
    {
        return val >= vec.x &&
               val >= vec.y &&
               val >= -vec.x &&
               val >= -vec.y;
    }

    private Vector2 lastDirection;

    private void Update()
    {
        var direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (direction.magnitude != 0)
        {
            lastDirection = direction;
        }
        if (!_attack.IsShooting)
        {
            _rigidbody.velocity = direction.normalized*Speed;
        }
        else
        {
            _rigidbody.velocity = Vector2.zero;
        }

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
        MovementAnimator.SetInteger("Direction", (int)CurrentDirection);
        MovementAnimator.SetBool("Running", direction.magnitude != 0 && !_attack.IsShooting);
        MovementAnimator.SetBool("Shooting", _attack.IsShooting);
    }
}