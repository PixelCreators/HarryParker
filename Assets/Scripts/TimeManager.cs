using UnityEngine;
public class TimeManager : MonoBehaviour 
{
    public static float TimeMultiplier {get; private set; }

    private void Awake()
    {
        DecisionDisplay.DecisionDisplayed += delegate { TimeMultiplier = 0; };
        DecisionDisplay.DecisionChosen += delegate { TimeMultiplier = 1; };
        TimeMultiplier = 1;
    }
}
