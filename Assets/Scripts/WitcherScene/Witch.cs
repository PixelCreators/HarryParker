using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class Witch : MonoBehaviour
{
    public GameObject Portal;
    public Transform PosMin;
    public Transform PosMax;

    public Transform BlinkSprite;
    public GameObject SpellUpPrefab;
    public GameObject SpellDownPrefab;

    public void StartFight()
    {
        _fighting = true;
        StartCoroutine(FightCoroutine());
    }

    private void OnHit(int hpLeft)
    {
        if (hpLeft > 0)
        {
            _hit = true;
        }
        else
        {
            _fighting = false;
        }
    }

    public void OpenPortal()
    {
        Portal.SetActive(true);
    }

    private bool _fighting;
    public float CastTime;
    private bool _hit;
    public float CastDelay = 3f;
    private float _lastCast;

    private IEnumerator FightCoroutine()
    {
        while (_fighting)
        {
            if (_hit)
            {
                _hit = false;
                yield return StartCoroutine(BlinkAway());
            }
            if (!((_lastCast + CastDelay) < Time.time))
            {
                yield return StartCoroutine(Cast());
            }

            yield return null;
        }
    }

    private IEnumerator Cast()
    {
        Vector3 pos = PlayerPosition.PlayerTransform.position;
        while (PlayerPosition.DistanceToPlayer(pos) < 4)
        {
            pos = new Vector3(Random.Range(PosMin.position.x, PosMax.position.x),
                Random.Range(PosMin.position.y, PosMax.position.y));
        }
        if ((pos.y - PosMin.position.y)/(PosMax.position.y - PosMin.position.x) < 0.5f)
        {
            Instantiate(SpellUpPrefab, pos, Quaternion.identity);
        }
        else
        {
            Instantiate(SpellDownPrefab, pos, Quaternion.identity);
        }
        yield return new WaitForSeconds(CastTime);
    }

    private IEnumerator BlinkAway()
    {
        Vector3 pos = new Vector3(Random.Range(PosMin.position.x, PosMax.position.x), Random.Range(PosMin.position.y, PosMax.position.y));
        float scale = 0;
        BlinkSprite.gameObject.SetActive(true);
        while (scale < 1)
        {
            BlinkSprite.transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
            scale += Time.deltaTime;
        }

        transform.position = pos;
        while (scale > 0)
        {
            BlinkSprite.transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
            scale -= Time.deltaTime;
        }
        BlinkSprite.gameObject.SetActive(false);
    }
}
