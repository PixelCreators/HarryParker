using UnityEngine;
using UnityEngine.UI;

public class DecisionButton : MonoBehaviour
{
    public Text ButtonText;
    private int id;

    public void Init(string decision, int id)
    {
        ButtonText.text = decision;
        this.id = id;
    }

    public void OnClick()
    {
        DecisionDisplay.ChooseDecision(id);
    }
}
