using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    public Animator m_Animator;
    public List<GameObject> m_TargetCubes;

    private void Update()
    {

        if (DPadPosition.x == -1)
        {
            m_Animator.SetInteger("SwordAngle", -40);
            SetCombatTargetCue(0);
        }


        if (DPadPosition.x == 1)
        {
            m_Animator.SetInteger("SwordAngle", 40);
            SetCombatTargetCue(2);
        }


        if (DPadPosition.y == -1)
        {
            m_Animator.SetInteger("SwordAngle", 100);
            SetCombatTargetCue(3);
        }

        if (DPadPosition.y == 1)
        {
            m_Animator.SetInteger("SwordAngle", 0);
            SetCombatTargetCue(1);
        }

        if (DPadPosition.sqrMagnitude == 0)
        {
            m_Animator.SetInteger("SwordAngle", -1);
            SetCombatTargetCue(-1);
        }

        if (PlayerInput.BasicAttack)
        {
            m_Animator.SetBool("Attacking", true);
        }
        else
        {
            m_Animator.SetBool("Attacking", false);
        }

        if (PlayerInput.JoystickPosition.sqrMagnitude == 0)
        {
            m_Animator.SetBool("Walking", false);
        }
        else
        {
            m_Animator.SetBool("Walking", true);
        }
    }

    public Vector2 DPadPosition
    {
        get
        {
            if (PlayerInput.LastJoystickDirection.y < 0)
            {
                var newDPAD = PlayerInput.DPadPosition;
                newDPAD.x = -newDPAD.x;
                return newDPAD;
            }
            //If you're facing opposite direction
            return PlayerInput.DPadPosition;
        }
    }

    public void SetCombatTargetCue(int index)
    {
        for (int i = 0; i < m_TargetCubes.Count; i++)
        {
            if (i == index)
            {
                m_TargetCubes[i].SetActive(true);
            }
            else
            {
                m_TargetCubes[i].SetActive(false);
            }
        }
    }

}

public static class PlayerInput
{
    public static Vector2 LastJoystickDirection;

    public static Vector2 JoystickPosition
    {
        get
        {
            var pos = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if(pos.sqrMagnitude >= 0.2F)
            LastJoystickDirection = pos;
            return pos;
        }
    }

    public static Vector2 DPadPosition
    {
        get
        {
            var pos = new Vector2(Input.GetAxisRaw("DPadX"), Input.GetAxisRaw("DPadY"));
            return pos;
        }
    }

    public static bool BasicAttack
    {
        get
        {
            return Input.GetButton("Fire1");
        }
    }
}

