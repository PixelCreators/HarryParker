using System.Collections;
using Constants;
using UnityEngine;
using LayerMask = Constants.LayerMask;

public class RushAt : MonoBehaviour
{
    public float ColliderSize = 0.91f;

    [HideInInspector]
    public bool IsRushing;

    [HideInInspector]
    public bool KillOnContact = false;

    public float MaxRushTime = 2f;
    public Animator RushAnimator;
    public float RushSpeed = 5;
    public AnimationCurve SpeedUpCurve;
    public float SpeedUpTime = 0.3f;
    public bool WallContact = false;

    public void Rush(Vector3 position)
    {
        if (IsRushing)
        {
            return;
        }
        StartCoroutine(RushCoroutine(position));
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.H))
        {
            Rush(PlayerPosition.PlayerTransform.position);
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (IsRushing && KillOnContact && col.gameObject.CompareTag(Tags.Player))
        {
            Player.Kill();
            KillOnContact = false;
        }

        if (col.gameObject.layer == Layers.Walls)
        {
            WallContact = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.layer == Layers.Walls)
        {
            WallContact = false;
        }
    }

    private IEnumerator RushCoroutine(Vector3 position)
    {
        var direction = (position - transform.position).normalized;
        var hitInfo = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, LayerMask.Walls);
        var target = hitInfo.point;
        var startTime = Time.fixedTime;
        float speedUpProgress = 0;
        IsRushing = true;

        RushAnimator.SetInteger("Direction", (int) DirectionHelper.VecToDirection(direction));
        RushAnimator.SetBool("Running", true);
        RushAnimator.SetBool("Shooting", false);

        while (speedUpProgress < 1)
        {
            yield return new WaitForFixedUpdate();
            speedUpProgress = (Time.fixedTime - startTime)/SpeedUpTime;
            var speed = SpeedUpCurve.Evaluate(speedUpProgress);
            GetComponent<Rigidbody2D>()
                .MovePosition(Vector3.MoveTowards(transform.position, target, speed*Time.fixedDeltaTime));
        }

        while (((Vector2) transform.position - target).magnitude > ColliderSize && !WallContact)
        {
            yield return null;
            GetComponent<Rigidbody2D>()
                .MovePosition(Vector3.MoveTowards(transform.position, target,
                    TimeManager.TimeMultiplier*RushSpeed*Time.fixedDeltaTime));
        }
        IsRushing = false;

        RushAnimator.SetBool("Running", false);
    }
}