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
    public AudioSource HulkSmash;

    public AudioClip DeathMetal;

    public GameObject AnimationHolder;
    public GameObject DeadSprite;

    private void Awake()
    {
        GetComponent<Enemy>().Died += OnDied;
        _rush = GetComponent<RushAt>();
        SmashScream = GetComponent<AudioSource>();
    }

    public void StopSmashing()
    {
        _isSmashing = false;
        AnimationHolder.SetActive(false);
        DeadSprite.SetActive(true);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private void OnDied()
    {
        StopSmashing();
        _rush.enabled = false;
        _rush.WallContact = true;
        HulkEncounter.Finish();
    }

    public void SMAAAAAAAAAAAAAAASH()
    {
        MyAudio.PlayBackgroundMusic(DeathMetal);
        _isSmashing = true;
        lastRush = Time.time + RushDelay;
        StartCoroutine(SetDeadlyCoroutine());
    }

    public void SetInvincible()
    {
        GetComponent<Enemy>().SetInvincible();
    }

    private IEnumerator SetDeadlyCoroutine()
    {
        yield return new WaitForSeconds(2);
        _rush.KillOnContact = true;
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
