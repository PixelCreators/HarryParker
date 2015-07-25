using UnityEngine;

[RequireComponent(typeof (Rigidbody2D))]
public class ActorMotor : MonoBehaviour
{
    private const float distanceEpsilon = 0.1f;
    public Transform DEBUGTarget;
    public Animator movementAnimator;
    public float speed = 3;
    private Vector3 target;

    private void Update()
    {
        if (DEBUGTarget != null)
        {
            target = DEBUGTarget.position;
        }
        var toTarget = target - transform.position;
        Debug.Log(toTarget.magnitude);
        var dir = DirectionHelper.VecToDirection(toTarget);
        movementAnimator.SetInteger("Direction", (int) dir);
        var running = toTarget.magnitude > distanceEpsilon;
        Debug.Log(running);
        movementAnimator.SetBool("Running", running);

        var rigidbody = GetComponent<Rigidbody2D>();
        if (running)
        {
            rigidbody.MovePosition(transform.position + toTarget.normalized*speed*Time.deltaTime);
        }
        else
        {
            rigidbody.velocity = Vector2.zero;
        }
    }

    public void MoveTo(Vector3 position)
    {
        target = position;
    }
}