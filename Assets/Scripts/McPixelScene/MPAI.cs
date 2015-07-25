using System.Collections;
using UnityEngine;

public class MPAI : MonoBehaviour
{
    public GameObject KickSprite;
    public GameObject IdleSprite;
    public Transform StoneSprite;
    public GameObject Door;

    public void GetKicked()
    {
        StartCoroutine(KickPlayer());
    }

    private IEnumerator KickPlayer()
    {
        yield return new WaitForSeconds(1);
        IdleSprite.SetActive(false);
        KickSprite.SetActive(true);
        yield return new WaitForSeconds(1);
        IdleSprite.SetActive(true);
        KickSprite.SetActive(false);
    }

    public void DropStone()
    {
        StoneSprite.gameObject.SetActive(true);
    }

    public float DropTime;

    private IEnumerator DropStoneCoroutine()
    {
        Vector3 start = StoneSprite.position;
        float startTime = Time.time;
        float progress = 0;
        while (progress < 1)
        {
            progress = (Time.time - startTime)/DropTime;
            StoneSprite.position = Vector3.Lerp(start, transform.position, progress);
            yield return null;
        }
        StoneSprite.position = transform.position;
        Door.SetActive(false);
    }

    private void Update()
    {
        if (transform.position.x < PlayerPosition.PlayerTransform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
