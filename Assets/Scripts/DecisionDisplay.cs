using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecisionDisplay : MonoBehaviour
{
    private static DecisionDisplay _instance;
    public GameObject ButtonPrefab;
    public Transform ButtonPanel;
    public Text DescriptionText;
    public Image DecisionSprite;
    private List<GameObject> activeButtons = new List<GameObject>() ;
    public Decision[] Decisions;
    private int _pendingDecision;
    public static List<Button> newButtons;
    public static event Action DecisionDisplayed;
    public static event Action<int, int> DecisionChosen;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    public static void TriggerDecision(int decisionId)
    {
        _instance._pendingDecision = decisionId;
        newButtons = new List<Button>();

        for (int i = 0; i < 4; i++)
        {
            _instance.ButtonPanel.GetComponent<ButtonPanel>().votes[i] = 0;
        }

        if(decisionId == 1)
        {
            _instance.ButtonPanel.GetComponent<ButtonPanel>().zero_3.gameObject.SetActive(false);
        }

        if(decisionId == 3)
        {
            _instance.ButtonPanel.GetComponent<ButtonPanel>().zero_3.gameObject.SetActive(true);
        }

        if (_instance.gameObject.activeInHierarchy)
        {
            return;
        }
        if (DecisionDisplayed != null)
        {
            DecisionDisplayed();
        }

        var decision = _instance.Decisions[decisionId];
        Debug.Log(decision.Description);
        Debug.Log(_instance.DescriptionText.name);
        _instance.DescriptionText.text = decision.Description;
        _instance.DecisionSprite.sprite = decision.sprite;
        for (int i = 0; i < decision.Options.Length; i++)
        {
            var option = decision.Options[i];
            var newButton = Instantiate(_instance.ButtonPrefab);
            newButton.GetComponent<DecisionButton>().Init(option, i);
            newButton.transform.SetParent(_instance.ButtonPanel);
            newButton.GetComponent<Button>().interactable = false;
            newButtons.Add(newButton.GetComponent<Button>());
            _instance.activeButtons.Add(newButton);
            _instance.gameObject.SetActive(true);
        }

        DecisionMessage decisionMessage = new DecisionMessage();

        int length = _instance.Decisions[decisionId].Options.Length + 1;
        decisionMessage.Decisions = new string[length];
        decisionMessage.Decisions[0] = _instance.Decisions[decisionId].Description;
        for (int i = 1; i < length; i++)
        {
            decisionMessage.Decisions[i] = _instance.Decisions[decisionId].Options[i - 1];
        }
        Server.Instance.SendDecisionText(decisionMessage);
    }

    public static void ChooseDecision(int decision)
    {
        newButtons[decision].interactable = true;
        newButtons[decision].Select();
        if (DecisionChosen != null)
        {
            DecisionChosen(_instance._pendingDecision, decision);
        }
        _instance.StartCoroutine(_instance.HidePanel());
    }

    public IEnumerator HidePanel()
    {
        if (!Server.Instance.mainPlayer)
            yield return new WaitForSeconds(5.0f);

        foreach (var activeButton in _instance.activeButtons)
        {
            Destroy(activeButton);
        }
        _instance.gameObject.SetActive(false);
    }
    public static void EnableMainPlayerDecisions()
    {
        foreach(var button in newButtons)
        {
            button.interactable = true;
        }
    }
}