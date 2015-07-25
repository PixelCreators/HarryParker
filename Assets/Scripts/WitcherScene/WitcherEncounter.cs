using UnityEngine;
public class WitcherEncounter : MonoBehaviour
{
    private WitcherEncounter _instance;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
