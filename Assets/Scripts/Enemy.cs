using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int HP;

    public event Action Died;
    private bool _invincible;

    public void SetInvincible()
    {
        _invincible = true;
    }

    public void ApplyDamage()
    {
        HP -= 1;
        if (!_invincible && HP == 0 && Died != null)
        {
            Debug.Log("Hulk Died");
            Died();
        }
    }
}