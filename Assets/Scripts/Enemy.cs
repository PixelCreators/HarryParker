using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int HP;

    public event Action Died;
    //Arg is HP left
    public event Action<int> DamageApplied;
    private bool _invincible;

    public void SetInvincible()
    {
        _invincible = true;
    }

    public void ApplyDamage()
    {
        HP -= 1;
        if (DamageApplied != null)
        {
            DamageApplied(HP);
        }
        if (!_invincible && HP == 0 && Died != null)
        {
            Died();
        }
    }
}