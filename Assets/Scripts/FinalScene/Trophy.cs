using System.Collections;
using UnityEngine;
public class Trophy : MonoBehaviour
{
    public Transform Hulk;
    public GameObject ChargingHulk;
    public GameObject SmashingHulk;
    public float RushTime = 2.5f;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Hulk.gameObject.SetActive(true);
        }
    }

    IEnumerator HulkCoroutine()
    {
        float progress = 0;
        float start = Time.time;
        Vector3 startPosition = Hulk.position;

        while (progress < 1)
        {
            progress = (start - Time.time)/RushTime;
            Hulk.position = Vector3.Lerp(startPosition, PlayerPosition.PlayerTransform.position, progress);
            yield return null;
        }
        
        ChargingHulk.SetActive(false);
        SmashingHulk.SetActive(true);
        Player.Kill();
    }
}
