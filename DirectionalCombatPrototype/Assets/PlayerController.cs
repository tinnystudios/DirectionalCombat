using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterState mCharacterState = CharacterState.Idle;

    public CharacterAnimator m_CharacterAnimator;
    public Rigidbody m_RigidBody;

    public float m_MoveSpeed = 3;
    public float m_RotationSpeed = 30;
    public float m_JumpForce = 5;
    public float m_GroundDistance = 0.5F;

    public LayerMask m_GroundMask;

    private void Update()
    {
        if (m_CharacterAnimator.IsAttacking)
            return;

        ProcessMovement();

        if (PlayerInput.BasicAttackUp)
        {
            //Stun for a period
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

        if (PlayerInput.RightTriggerUp)
        {
            var target = TargettingUtils.GetNearestTarget<BasicAI>(transform);
            if (target != null)
            {
                var dir = target.transform.position - transform.position;
                dir.Normalize();
                dir.y = 0;
                transform.forward = dir;
            }
        }

    }

    IEnumerator Nudge()
    {
        yield return new WaitForSeconds(0.2F);
        //NudgeForward(1);
    }

    public void NudgeForward(float amount)
    {
        transform.position += transform.forward * amount;
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
