using UnityEngine;

public abstract class MovementAction : AIAction
{
    [Header("Move Component")]
    public MoveComponent m_MoveComponent;

    private float breakDuration = 0.5F;
    private float startTime = 0;

    public override bool IsDone
    {
        get
        {
            return Time.time - startTime >= breakDuration || Target == null;
        }
    }

    public override void Reset()
    {
        startTime = Time.time;
        base.Reset();
    }

}