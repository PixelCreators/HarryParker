using UnityEngine;

public class Witch : MonoBehaviour
{
    public GameObject Portal;
    public void StartFight()
    {
        Debug.Log(name + " started fight");
    }

    public void OpenPortal()
    {
        Portal.SetActive(true);
    }
}
