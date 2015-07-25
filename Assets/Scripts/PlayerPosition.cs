using UnityEngine;
public class PlayerPosition : MonoBehaviour
{
    public static Transform PlayerTransform;

    void Awake()
    {
        PlayerTransform = transform;
    }

    public static float DistanceToPlayer(Vector3 pos)
    {
        return (pos - PlayerTransform.position).magnitude;
    }
}
