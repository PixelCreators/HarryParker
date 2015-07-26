using System.Collections;
using UnityEngine;
public class WitcherEncounter : MonoBehaviour
{
    private static WitcherEncounter _instance;
    public Witch YenTransform;
    public Witch TrissTransform;
    public ActorMotor WitcherMotor;
    public GameObject Portal;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    public void StartYenEncounter()
    {
        StartCoroutine(_instance.YenEncounterCoroutine());
    }

    private IEnumerator YenEncounterCoroutine()
    {
        Debug.Log("YenEncounterStarted");
        yield return null;
    }

    public void StartTrissEncounter()
    {
        StartCoroutine(_instance.TrissEncounterCoroutine());
    }

    private IEnumerator TrissEncounterCoroutine()
    {
        Debug.Log("TrissEncounterStarted");
        yield return null;
    }

    private IEnumerator StartWitchEncounter(Witch attacking, Witch leaving)
    {
        var leavingTransform = leaving.transform;
        var witcherTransform = WitcherMotor.transform;
        WitcherMotor.MoveTo(leaving.transform.position);
        while ((leavingTransform.position - witcherTransform.position).magnitude < 1)
        {
            yield return null;
        }

    }

    public void StartWitcherEncounter()
    {
        StartCoroutine(WitcherEncounterCoroutine());
    }

    private IEnumerator WitcherEncounterCoroutine()
    {
        Debug.Log("WitcherEncounterStarted");
        yield return null;
    }
}
