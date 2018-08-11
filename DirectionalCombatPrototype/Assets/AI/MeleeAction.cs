using System;
using System.Collections;
using UnityEngine;

public class MeleeAction : CoolDownAction
{
    public GameObject m_AttackObject;
    public bool mAttacked = false;

    public float force = 5;
    public float height = 2;
    public float duration = 0.5F;

    public AnimationCurve m_JumpCurve = null;
    public AnimationCurve m_DodgeCurve = null;

    public override bool IsDone
    {
        get
        {
            return mAttacked;
        }
    }

    public override void Perform()
    {
        var dir = Target.position - transform.position;
        dir.Normalize();
        dir.y = 0;

        var newPosition = transform.position + (dir * force);
        newPosition.y = height;

        this.MoveToPosition(transform, newPosition, duration, m_JumpCurve, m_DodgeCurve);
        StartCoolDown();
        mAttacked = true;
    }

    public override void Reset()
    {
        mAttacked = false;
        base.Reset();
    }
}
