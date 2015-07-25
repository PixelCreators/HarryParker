using System.Collections;
using UnityEngine;

public class RushAt : MonoBehaviour
{
    public AnimationCurve SpeedUpCurve;
    public bool IsRushing;

    public void Rush(Vector3 position)
    {
        if (IsRushing)
        {
            return;
        }
        StartCoroutine(RushCoroutine(position));
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.H))
        {
            Rush(PlayerPosition.PlayerTransform.position);
        }
    }

    [HideInInspector]
    public bool KillOnContact = false;

    private void OnCollisionStay2D(Collision2D col)
    {
        if (IsRushing && KillOnContact && col.gameObject.CompareTag(Constants.Tags.Player))
        {
            Player.Kill();
            KillOnContact = false;
        }

        if (col.gameObject.CompareTag(Constants.Tags.Player))
        {
            Debug.Log(IsRushing);
        }

        if (col.gameObject.layer == Constants.Layers.Walls)
        {
            WallContact = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.layer == Constants.Layers.Walls)
        {
            WallContact = false;
        }
    }

    public bool WallContact = false;
    public float RushSpeed = 5;
    public float MaxRushTime = 2f;
    public float SpeedUpTime = 0.3f;
    public float ColliderSize = 0.91f;

    private IEnumerator RushCoroutine(Vector3 position)
    {
        var direction = (position - transform.position).normalized;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, Constants.LayerMask.Walls);
        var target = hitInfo.point;
        var startTime = Time.fixedTime;
        float speedUpProgress = 0;
        IsRushing = true;

        while (speedUpProgress < 1)
        {
            yield return new WaitForFixedUpdate();
            speedUpProgress = (Time.fixedTime - startTime)/SpeedUpTime;
            float speed = SpeedUpCurve.Evaluate(speedUpProgress);
            GetComponent<Rigidbody2D>().MovePosition(Vector3.MoveTowards(transform.position, target, speed*Time.fixedDeltaTime));
        }

        while (((Vector2)transform.position - target).magnitude > ColliderSize && !WallContact)
        {
            yield return null;
            GetComponent<Rigidbody2D>().MovePosition(Vector3.MoveTowards(transform.position, target, TimeManager.TimeMultiplier*RushSpeed*Time.fixedDeltaTime));
        }
        IsRushing = false;
    }
}
