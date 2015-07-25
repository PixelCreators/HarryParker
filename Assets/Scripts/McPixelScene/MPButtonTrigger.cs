using UnityEngine;

public class MPButtonTrigger : MonoBehaviour
{
    private bool PlayerInBound = false;
    public MPAI McPixel;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Tooltip.Show("Naciśnij");
            PlayerInBound = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Tooltip.Hide();
            PlayerInBound = false;
        }
    }

    private void Update()
    {
        if (PlayerInBound && Input.GetKeyDown(KeyCode.F))
        {
            McPixel.DropStone();
            gameObject.SetActive(false);
        }
    }
}