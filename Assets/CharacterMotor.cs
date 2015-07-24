using UnityEngine;

[RequireComponent(typeof (Rigidbody2D))]
public class CharacterMotor : MonoBehaviour
{
    public float Speed;
    private Rigidbody2D _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _rigidbody.velocity = direction*Speed;
    }
}