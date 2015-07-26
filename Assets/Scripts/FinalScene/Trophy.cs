using System.Collections;
using UnityEngine;
public class Trophy : MonoBehaviour
{
    public Transform Hulk;
    public GameObject ChargingHulk;
    public GameObject SmashingHulk;
    public const float RushTime = 1f;
    public AudioClip HulkSmash;
    public AudioClip HulkMusic;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Hulk.gameObject.SetActive(true);
            StartCoroutine(HulkCoroutine());
        }
    }

    IEnumerator HulkCoroutine()
    {
        AudioSource.PlayClipAtPoint(HulkMusic, Vector3.zero);
        MyAudio.PlayBackgroundMusic(HulkMusic);
        float progress = 0;
        float start = Time.time;
        Vector3 startPosition = Hulk.position;

        while (progress < 1)
        {
            Debug.Log(progress);
            progress = (Time.time - start)/RushTime;
            Hulk.position = Vector3.Lerp(startPosition, PlayerPosition.PlayerTransform.position, progress);
            yield return null;
        }
        
        ChargingHulk.SetActive(false);
        SmashingHulk.SetActive(true);
        Player.Kill();
    }
}
