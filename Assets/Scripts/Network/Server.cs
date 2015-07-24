using UnityEngine;
using UnityEngine.Networking;

public class Server : MonoBehaviour
{

    void Start()
    {
        NetworkServer.Listen(1337);

        if(NetworkServer.active)
            DebugConsole.Instance.PrintString("Server started");

    }
}
