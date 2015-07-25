using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MyMsgTypes
{
    public static short Decision = 1001;
    public static short PlayerResult = 1002;
};

public class Client : MonoBehaviour
{
    public static Client Instance;
    public NetworkClient NetworkClient;
    private InputField IPInputField;

    
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
        Application.targetFrameRate = 30;
        NetworkClient = new NetworkClient();
        IPInputField = FindObjectOfType<InputField>();
    }

    public void Connect()
    {
        DebugConsole.Instance.PrintString("Trying to connect with server");
        var ip = IPInputField.text;
        NetworkClient.Connect(ip, 1337);
        NetworkClient.RegisterHandler(MsgType.Connect, OnConnected);
        NetworkClient.RegisterHandler(MsgType.Error, OnError);
        NetworkClient.RegisterHandler(MyMsgTypes.Decision, GetMessage);
        Application.LoadLevel("DecisionScene");
    }

    public void OnConnected(NetworkMessage netMsg)
    {
        DebugConsole.Instance.PrintString("Connected to server");
    }

    public void OnError(NetworkMessage netMsg)
    {
        DebugConsole.Instance.PrintString("Error");
    }

    public void GetMessage(NetworkMessage netMsg)
    {
        FindObjectOfType<ResultManager>().GetMessage(netMsg);
    }
	

}
