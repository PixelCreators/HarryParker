using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
    NetworkClient _networkClient;
    public InputField IPInputField;

    public class MyMsgTypes
    {
        public static short Decision = 1001;
        public static short PlayerResult = 1002;
    };

    void Start()
    {
        _networkClient = new NetworkClient();
	}

    public void Connect()
    {
        ///sdf

        DebugConsole.Instance.PrintString("DupaDebug");
        var ip = IPInputField.text;
        _networkClient.Connect(ip, 1337);
        _networkClient.RegisterHandler(MsgType.Connect, OnConnected);
        _networkClient.RegisterHandler(MsgType.Error, OnError);
        _networkClient.RegisterHandler(MyMsgTypes.Decision, ShowMessage);
    }

    public void OnConnected(NetworkMessage netMsg)
    {
        DebugConsole.Instance.PrintString("Connected to server");
    }

    public void OnError(NetworkMessage netMsg)
    {
        DebugConsole.Instance.PrintString("Error");
    }

    public void ShowMessage(NetworkMessage netMsg)
    {
        var beginMessage = netMsg.ReadMessage<DecisionMessage>();
        DebugConsole.Instance.PrintString(beginMessage.Decisions[0]);
    }
	
    public void SendMessage(PlayerResult msg)
    {
        _networkClient.Send(MyMsgTypes.PlayerResult, msg);
    }

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            PlayerResult pr = new PlayerResult();
            pr.JustDoIt = true;
            pr.Result = 2;
            pr.PlayerID = 1;
            SendMessage(pr);
        }

	}
}
