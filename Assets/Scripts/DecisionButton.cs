using UnityEngine;
using UnityEngine.UI;

public class DecisionButton : MonoBehaviour
{
    public Text ButtonText;
    private string decision;

    public void Init(string decision)
    {
        ButtonText.text = decision;
        this.decision = decision;
    }

    public void OnClick()
    {
        DecisionDisplay.ChooseDecision(decision);
    }
}
