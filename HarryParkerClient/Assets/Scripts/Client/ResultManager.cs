using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class ResultManager : MonoBehaviour
{
    private float _currentTime;
    private bool _chosen;
    private bool _sended;
    public bool JustDidIt;
    private string[] _decisions;

    public GameObject ButtonPrefab;
    public bool JustDoIt;
    public int Result;
    public long PlayerID;

    private Transform _verticalLayoutGroup;
    private Text _storyText;
    private Text _timerText;
    private Button _send, _justDoIt;
    private Button _invisibleButton;
    private GameObject _justDoItPanel;

    void Start ()
    {
        PlayerID = ((long)Random.Range(0, int.MaxValue) << 32) | (long)Random.Range(0, int.MaxValue);
        Result = -1;

        _timerText = FindObjectOfType<TimerText>().GetComponent<Text>();
        _storyText = FindObjectOfType<StoryText>().GetComponent<Text>();
        _verticalLayoutGroup = FindObjectOfType<ButtonsLayout>().transform;
        _send = FindObjectOfType<Send>().GetComponent<Button>();
        _justDoIt = FindObjectOfType<JustDoIT>().GetComponent<Button>();
        _invisibleButton = FindObjectOfType<InvisibleButton>().GetComponent<Button>();
        _justDoItPanel = FindObjectOfType<JustDoItPanel>().gameObject;

        _invisibleButton.gameObject.SetActive(false);
        _justDoItPanel.SetActive(false);
    }
	
    private void DestroyButtons()
    {
        var buttons = FindObjectsOfType<AnswerButton>();
        foreach (var button in buttons)
        {
            Debug.Log(button.name);
            Destroy(button.gameObject);
        }
    }

    private void ReadMessage(NetworkMessage netMsg)
    {
        var msg = netMsg.ReadMessage<DecisionMessage>();
        _decisions = msg.Decisions;
    }
    
    public void EndVoting(bool IsJustDoIt)
    {
        _sended = true;
        Debug.Log(IsJustDoIt);
        if (IsJustDoIt)
            _justDoItPanel.SetActive(true);
        else
            SetRandomResult();
    }

    IEnumerator Timer(float time)
    {
        _timerText.gameObject.SetActive(true);
        for (_currentTime = 0; _currentTime <= time; _currentTime += Time.deltaTime)
        {
            for (int i = 0; i < 5; i++)
            {
                yield return null;
                _currentTime += Time.deltaTime;
            }

            if (_currentTime >= 10)
                break;
            _timerText.text = (time - _currentTime).ToString();

            if (_sended == true)
            {
                _timerText.gameObject.SetActive(false);
                break;
            }
        }
        _timerText.text = "0.00";

        if (_send.interactable)
            SetRandomResult();
    }


    public void SetResult(int id)
    {
        Result = id;
        _chosen = true;
    }

    public void DisplayOptions()
    {
        //Set buttons visible
        _send.interactable = true;
        if (!JustDidIt) _justDoIt.interactable = true;

        //Instatiate decision buttons 
        for (int i = 0; i < _decisions.Length; i++)
        {
            if (i == 0)
                _storyText.text = _decisions[0];    //Fill story text
            else
            {
                var newButton = Instantiate(ButtonPrefab);
                newButton.transform.SetParent(_verticalLayoutGroup, false);
                newButton.GetComponent<AnswerButton>().Init(_decisions[i], i);
            }
        }

        //Start Timer coutdown
        StartCoroutine(Timer(10.0f));
    }


    private void SetRandomResult()
    {
        var answerButtons = FindObjectsOfType<AnswerButton>();
        
        var randomButton = answerButtons[Random.Range(0, answerButtons.Length - 1)].GetComponent<Button>();
        randomButton.Select();
        randomButton.onClick.Invoke();
        _send.onClick.Invoke();
    }

    public void GetMessage(NetworkMessage netMsg)
    {
        //Reset some vars
        _currentTime = 0.0f;
        _sended = false;
        _chosen = false;
        Result = -1;
        _invisibleButton.gameObject.SetActive(false);
        _justDoItPanel.SetActive(false);

        DestroyButtons();
        ReadMessage(netMsg);
        DisplayOptions();
    }

    public void SendMessage()
    {
        _sended = true;
        PlayerResult playerResult = new PlayerResult();
        playerResult.JustDoIt = JustDoIt;
        playerResult.Result = Result;
        playerResult.PlayerID = PlayerID;
        Client.Instance.NetworkClient.Send(MyMsgTypes.PlayerResult, playerResult);
        Debug.Log("Message!");
        _invisibleButton.gameObject.SetActive(true);
    }

    public void Reset()
    {
        JustDidIt = false;
        JustDoIt = false;
    }
}