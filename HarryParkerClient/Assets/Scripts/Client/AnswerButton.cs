using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    private Button _button;
    private Button[] _buttons;
    private ResultManager _resultManager;
    public int id;

    void Start()
    {
        _button = GetComponent<Button>();
        _resultManager = FindObjectOfType<ResultManager>();
    }

    public void Init(string text, int id)
    {
        GetComponentInChildren<Text>().text = text;
        this.id = id - 1;
    }

    public void UseButton()
    {
        _buttons = GetAllButtons();
        foreach (var button in _buttons)
        {
            button.interactable = true;
        }

        _button.interactable = false;

        _resultManager.SetResult(id);

    }

    Button[] GetAllButtons()
    {
        var answerButtons = FindObjectsOfType<AnswerButton>();

        var buttons = new Button[answerButtons.Length];
        for (int i = 0; i < answerButtons.Length; i++)
        {
            buttons[i] = answerButtons[i].GetComponent<Button>();
        }

        return buttons;
    }
}
