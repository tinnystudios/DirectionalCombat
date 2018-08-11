using System;
using UnityEngine;

public class EnemyStatus : Status
{
    public override void TakeDamage(int amount, int currentCombo = 0)
    {
        base.TakeDamage(amount);

        var flicker = GetComponentInChildren<IFlicker<Color>>();

        if (flicker != null)
            flicker.Flicker(Color.red);

    }
}

