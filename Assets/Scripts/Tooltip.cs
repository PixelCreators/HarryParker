using UnityEngine;
using UnityEngine.UI;
public class Tooltip : MonoBehaviour 
{
    private static Tooltip _instance;
    public Text TooltipText;

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

    public static void Show(string msg)
    {
        _instance.TooltipText.text = msg;
        _instance.TooltipText.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        _instance.TooltipText.gameObject.SetActive(false);
    }
}
