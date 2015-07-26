using UnityEngine;

public class FlameWall : MonoBehaviour
{
    private float _spawned;
    public float Duration;

    private void OnEnable()
    {
        _spawned = Time.time;
    }

    private void Update()
    {
        if (_spawned + Duration < Time.time)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Player.Kill();
        }
    }
}