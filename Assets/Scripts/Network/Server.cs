using UnityEngine;
using UnityEngine.Networking;

public class MyMsgTypes
{
    public static short Decision = 1001;
    public static short PlayerResult = 1002;
};


public class Server : MonoBehaviour
{

    void Start()
    {
        NetworkServer.Listen(1337);

        if(NetworkServer.active)
            DebugConsole.Instance.PrintString("Server started");

        NetworkServer.RegisterHandler(MyMsgTypes.PlayerResult, GetPlayerResult);

    }

    public void GetPlayerResult(NetworkMessage msg)
    {
        var pr = msg.ReadMessage<PlayerResult>();
        DebugConsole.Instance.PrintString("JustDoIT: " + pr.JustDoIt.ToString() + "Result: " + pr.Result.ToString() + "PlayerNumber: " + pr.PlayerID.ToString());
    }

    void SendDecisionText(string[] decMsg)
    {
        DecisionMessage msg = new DecisionMessage();
        msg.Decisions = decMsg;

        NetworkServer.SendToAll(MyMsgTypes.Decision, msg);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            string[] msg = new string[1];
            msg[0] = "Test message";
            SendDecisionText(msg);
        }
    }
}
