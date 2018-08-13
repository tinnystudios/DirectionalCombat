using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyEnemy : MonoBehaviour, IDamageReceiver
{
    public WeakPoint[] weakPoints;

    void Awake()
    {
        weakPoints = GetComponentsInChildren<WeakPoint>();
        SelectRandomWeakPoint();
    }

    public void TakeDamage(int amount, IDamageSender sender)
    {
        var pushForce = sender.Stunable ? 1.0F : 0.5F;
        var stunTime = sender.Stunable ? 3.0F : 1.5F;

        var stun = GetComponent<IStunable>();

        if (stun != null)
            stun.Stun(stunTime);

        var pushable = GetComponent<IPushable>();

        if (pushable != null)
        {
            var dir = transform.position - sender.transform.position;
            dir.Normalize();
            pushable.Push(dir, pushForce);
        }

        SelectRandomWeakPoint();
    }

    public void SelectRandomWeakPoint()
    {

        var rnd = Random.Range(0, weakPoints.Length);

        for (int i = 0; i < weakPoints.Length; i++)
        {
            if (rnd == i)
                weakPoints[i].gameObject.SetActive(true);
            else
                weakPoints[i].gameObject.SetActive(false);
        }
    }
}

public interface IDamageReceiver
{
    void TakeDamage(int amount, IDamageSender sender);
}

public interface IDamageSender
{
    Transform transform { get; }
    bool Stunable { get; }
}