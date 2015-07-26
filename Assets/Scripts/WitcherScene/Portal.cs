using System.Collections;
using UnityEngine;
public class Portal : MonoBehaviour
{
    public Transform P1;
    public float P1Speed;
    public Transform P2;
    public float P2Speed;
    public Transform P3;
    public float P3Speed;
    public AnimationCurve ScaleCurve;

    void Update()
    {
        P1.Rotate(new Vector3(0, 0, 1), P1Speed*Time.deltaTime);
        P2.Rotate(new Vector3(0, 0, 1), P2Speed*Time.deltaTime);
        P3.Rotate(new Vector3(0, 0, 1), P3Speed*Time.deltaTime);
    }

    public void Trigger()
    {
        
    }

    private IEnumerator PortalCoroutine()
    {
        yield return null;
    }
}
