using UnityEngine;

[RequireComponent(typeof (CharacterMotor))]
public class PlayerAttack : MonoBehaviour
{
    public GameObject Projectile;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
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