using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour 
{
    public float Speed;

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().MovePosition(transform.position + transform.right*Speed*Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        var other = col.gameObject;

        if (other.layer == Constants.Layers.Enemy)
        {
            other.GetComponent<Enemy>().ApplyDamage();
        }

        Destroy(gameObject);
    }
}
