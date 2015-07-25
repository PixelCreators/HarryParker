using System;
using UnityEngine;
public class Player : MonoBehaviour
{
    private static Player _instance;

    void Awake()
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

    public static event Action Died;

    public GameObject DiedScreen;
    public static void Kill()
    {
        _instance.DiedScreen.SetActive(true);
        if (Died != null)
        {
            Died();
        }
    }
}
