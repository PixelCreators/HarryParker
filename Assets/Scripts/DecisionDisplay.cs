using System;
using System.Collections.Generic;
using UnityEngine;

public class DecisionDisplay : MonoBehaviour
{
    private static DecisionDisplay _instance;
    public GameObject ButtonPrefab;
    public Transform ButtonPanel;
    private List<GameObject> activeButtons = new List<GameObject>() ;

    public static event Action DecisionDisplayed;
    public static event Action<string> DecisionChosen;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void TriggerDecision(Decision decision)
    {
        if (_instance.gameObject.activeInHierarchy)
        {
            return;
        }
        if (DecisionDisplayed != null)
        {
            DecisionDisplayed();
        }
        foreach (var option in decision.Options)
        {
            var newButton = Instantiate(_instance.ButtonPrefab);
            newButton.transform.SetParent(_instance.ButtonPanel, false);
            newButton.GetComponent<DecisionButton>().Init(option);
            _instance.gameObject.SetActive(true);
        }
        // TODO: Sendd options to clients
    }

    public static void ChooseDecision(string decision)
    {
        _instance.gameObject.SetActive(false);
        if (DecisionChosen != null)
        {
            DecisionChosen(decision);
        }
    }
}