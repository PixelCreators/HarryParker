using UnityEngine;

public class ConnectButton : MonoBehaviour
{
    public void Connect()
    {
        Client.Instance.Connect();
    }
}
