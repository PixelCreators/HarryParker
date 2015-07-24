using UnityEngine;

[RequireComponent(typeof (Rigidbody2D))]
public class CharacterMotor : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public float Speed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _rigidbody.velocity = direction.normalized*Speed;
    }
}