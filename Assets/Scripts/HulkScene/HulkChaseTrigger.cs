using UnityEngine;

public class HulkChaseTrigger : MonoBehaviour
{
    public HulkAI Hulk;
    public AudioSource HulkSmash;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Hulk.SMAAAAAAAAAAAAAAASH();

            HulkSmash.Play();
            gameObject.SetActive(false);
        }
    }
}