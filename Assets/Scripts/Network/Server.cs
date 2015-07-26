using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class MyMsgTypes
{
    public static short Decision = 1001;
    public static short PlayerResult = 1002;
    public static short EndVoting = 1003;
    public static short ResetClients = 1004;
};


public class Server : MonoBehaviour
{
    public static Server Instance;
    
    public bool JustDoIt;
    int votingNumber;

    List<long> Players;
    public ButtonPanel buttonPanel;
    int playersVoted;
    int[] votingOptions;
    DecisionMessage lastMsg;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        buttonPanel = FindObjectOfType<ButtonPanel>();
    }
    

    void Start()
    {
        Debug.Log(buttonPanel.name);
        NetworkServer.Listen(1337);

        if (NetworkServer.active)
            Debug.Log("Server started");
            //DebugConsole.Instance.PrintString("Server started");

        NetworkServer.RegisterHandler(MyMsgTypes.PlayerResult, GetPlayerResult);
    }

    public void GetPlayerResult(NetworkMessage msg)
    {
        var pr = msg.ReadMessage<PlayerResult>();
        Debug.Log("JustDoIT: " + pr.JustDoIt.ToString() + "Result: " + pr.Result.ToString() + "PlayerNumber: " + pr.PlayerID.ToString());

        playersVoted++;
        votingOptions[pr.Result]++;

        buttonPanel.votes[pr.Result]++;


        JustDoIt = pr.JustDoIt;
        if (JustDoIt)
        {
            EndVoting();
            DecisionDisplay.ChooseDecision(pr.Result);
        }
            //DebugConsole.Instance.PrintString("JustDoIT: " + pr.JustDoIt.ToString() + "Result: " + pr.Result.ToString() + "PlayerNumber: " + pr.PlayerID.ToString());
    }
    

    public IEnumerator Voting()
    {
        votingNumber++;
        var numberOfPlayers = NetworkServer.connections.Count - 1;
        votingOptions = new int[lastMsg.Decisions.Length - 1];

        for (int i = 0; i < 4; i++)
        {
            Debug.Log(buttonPanel.name);
            buttonPanel.votes[i] = 0;
        }

        while (true)
        {
            if (votingNumber == 3)
            {
                DecisionDisplay.EnableMainPlayerDecisions();
                JustDoIt = true;
                EndVoting();
                JustDoIt = false;
                Debug.Log("Pass voting");
                votingNumber = 0;
                yield break;
            }
            if (playersVoted >= numberOfPlayers && !JustDoIt)
            {
                break;
            }
            if (JustDoIt)
            {
                votingNumber = 0;
                yield break;
            }
            yield return null;
        }
        
        for(int i = 0; i < lastMsg.Decisions.Length - 1; i++)
        {
            if(votingOptions[i] == numberOfPlayers)
            {
                DecisionDisplay.ChooseDecision(i);
                votingNumber = 0;
                yield break;
            }
        }

        SendDecisionText(lastMsg);
        
        yield return null;
    }

    public void SendDecisionText(DecisionMessage msg)
    {
        lastMsg = msg;
        playersVoted = 0;

        NetworkServer.SendToAll(MyMsgTypes.Decision, msg);

        StartCoroutine(Voting());
    }

    public void EndVoting()
    {
        EndVotingMessage msg = new EndVotingMessage();
        msg.isJustDoIt = JustDoIt;
        NetworkServer.SendToAll(MyMsgTypes.EndVoting, msg);
    }

    public void ResetClients()
    {
        NetworkServer.SendToAll(MyMsgTypes.ResetClients, new EndVotingMessage());
    }

    void Update()
    {

    }
}
