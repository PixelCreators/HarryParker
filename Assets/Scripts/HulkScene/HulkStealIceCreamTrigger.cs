using UnityEngine;

public class HulkStealIceCreamTrigger : MonoBehaviour 
{
    bool PlayerInBound = false;
    public HulkAI Hulk;
    public IceCreamEncounter Encounter;
    public AudioSource HulkSmash;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Tooltip.Show("Ukradnij Loda");
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
            HulkSmash.Play();
            Tooltip.Hide();
            Hulk.SMAAAAAAAAAAAAAAASH();
            Encounter.gameObject.SetActive(true);
            Encounter.enabled = true;
            gameObject.SetActive(false);
        }
    } 
}
