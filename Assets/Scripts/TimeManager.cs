using UnityEngine;
public class TimeManager : MonoBehaviour 
{
    public static float TimeMultiplier {get; private set; }

    private void Awake()
    {
        DecisionDisplay.DecisionDisplayed += OnDecisionDisplayed;
        DecisionDisplay.DecisionChosen += OnDecisionChosen;
        TimeMultiplier = 1;
    }

    private void OnDecisionChosen(int arg1, int arg2)
    {
        TimeMultiplier = 1;
    }

    private void OnDecisionDisplayed()
    {
        TimeMultiplier = 0;
    }

    private void OnDestroy()
    {
        DecisionDisplay.DecisionDisplayed -= OnDecisionDisplayed;
        DecisionDisplay.DecisionChosen -= OnDecisionChosen; 
    }
}
