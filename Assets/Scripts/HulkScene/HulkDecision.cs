﻿using System.Collections;
using UnityEngine;
public class HulkDecision : MonoBehaviour 
{
    public int decisionId;

    public GameObject HulkChaseTrigger;
    public GameObject HulkKickTrigger;
    public GameObject HulkHugTrigger;
    public GameObject StealHulksIceCreamTrigger;

    void Awake()
    {
        DecisionDisplay.DecisionChosen += OnDecisionChosen;
    }

    void OnDestroy()
    {
        DecisionDisplay.DecisionChosen -= OnDecisionChosen;
    }

    void OnDecisionChosen(int decisionId, int decision)
    {
        if (this.decisionId != decisionId)
        {
            return;
        }
        StartCoroutine( StartEnc(decision));
    }

    private IEnumerator StartEnc(int decision)
    {
        yield return new WaitForSeconds(2);
        switch (decision)
        {
            case 0: // Avoid Hulk
                HulkChaseTrigger.SetActive(true);
                break;
            case 1: // Kick Hulk
                HulkKickTrigger.SetActive(true);
                break;
            case 2: // Hug Hulk
                HulkHugTrigger.SetActive(true);
                break;
            case 3: // Steal ice cream
                StealHulksIceCreamTrigger.SetActive(true);
                break;
        }
    }
}
