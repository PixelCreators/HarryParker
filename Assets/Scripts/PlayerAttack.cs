using UnityEngine;

[RequireComponent(typeof (CharacterMotor))]
public class PlayerAttack : MonoBehaviour
{
    private float _lastShot;
    public bool IsShooting;
    public GameObject Projectile;
    public float ShootInterval;

    private void Update()
    {
        if (_lastShot + ShootInterval > Time.time)
        {
            IsShooting = true;
            return;
        }
        IsShooting = false;

        if (Input.GetKeyDown(KeyCode.M))
        {
            _lastShot = Time.time;
            IsShooting = true;

            Quaternion rotation;
            switch (GetComponent<CharacterMotor>().CurrentDirection)
            {
                case Direction.Down:
                    rotation = Quaternion.FromToRotation(Vector2.right, Vector2.down);
                    break;
                case Direction.Left:
                    rotation = Quaternion.FromToRotation(Vector2.right, Vector2.left);
                    break;
                case Direction.Right:
                    rotation = Quaternion.FromToRotation(Vector2.right, Vector2.right);
                    break;
                case Direction.Up:
                    rotation = Quaternion.FromToRotation(Vector2.right, Vector2.up);
                    break;
                default:
                    rotation = Quaternion.identity;
                    break;
            }

            Instantiate(Projectile, transform.position, rotation);
        }
    }
}