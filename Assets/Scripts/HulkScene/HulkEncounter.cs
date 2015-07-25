using UnityEngine;
public class HulkEncounter : MonoBehaviour
{
    public GameObject Door;

    private static HulkEncounter _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void Finish()
    {
        _instance.Door.SetActive(false);
    }
}
