using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class MyMsgTypes
{
    public static short Decision = 1001;
    public static short PlayerResult = 1002;
    public static short EndVoting = 1003;
};


public class Server : MonoBehaviour
{
    public static Server Instance;
    
    public bool JustDoIt;

    List<long> Players;

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
    }

    void Start()
    {
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
        JustDoIt = pr.JustDoIt;
        //DebugConsole.Instance.PrintString("JustDoIT: " + pr.JustDoIt.ToString() + "Result: " + pr.Result.ToString() + "PlayerNumber: " + pr.PlayerID.ToString());
    }
    

    public IEnumerator Voting()
    {
        var numberOfPlayers = NetworkServer.connections.Count - 1;
        votingOptions = new int[lastMsg.Decisions.Length - 1];

        while (true)
        {
            if (playersVoted >= numberOfPlayers)
            {
                break;
            }
            yield return null;
        }
        
        for(int i = 0; i < lastMsg.Decisions.Length - 1; i++)
        {
            Debug.Log(votingOptions[i]);
            Debug.Log(numberOfPlayers);
            if(votingOptions[i] == numberOfPlayers)
            {
                DecisionDisplay.ChooseDecision(i);
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            EndVoting();
    }
}
