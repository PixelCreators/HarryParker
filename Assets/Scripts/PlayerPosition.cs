using UnityEngine;
public class PlayerPosition : MonoBehaviour
{
    public static Transform PlayerTransform;
    public static Transform HotdogPivot;
    public Transform HotdogPivotTransform;

    void Awake()
    {
        PlayerTransform = transform;
        HotdogPivot = HotdogPivotTransform;
    }

    public static float DistanceToPlayer(Vector3 pos)
    {
        return (pos - PlayerTransform.position).magnitude;
    }
}
