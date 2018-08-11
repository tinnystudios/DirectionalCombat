using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public Action<Status> OnDeath;

    public string mName = "default name";
    public float mHP = 5;
    public float maxHP = 5;

    private void Awake()
    {
        mHP = maxHP;
    }

    public virtual void TakeDamage(int amount, int currentCombo = 0)
    {
        mHP -= amount;

        if (mHP <= 0)
        {
            if (OnDeath != null)
            {
                OnDeath.Invoke(this);
            }

            Death();
        }
    }

    public float PercentageRemaining
    {
        get
        {
            return mHP / maxHP;
        }
    }

    public virtual void Death()
    {
        Destroy(gameObject);
    }

}

public interface ISender
{

}

