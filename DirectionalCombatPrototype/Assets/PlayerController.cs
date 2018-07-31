using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float m_MoveSpeed = 3;
    public float m_RotationSpeed = 30;

    private void Update()
    {
        var outputPosition = new Vector3(PlayerInput.JoystickPosition.x, 0, PlayerInput.JoystickPosition.y);
        transform.position += outputPosition * Time.deltaTime * m_MoveSpeed;

        if (PlayerInput.JoystickPosition.sqrMagnitude != 0)
        {
            transform.forward = Vector3.Lerp(transform.forward, outputPosition,m_RotationSpeed * Time.deltaTime);
        }
    }

}
