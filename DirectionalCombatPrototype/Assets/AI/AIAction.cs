using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A cooldown action does not affect the next action, it is only the cooldown for this action
/// </summary>
public abstract class CoolDownAction : AIAction
{
    [SerializeField] protected float m_CoolDownDuration = 1;
    protected bool mCoolingDown = false;

    public override bool MeetConditions
    {
        get
        {
            return !mCoolingDown && base.MeetConditions;
        }
    }

    public bool IsCoolingDown { get { return mCoolingDown; } }

    public virtual void StartCoolDown()
    {
        if (!mCoolingDown)
            StartCoroutine(CoolDown(m_CoolDownDuration));
    }

    public virtual IEnumerator CoolDown(float coolDownTime)
    {
        mCoolingDown = true;
        yield return new WaitForSeconds(coolDownTime);
        mCoolingDown = false;
    }
}

/// <summary>
/// 
/// </summary>
public abstract class AIAction : MonoBehaviour
{
    private Transform mTarget = null;

    [Header("Basic")]
    [SerializeField] protected float m_PerformCost = 1; //The cost is how long it takes to recovery before the next action
    [SerializeField] protected float m_Range = 2;

    [Header("Settings")]
    [SerializeField] private List<GoalStateEffect> effects = new List<GoalStateEffect>();
    [SerializeField] private Color m_RangeGizmosColor = Color.white;

    public Dictionary<GoalState, GoalStateEffect> m_LookUp = new Dictionary<GoalState, GoalStateEffect>();

    public abstract void Perform();
    public virtual void Reset() { }

    public void SetTarget(Transform target)
    {
        mTarget = target;
    }

    #region Properties

    public abstract bool IsDone { get; }
    public virtual bool IsFault { get { return false; } }
    public virtual bool MeetConditions { get { return InRange; } }
    public Transform Target { get { return mTarget; } }
    public float PerformCost { get { return m_PerformCost; } }

    private bool InRange
    {
        get
        {
            if (mTarget == null) return false;
            var dist = Vector3.Distance(transform.position, mTarget.position);
            return dist <= m_Range;
        }
    }

    #endregion

    private void Awake()
    {
        foreach (var effect in effects)
            m_LookUp.Add(effect.m_GoalState, effect);
    }

    public Vector3 DirToTarget
    {
        get
        {
            var dir = Target.position - transform.position;
            dir.Normalize();
            return dir;
        }
    }

    public float DistToTarget
    {
        get
        {
            return Vector3.Distance(Target.position, transform.position);
        }
    }

    #region Editors

    private void OnValidate()
    {
        foreach (var effect in effects)
        {
            if(effect.m_GoalState != null)
                effect.name = effect.m_GoalState.name;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = m_RangeGizmosColor;
        Gizmos.DrawWireSphere(transform.position, m_Range);
    }

    #endregion
}

[System.Serializable]
public class GoalStateEffect
{
    [HideInInspector] public string name = "Effect";
    public GoalState m_GoalState = null;
    public float effectiveness = 1;
}