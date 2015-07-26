using UnityEngine;
public class YouDiedScreen : MonoBehaviour 
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Server.Instance.ResetClients();
            Application.LoadLevel("1");
        }
    }
}
