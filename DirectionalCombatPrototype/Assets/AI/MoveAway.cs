using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAway : MovementAction
{
    public override void Perform()
    {
        m_MoveComponent.Move(-DirToTarget);
    }
}
