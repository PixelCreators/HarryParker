using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnterRoomTrigger : MonoBehaviour
{
    public Transform CamPosition;
    public GameObject Door;
    public GameObject NoReturnCollider;

    void OnTriggerEnter2D(Collider2D other)
    {
        CameraControler.MoveTo(CamPosition.position);
        NoReturnCollider.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        GetComponent<Collider2D>().isTrigger = false;
        if (Door != null)
        {
            Door.gameObject.SetActive(true);
        }
    }
}
