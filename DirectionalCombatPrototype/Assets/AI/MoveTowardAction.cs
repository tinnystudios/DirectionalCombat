using UnityEngine;

public class MoveTowardAction : MovementAction
{
    //Requires action to be available
    [SerializeField] private CoolDownAction m_RequiredAction;

    [Header("Move Toward")]
    public float m_StopRange = 2;

    public override bool MeetConditions
    {
        get
        {
            return base.MeetConditions && !m_RequiredAction.IsCoolingDown;
        }
    }

    public override bool IsDone
    {
        get
        {
            return base.IsDone || DistToTarget <= m_StopRange;
        }
    }

    public override void Perform()
    {
        m_MoveComponent.Move(DirToTarget);
    }

}