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

    public bool Dead;
    private Vector2 lastDirection;
    public Animator MovementAnimator;
    public float Speed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _attack = GetComponent<PlayerAttack>();
    }

    public void Kick(Vector3 position)
    {
        if (position.x > transform.position.x)
        {
            MovementAnimator.SetTrigger("KickL");
        }
        else
        {
            MovementAnimator.SetTrigger("KickR");
        }
    }

    private void Update()
    {
        if (Dead)
        {
            MovementAnimator.SetBool("Dead", true);
            MovementAnimator.SetInteger("Direction", (int) Direction.Down);
            MovementAnimator.SetBool("Running", false);
            MovementAnimator.SetBool("Shooting", false);
            return;
        }
        var direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (direction.magnitude != 0)
        {
            lastDirection = direction;
        }
        _rigidbody.velocity = direction.normalized*Speed*TimeManager.TimeMultiplier;
        CurrentDirection = DirectionHelper.VecToDirection(lastDirection);
        MovementAnimator.SetInteger("Direction", (int) CurrentDirection);
        MovementAnimator.SetBool("Running",
            direction.magnitude != 0 && !_attack.IsShooting && TimeManager.TimeMultiplier != 0);
        MovementAnimator.SetBool("Shooting", _attack.IsShooting && TimeManager.TimeMultiplier != 0);
    }
}