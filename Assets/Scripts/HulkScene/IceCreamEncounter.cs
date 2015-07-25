using UnityEngine;
public class IceCreamEncounter : MonoBehaviour
{
    public Transform ProgressBar;
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
        ProgressBar.localScale = new Vector3(ProgressBar.localScale.x, 1 - progress);
    }

    void OnDisable()
    {
        ProgressBar.gameObject.SetActive(false);
    }
}
