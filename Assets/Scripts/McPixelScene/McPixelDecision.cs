using System.Collections;
using UnityEngine;
public class McPixelDecision : MonoBehaviour
{
    public int decisionId;

    public GameObject ButtonTrigger;
    public GameObject KickTrigger;
    public GameObject BurnTrigger;

    
    void Awake()
    {
        DecisionDisplay.DecisionChosen += OnDecisionChosen;
    }

    void OnDestroy()
    {
        DecisionDisplay.DecisionChosen -= OnDecisionChosen;
    }

    private void OnDecisionChosen(int decisionId, int decision)
    {
        if (this.decisionId != decisionId)
        {
            return;
        }
        StartCoroutine( GetValue(decision));
    }

    private IEnumerator GetValue(int decision)
    {
        yield return new WaitForSeconds(2);
        switch (decision)
        {
            case 0:
                ButtonTrigger.SetActive(true);
                break;
            case 1:
                KickTrigger.SetActive(true);
                break;
            case 2:
                BurnTrigger.SetActive(true);
                break;
        }
    }
}
