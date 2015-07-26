using System.Collections;
using UnityEngine;

public class MPAI : MonoBehaviour
{
    public GameObject KickSprite;
    public GameObject IdleSprite;
    public GameObject ExplodedSprite;
    public GameObject Hotdog;
    public Transform StoneSprite;
    public Transform StoneTargetPosition;
    public GameObject Door;

    public void GetKicked()
    {
        StartCoroutine(KickPlayer());
    }

    private IEnumerator KickPlayer()
    {
        Player.Kick(transform.position);
        yield return new WaitForSeconds(0.4f);
        IdleSprite.SetActive(false);
        KickSprite.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        IdleSprite.SetActive(true);
        KickSprite.SetActive(false);
        yield return new WaitForSeconds(0.4f);
        DecisionDisplay.TriggerDecision(1);
    }

    public void BurnHotdog()
    {
        StartCoroutine(HotdogCoroutine());
    }

    private IEnumerator HotdogCoroutine()
    {
        var playerHDPivot = PlayerPosition.HotdogPivot;
        var hdTrans = Hotdog.transform;
        var originalPosition = hdTrans.position;
        Hotdog.SetActive(true);
        float time = 0.4f;
        yield return new WaitForSeconds(time);
        hdTrans.position = playerHDPivot.position;
        yield return new WaitForSeconds(time);
        hdTrans.position = originalPosition;
        yield return new WaitForSeconds(time);
        hdTrans.position = playerHDPivot.position;
        yield return new WaitForSeconds(time);
        hdTrans.position = originalPosition;
        yield return new WaitForSeconds(time);
        Hotdog.SetActive(false);
        IdleSprite.SetActive(false);
        ExplodedSprite.SetActive(true);
        FinishLevel();
    }

    public void DropStone()
    {
        StoneSprite.gameObject.SetActive(true);
        StartCoroutine(DropStoneCoroutine());
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
            StoneSprite.position = Vector3.Lerp(start, StoneTargetPosition.position, progress);
            yield return null;
        }
        StoneSprite.position = StoneTargetPosition.position;
        FinishLevel();
    }

    private void FinishLevel()
    {
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
