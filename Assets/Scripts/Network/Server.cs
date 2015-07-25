using UnityEngine;
using UnityEngine.Networking;

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
        JustDoIt = true;
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
        //DebugConsole.Instance.PrintString("JustDoIT: " + pr.JustDoIt.ToString() + "Result: " + pr.Result.ToString() + "PlayerNumber: " + pr.PlayerID.ToString());
    }

    public void SendDecisionText(DecisionMessage msg)
    {
        NetworkServer.SendToAll(MyMsgTypes.Decision, msg);
    }

    public void EndVoting()
    {
        EndVotingMessage msg = new EndVotingMessage();
        msg.isJustDoIt = JustDoIt;
        NetworkServer.SendToAll(MyMsgTypes.EndVoting, msg);
    }

    void Update()
    {

    }
}
