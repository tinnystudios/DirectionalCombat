using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    public Animator m_Animator;
    public float m_AnimatorSpeed = 1;

    public List<TargetCube> m_Targets;

    private void Start()
    {
        m_Animator.speed = m_AnimatorSpeed;
    }

    private void Update()
    {

        if (AimDirection.x == -1)
        {
            m_Animator.SetInteger("SwordAngle", -40);
            SetCombatTargetCue(0);
        }


        if (AimDirection.x == 1)
        {
            m_Animator.SetInteger("SwordAngle", 40);
            SetCombatTargetCue(2);
        }


        if (AimDirection.y == -1)
        {
            m_Animator.SetInteger("SwordAngle", 100);
            SetCombatTargetCue(3);
        }

        if (AimDirection.y == 1)
        {
            m_Animator.SetInteger("SwordAngle", 0);
            SetCombatTargetCue(1);
        }

        if (AimDirection.sqrMagnitude == 0)
        {
            m_Animator.SetInteger("SwordAngle", -1);
            SetCombatTargetCue(4);
        }

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

    public void BasicAttack(bool state)
    {
        m_Animator.SetBool("Attacking", state);
    }

    public bool IsAttacking
    {
        get { return m_Animator.GetBool("Attacking"); }
    }

    public Vector2 AimDirection
    {
        get
        {
            if (PlayerInput.LastJoystickDirection.y < 0)
            {
                var newDirection = PlayerInput.JoystickRPosition;
                newDirection.x = -newDirection.x;
                return newDirection;
            }
            //If you're facing opposite direction
            return PlayerInput.JoystickRPosition;
        }
    }


    public void SetCombatTargetCue(int index)
    {
        
        for (int i = 0; i < m_Targets.Count; i++)
        {
            if (i == index)
            {
                m_Targets[i].SetActive(true);
            }
            else
            {
                m_Targets[i].SetActive(false);
            }
        }
    }

}

