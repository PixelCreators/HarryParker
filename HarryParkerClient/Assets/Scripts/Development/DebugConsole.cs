using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DebugConsole : MonoBehaviour
{
    public static DebugConsole Instance;

    public int LinesNumber;

    private string[] _outputConsole;
    private Text _text;

    private int k = 0;
    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        _text = GetComponent<Text>();
        _outputConsole = new string[LinesNumber];

        for (int i = 0; i < LinesNumber; i++)
            _outputConsole[i] = "";

    }

    public void PrintString(string text)
    {
        MoveStringsDown();
        AddStringToConsole(text);
        DisplayStringOnScreen();
    }

    private void MoveStringsDown()
    {
        for (int i = LinesNumber - 2; i >= 0; i--)
        {
            _outputConsole[i + 1] = _outputConsole[i];
        }
    }

    private void AddStringToConsole(string text)
    {
        _outputConsole[0] = text;
    }

    private void DisplayStringOnScreen()
    {
        _text.text = "";
        foreach (var s in _outputConsole)
            _text.text += s + '\n';
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
            PrintString("Test " + k++);
    }
}
