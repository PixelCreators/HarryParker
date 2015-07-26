using System.Collections;
using UnityEngine;

public class WitcherEncounter : MonoBehaviour
{
    private static WitcherEncounter _instance;
    public Witch Triss;
    public ActorMotor WitcherMotor;
    public Witch Yen;
    public AudioClip WitcherFightMusic;

    public GameObject Door;

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

    public static void Finish()
    {
        _instance.Door.SetActive(false);
    }

    public void StartYenEncounter()
    {
        StartCoroutine(YenEncounterCoroutine());
    }

    private IEnumerator YenEncounterCoroutine()
    {
        return StartWitchEncounter(Yen, Triss);
    }

    public void StartTrissEncounter()
    {
        StartCoroutine(TrissEncounterCoroutine());
    }

    private IEnumerator TrissEncounterCoroutine()
    {
        return StartWitchEncounter(Triss, Yen);
    }

    private IEnumerator StartWitchEncounter(Witch attacking, Witch leaving)
    {
        MyAudio.PlayBackgroundMusic(WitcherFightMusic);
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
        MyAudio.PlayBackgroundMusic(WitcherFightMusic);
        WitcherMotor.speed = 4.5f;
        WitcherMotor.DEBUGTarget = PlayerPosition.PlayerTransform;
        WitcherMotor.MoveTo(Vector3.zero);
    }
}