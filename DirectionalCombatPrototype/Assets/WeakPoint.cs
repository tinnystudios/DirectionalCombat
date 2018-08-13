using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour, IDamagable
{
    public void TakeDamage(int i, IDamageSender sender)
    {
        var damageReceiver = GetComponentInParent<IDamageReceiver>();
        if (damageReceiver != null) damageReceiver.TakeDamage(i, sender);
    }
}
