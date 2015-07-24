using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
    NetworkClient _networkClient;
    public InputField IPInputField;
    

    void Start ()
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
    }

    public void OnConnected(NetworkMessage netMsg)
    {
        DebugConsole.Instance.PrintString("Connected to server");
    }

    public void OnError(NetworkMessage netMsg)
    {
        DebugConsole.Instance.PrintString("Error");
    }
	
	void Update ()
    {
	
	}
}
