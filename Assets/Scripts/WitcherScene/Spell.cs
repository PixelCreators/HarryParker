using System.Collections;
using UnityEngine;
public class Spell : MonoBehaviour 
{
    public bool goUp;
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
        while (true)
        {
            var pos = transform.position + _spawnRelative;
            var hitInfo = Physics2D.Raycast(pos, goUp ? Vector2.up : Vector2.down, SpawnDelta.magnitude, Constants.LayerMask.Walls);
            if (hitInfo.collider == null)
            {
                Instantiate(FlamePrefab, pos, Quaternion.identity);
                _spawnRelative += SpawnDelta;
                yield return new WaitForSeconds(0.3f);
            }
            else
            {
                yield break;
            }
        }
    }
}
