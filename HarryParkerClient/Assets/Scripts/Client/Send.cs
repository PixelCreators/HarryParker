using UnityEngine;
using UnityEngine.UI;

public class Send : MonoBehaviour
{
    
    public void SendMsg()
    {
        var resultManager = FindObjectOfType<ResultManager>();
        if (resultManager.Result != -1)
        {
            resultManager.JustDoIt = false;
            resultManager.SendMessage();
            GetComponent<Button>().interactable = false;
            FindObjectOfType<JustDoIT>().GetComponent<Button>().interactable = false;
        }
    }
}
