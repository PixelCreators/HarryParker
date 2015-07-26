using System.Collections;
using UnityEngine;

[RequireComponent(typeof (Enemy))]
public class Witch : MonoBehaviour
{
    private bool _fighting;
    private bool _hit;
    private float _lastCast;
    public Transform BlinkSprite;
    public float CastDelay = 3f;
    public GameObject CastingSprite;
    public float CastTime;
    public GameObject NormalSprite;
    public GameObject Portal;
    public Transform PosMax;
    public Transform PosMin;
    public GameObject SpellDownPrefab;
    public GameObject SpellUpPrefab;
    public int CastAtOnce = 3;

    public void StartFight()
    {
        _fighting = true;
        GetComponent<Enemy>().DamageApplied += OnHit;
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

    private IEnumerator FightCoroutine()
    {
        Debug.Log(name);
        while (_fighting)
        {
            if (_hit)
            {
                _hit = false;
                yield return StartCoroutine(BlinkAway());
            }
            if ((_lastCast + CastDelay) < Time.time)
            {
                yield return StartCoroutine(Cast());
            }

            yield return null;
        }
        Portal.transform.position = transform.position;
        Portal.SetActive(true);
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        WitcherEncounter.Finish();
    }

    private IEnumerator Cast()
    {
        for (int i = 0; i < CastAtOnce; i++)
        {
            CastSpell();
        }
        NormalSprite.SetActive(false);
        CastingSprite.SetActive(true);
        yield return new WaitForSeconds(CastTime);
        NormalSprite.SetActive(true);
        CastingSprite.SetActive(false);
    }

    private void CastSpell()
    {
        var pos = PlayerPosition.PlayerTransform.position;
        var castUp = Random.value > 0.5f;
        Vector3 max = PosMax.position;
        Vector3 min = PosMin.position;
        if (castUp)
        {
            max.y = ( max.y + min.y) / 2;
        }
        else
        {
            min.y = ( max.y +  min.y) / 2;
        }
        while (PlayerPosition.DistanceToPlayer(pos) < 4)
        {
            pos = new Vector3(Random.Range(min.x, max.x),
                Random.Range(min.y, max.y));
        }
        Instantiate(castUp ? SpellUpPrefab : SpellDownPrefab, pos, Quaternion.identity);
    }

    private IEnumerator BlinkAway()
    {
        var pos = new Vector3(Random.Range(PosMin.position.x, PosMax.position.x),
            Random.Range(PosMin.position.y, PosMax.position.y));
        float scale = 0;
        BlinkSprite.gameObject.SetActive(true);
        while (scale < 1)
        {
            BlinkSprite.transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
            scale += Time.deltaTime*5;
        }

        transform.position = pos;
        while (scale > 0)
        {
            BlinkSprite.transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
            scale -= Time.deltaTime*5;
        }
        BlinkSprite.gameObject.SetActive(false);
    }
}