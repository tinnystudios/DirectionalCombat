using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    public AttackController m_AttackController;
    public Animator m_Animator;
    public float m_AnimatorSpeed = 1;

    public List<TargetCube> m_Targets;

    private void Start()
    {
        m_Animator.speed = m_AnimatorSpeed;
    }

    public void SetSwordAngle(int angle)
    {
        m_Animator.SetInteger("SwordAngle", angle);
    }

    public void PlayJump(bool state)
    {
        m_Animator.SetTrigger("Jump");
    }

    public void PlayWalking(bool state, float speed = 1)
    {
        m_Animator.SetBool("Walking", state);
        m_Animator.SetFloat("WalkSpeed", speed);
    }

    public void ContinueAttack()
    {
        m_AttackController.SetComboState(false);
        m_Animator.ResetTrigger("ContinueAttack");
        m_Animator.SetTrigger("ContinueAttack");
    }

    public void BasicAttack(bool state)
    {
        m_Animator.SetBool("Attacking", state);
    }

    public bool IsAttacking
    {
        get { return m_Animator.GetBool("Attacking"); }
    }


}

