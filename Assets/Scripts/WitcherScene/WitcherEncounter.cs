using System.Collections;
using UnityEngine;
public class WitcherEncounter : MonoBehaviour
{
    private static WitcherEncounter _instance;
    public Transform YenTransform;
    public Transform TrissTransform;
    public ActorMotor WitcherMotor; 

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

    public static void StartYenEncounter()
    {
        _instance.StartCoroutine(_instance.YenEncounterCoroutine());
    }

    private IEnumerator YenEncounterCoroutine()
    {
        Debug.Log("YenEncounterStarted");
        yield return null;
    }

    private static void StartTrissEncounter()
    {
        _instance.StartCoroutine(_instance.TrissEncounterCoroutine());
    }

    private IEnumerator TrissEncounterCoroutine()
    {
        Debug.Log("TrissEncounterStarted");
        yield return null;
    }
}
