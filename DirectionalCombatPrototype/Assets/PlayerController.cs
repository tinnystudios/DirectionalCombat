using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterState mCharacterState = CharacterState.Idle;

    public CharacterAnimator m_CharacterAnimator;
    public AttackController m_AttackController;
    public CombatTargetSystem m_CombatTargetSystem;

    public Rigidbody m_RigidBody;

    public float m_MoveSpeed = 3;
    public float m_RotationSpeed = 30;
    public float m_JumpForce = 5;
    public float m_GroundDistance = 0.5F;

    public LayerMask m_GroundMask;
    private bool lockOn = false;
    private void Update()
    {
        if (!m_CharacterAnimator.IsAttacking)
        {
            ProcessMovement();
        }

        ProcessCombatAimSystem();

        if (PlayerInput.BasicAttackUp)
        {
            if (m_AttackController.canCombo)
            {
                m_AttackController.currentCombo = 1;
                m_CharacterAnimator.ContinueAttack();
            }
            else
            {
                m_AttackController.currentCombo = 0;
            }

            m_CharacterAnimator.BasicAttack(true);
            StartCoroutine(Nudge());
        }

        if (IsGrounded())
        {
            if (PlayerInput.JumpUp)
            {
                Jump();
            }
        }

        if (PlayerInput.TargetButtonUp)
        {
            lockOn = !lockOn;
        }


        var target = TargettingUtils.GetNearestTarget<BasicAI>(transform);
        if (target != null && lockOn)
        {
            var dir = target.transform.position - transform.position;
            dir.Normalize();
            dir.y = 0;
            transform.forward = Vector3.Lerp(transform.forward, dir,5 * Time.deltaTime);
        }
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
        m_CombatTargetSystem.SetCombatTargetCue(index);
    }

    public void SetSwordAngle(int angle)
    {
        //if(!m_CharacterAnimator.IsAttacking)
            m_CharacterAnimator.SetSwordAngle(angle);
    }

    public void ProcessCombatAimSystem()
    {
        if (AimDirection.x == -1)
        {
            SetSwordAngle(-40);
            SetCombatTargetCue(0);
        }


        if (AimDirection.x == 1)
        {
            SetSwordAngle(40);
            SetCombatTargetCue(2);
        }


        if (AimDirection.y == -1)
        {
            SetSwordAngle(100);
            SetCombatTargetCue(3);
        }

        if (AimDirection.y == 1)
        {
            SetSwordAngle(0);
            SetCombatTargetCue(1);
        }

        if (AimDirection.sqrMagnitude == 0)
        {
            SetSwordAngle (-1);
            SetCombatTargetCue(4);
        }

    }

    IEnumerator Nudge()
    {
        yield return new WaitForSeconds(0.2F);
        //NudgeForward(1);
    }

    public void NudgeForward(float amount, bool useDir = false)
    {
        var dir = transform.forward;
        transform.position += dir * amount;
    }

    public bool IsGrounded()
    {
        var pos = transform.position + Vector3.up * 0.3F;
        return Physics.Raycast(pos, Vector3.down, 0.5F, m_GroundMask);
    }

    public void Jump()
    {
        m_RigidBody.velocity = Vector3.up * m_JumpForce;
        m_CharacterAnimator.PlayJump(true);
        m_CharacterAnimator.PlayWalking(false);
    }

    public void ProcessMovement()
    {
        var outputPosition = new Vector3(PlayerInput.JoystickPosition.x, 0, PlayerInput.JoystickPosition.y);
        var mag = outputPosition.magnitude;
        var outputMoveSpeed = m_MoveSpeed;

        if (PlayerInput.JoystickPosition.sqrMagnitude != 0)
        {
            if(!lockOn)
            transform.forward = Vector3.Lerp(transform.forward, outputPosition, m_RotationSpeed * Time.deltaTime);

            if(PlayerInput.JoystickRPosition.sqrMagnitude == 0 && IsGrounded())
                m_CharacterAnimator.PlayWalking(true, mag * outputMoveSpeed);
            else
                outputMoveSpeed = m_MoveSpeed / 4;
        }
        else
        {
            m_CharacterAnimator.PlayWalking(false);
        }

        if (!IsGrounded())
        {
            m_CharacterAnimator.PlayWalking(false);
        }

        transform.position += outputPosition * Time.deltaTime * outputMoveSpeed;
    }

}

public enum CharacterState
{
    Idle,
    Moving,
    Attacking,
    Recoverying
}
