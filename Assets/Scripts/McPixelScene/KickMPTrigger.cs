using System.Collections;
using UnityEngine;

public class KickMPTrigger : MonoBehaviour 
{
    bool PlayerInBound = false;
    public MPAI MP;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Tooltip.Show("Kopnij");
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
            gameObject.SetActive(false);
            MP.GetKicked();
        }
    }
}

