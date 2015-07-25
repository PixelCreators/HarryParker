using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int HP;

    public event Action Died;

    public void ApplyDamage()
    {
        HP -= 1;
        if (HP == 0 && Died != null)
        {
            Died();
        }
    }
}