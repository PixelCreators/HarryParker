using UnityEngine;

public class HulkChaseTrigger : MonoBehaviour
{
    public HulkAI Hulk;
    public AudioClip HulkSmash;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Hulk.SMAAAAAAAAAAAAAAASH();

            AudioSource.PlayClipAtPoint(HulkSmash, Vector3.zero);
            gameObject.SetActive(false);
        }
    }
}