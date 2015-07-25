using UnityEngine;

public class HulkChaseTrigger : MonoBehaviour
{
    public HulkAI Hulk;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Hulk.SMAAAAAAAAAAAAAAASH();
            gameObject.SetActive(false);
        }
    }
}