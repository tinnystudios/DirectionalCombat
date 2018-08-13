using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationReceiver : MonoBehaviour
{
    public PlayerController m_PlayerController;
    public CharacterAnimator m_Animator;
    public AttackController m_AttackController;

    public void Hit()
    {
        m_AttackController.CalculateHit();    
    }

    public void Combo(int i)
    {
        m_AttackController.SetComboState(i != 0);
    }

    public void Nudge()
    {
        m_PlayerController.NudgeForward(0.5F, true);
    }
}
