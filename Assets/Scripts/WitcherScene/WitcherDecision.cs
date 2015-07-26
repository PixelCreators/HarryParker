using UnityEngine;

public class WitcherDecision : MonoBehaviour
{
    public int decisionId = 2;

    private void Awake()
    {
        DecisionDisplay.DecisionChosen += OnDecisionChosen;
    }

    private void OnDestroy()
    {
        DecisionDisplay.DecisionChosen -= OnDecisionChosen;
    }

    private void OnDecisionChosen(int decisionId, int option)
    {
        if (decisionId != this.decisionId)
        {
            return;
        }

        switch (option)
        {
            case 0:
                GetComponent<WitcherEncounter>().StartYenEncounter();
                break;
            case 1:
                GetComponent<WitcherEncounter>().StartTrissEncounter();
                break;
            case 2:
                GetComponent<WitcherEncounter>().StartWitcherEncounter();
                break;
        }
    }
}