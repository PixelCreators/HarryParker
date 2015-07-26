using System.Collections;
using UnityEngine;

public class WitcherEncounter : MonoBehaviour
{
    private static WitcherEncounter _instance;
    public GameObject Portal;
    public Witch Triss;
    public ActorMotor WitcherMotor;
    public Witch Yen;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    private void OnDestroy()
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
        return StartWitchEncounter(Yen, Triss);
    }

    public void StartTrissEncounter()
    {
        StartCoroutine(_instance.TrissEncounterCoroutine());
    }

    private IEnumerator TrissEncounterCoroutine()
    {
        return StartWitchEncounter(Triss, Yen);
    }

    private IEnumerator StartWitchEncounter(Witch attacking, Witch leaving)
    {
        var leavingTransform = leaving.transform;
        var witcherTransform = WitcherMotor.transform;
        WitcherMotor.MoveTo(leaving.transform.position);
        while ((leavingTransform.position - witcherTransform.position).magnitude > 1)
        {
            yield return null;
        }

        leaving.OpenPortal();

        while (Vector3.Distance(leavingTransform.position, witcherTransform.position) > 0.2f)
        {
            yield return null;
        }
        leaving.gameObject.SetActive(false);
        WitcherMotor.gameObject.SetActive(false);
        attacking.StartFight();
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