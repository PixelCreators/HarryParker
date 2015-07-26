using System.Collections;
using UnityEngine;
public class Spell : MonoBehaviour 
{
    public bool goUp;
    private bool _spawnNext;
    public GameObject FlamePrefab ;
    public Vector3 SpawnDelta;
    public float Spread;
    private Vector3 _spawnRelative;

    void OnEnable()
    {
        StartCoroutine(Cor());
    }

    IEnumerator Cor()
    {
        
    }
}
