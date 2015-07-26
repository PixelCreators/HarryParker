using UnityEngine;

public class KickHulkTrigger : MonoBehaviour 
{
    bool PlayerInBound = false;
    public HulkAI Hulk;
    public AudioClip HulkSmash;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Tooltip.Show("Obraź Hulka");
            PlayerInBound = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Tooltip.Hide();
            PlayerInBound = false;
        }
    }

    void Update()
    {
        if (PlayerInBound && Input.GetKeyDown(KeyCode.F))
        {
            Hulk.SMAAAAAAAAAAAAAAASH();
            AudioSource.PlayClipAtPoint(HulkSmash, Vector3.zero);
            Tooltip.Hide();
            gameObject.SetActive(false);
        }
    }
}
