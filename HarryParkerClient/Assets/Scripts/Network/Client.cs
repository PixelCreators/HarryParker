using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MyMsgTypes
{
    public static short Decision = 1001;
    public static short PlayerResult = 1002;
    public static short EndVoting = 1003;
    public static short ResetClients = 1004;
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
    }

    public void Connect()
    {
        //DebugConsole.Instance.PrintString("Trying to connect with server");
        var ip = FindObjectOfType<InputField>().text;
        NetworkClient.Connect(ip, 1337);
        NetworkClient.RegisterHandler(MsgType.Connect, OnConnected);
        NetworkClient.RegisterHandler(MsgType.Error, OnError);
        NetworkClient.RegisterHandler(MyMsgTypes.Decision, GetMessage);
        NetworkClient.RegisterHandler(MyMsgTypes.EndVoting, EndVoting);
        NetworkClient.RegisterHandler(MyMsgTypes.ResetClients, ResetClients);
    }

    public void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server");
        Application.LoadLevel("DecisionScene");
        //DebugConsole.Instance.PrintString("Connected to server");
    }

    public void OnError(NetworkMessage netMsg)
    {
        Debug.Log("Error");
        if (Application.loadedLevelName != "ConnectScene")
            Application.LoadLevel("ConnectScene");
        //DebugConsole.Instance.PrintString("Error");
    }

    public void GetMessage(NetworkMessage netMsg)
    {
        FindObjectOfType<ResultManager>().GetMessage(netMsg);
    }

    public void EndVoting(NetworkMessage netMsg)
    {
        FindObjectOfType<ResultManager>().EndVoting(netMsg.ReadMessage<EndVotingMessage>().isJustDoIt);
    }
	
    public void ResetClients(NetworkMessage netMsg)
    {
        FindObjectOfType<ResultManager>().Reset();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

}
