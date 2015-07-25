using UnityEngine;
using UnityEngine.UI;

public class JustDoIT : MonoBehaviour
{
    public void JustDo()
    {
        var resultManager = FindObjectOfType<ResultManager>();
        if (resultManager.Result != -1)
        {
            resultManager.JustDoIt = true;  
            resultManager.JustDidIt = true; //Lock down entirely JustDoIt button

            resultManager.SendMessage();
            GetComponent<Button>().interactable = false;
            FindObjectOfType<Send>().GetComponent<Button>().interactable = false;
        }
    }

}
