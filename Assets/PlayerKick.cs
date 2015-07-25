using System.Collections;
using UnityEngine;
public class PlayerKick : MonoBehaviour 
{
    private static PlayerKick _instance;

    public const float KickTime = 0.2f;
    public GameObject KickSprite;
    public GameObject NormalSprite;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    public static void Kick(Vector3 pos)
    {
        if (_instance.KickSprite.transform.position.x > pos.x)
        {
            _instance.KickSprite.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            _instance.KickSprite.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private IEnumerator KickCoroutine()
    {
        yield return new WaitForSeconds(KickTime);
        KickSprite.SetActive(false);
        NormalSprite.SetActive(true);
    }
}
