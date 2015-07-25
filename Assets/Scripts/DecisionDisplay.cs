using System;
using System.Collections.Generic;
using UnityEngine;

public class DecisionDisplay : MonoBehaviour
{
    private static DecisionDisplay _instance;
    public GameObject ButtonPrefab;
    public Transform ButtonPanel;
    private List<GameObject> activeButtons = new List<GameObject>() ;
    public Decision[] Decisions;
    private int _pendingDecision;

    public static event Action DecisionDisplayed;
    public static event Action<int, int> DecisionChosen;

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

    void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    public static void TriggerDecision(int decisionId)
    {
        _instance._pendingDecision = decisionId;
        if (_instance.gameObject.activeInHierarchy)
        {
            return;
        }
        if (DecisionDisplayed != null)
        {
            DecisionDisplayed();
        }
        for (int i = 0; i < _instance.Decisions[decisionId].Options.Length; i++)
        {
            var option = _instance.Decisions[decisionId].Options[i];
            var newButton = Instantiate(_instance.ButtonPrefab);
            newButton.transform.SetParent(_instance.ButtonPanel, false);
            newButton.GetComponent<DecisionButton>().Init(option, i);
            _instance.activeButtons.Add(newButton);
            _instance.gameObject.SetActive(true);
        }
        // TODO: Sendd options to clients
    }

    public static void ChooseDecision(int decision)
    {
        _instance.gameObject.SetActive(false);
        if (DecisionChosen != null)
        {
            DecisionChosen(_instance._pendingDecision, decision);
        }
        foreach (var activeButton in _instance.activeButtons)
        {
            Destroy(activeButton);
        }
    }
}