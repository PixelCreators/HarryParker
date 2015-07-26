using UnityEngine;
using UnityEngine.UI;

public class IceCreamEncounter : MonoBehaviour
{
    public Image ProgressBar;
    public float duration = 15f;
    public HulkAI Hulk;

    void OnEnable()
    {
        ProgressBar.gameObject.SetActive(true);
        Hulk.SetInvincible();
        startTime = Time.time;
    }

    private float startTime;
    void Update()
    {
        var progress = (Time.time - startTime)/duration;
        if (progress >= 1)
        {
            Hulk.StopSmashing();
            HulkEncounter.Finish();
            return;
        }
        ProgressBar.fillAmount = 1 - progress;
    }

    void OnDisable()
    {
        ProgressBar.gameObject.SetActive(false);
    }
}
