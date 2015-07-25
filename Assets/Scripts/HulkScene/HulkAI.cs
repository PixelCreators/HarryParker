using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RushAt))]
[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(CharacterMotor))]
public class HulkAI : MonoBehaviour
{
    private bool _isSmashing;
    private RushAt _rush;
    private float _rushChance = 0.7f;
    private const float _screamChance = 0.5f;
    private float lastRush;
    private const float RushDelay = 1.5f;
    public AudioSource SmashScream;

    private void Awake()
    {
        GetComponent<Enemy>().Died += delegate { _isSmashing = false; };
        _rush = GetComponent<RushAt>();
        SmashScream = GetComponent<AudioSource>();
    }

    public void SMAAAAAAAAAAAAAAASH()
    {
        _isSmashing = true;
        StartCoroutine(SetDeadlyCoroutine());
    }

    private IEnumerator SetDeadlyCoroutine()
    {
        yield return new WaitForSeconds(2);
        _rush.KillOnContact = true;
    }

    private void OnEnable()
    {
        lastRush = Time.time + RushDelay*5;
    }

    private void Update()
    {
        if (!_isSmashing)
        {
            return;
        }

        if (_rush.IsRushing)
        {
            return;
        }

        if (lastRush + RushDelay < Time.time && Random.value < _rushChance)
        {
            if (Random.value < _screamChance)
            {
                SmashScream.Play();
            }
            _rush.Rush(PlayerPosition.PlayerTransform.position);
            _rushChance = 0;
        }
        else if (lastRush + RushDelay < Time.time)
        {
            _rushChance += 0.05f;
        }
    }
}
