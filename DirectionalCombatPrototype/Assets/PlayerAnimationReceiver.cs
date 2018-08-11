using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationReceiver : MonoBehaviour
{
    public AttackController m_AttackController;

    public void Hit()
    {
        m_AttackController.CalculateHit();    
    }
}
