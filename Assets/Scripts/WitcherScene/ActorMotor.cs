using UnityEngine;

[RequireComponent(typeof (Rigidbody2D))]
public class ActorMotor : MonoBehaviour
{
    private const float distanceEpsilon = 0.1f;
    public Transform DEBUGTarget;
    public Animator movementAnimator;
    public float speed = 3;
    private Vector3 target;
    private bool targetSet = false;
    public bool Lusty;
    public GameObject FuckedScreen;

    private void Update()
    {
        if (DEBUGTarget != null)
        {
            target = DEBUGTarget.position;
        }
        if (!targetSet)
        {
            return;
        }

        var toTarget = target - transform.position;
        var dir = DirectionHelper.VecToDirection(toTarget);
        movementAnimator.SetInteger("Direction", (int) dir);
        var running = toTarget.magnitude > distanceEpsilon;
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            FuckedScreen.SetActive(true);
        }
    }

    public void MoveTo(Vector3 position)
    {
        targetSet = true;
        target = position;
    }
}