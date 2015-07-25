using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DecissionTrigger : MonoBehaviour
{
    public int DecisionId;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            DecisionDisplay.TriggerDecision(DecisionId);
            gameObject.SetActive(false);
        }
    }
}
