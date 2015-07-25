using UnityEngine;

public class HugTrigger : MonoBehaviour 
{
    bool PlayerInBound = false;
    public HulkAI Hulk;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Tooltip.Show("Przytul Hulka");
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
            Tooltip.Hide();
            Player.Kill();
        }
    }
}
