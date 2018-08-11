using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraFollow : MonoBehaviour
{
    public Transform m_Target;

    public float offsetDistance;
    public float height = 5;
    public float zOffset = -4;

    public float dotProduct;

    public float posSpeed = 2;
    public float rotSpeed = 2;
    private void Update()
    {
        if (m_Target == null) return;

        var aimPosition = m_Target.position - m_Target.forward * offsetDistance;

        dotProduct = Quaternion.Dot(Quaternion.identity, m_Target.rotation);
        if (dotProduct <= 0.6F)
        {
            aimPosition = m_Target.position + m_Target.forward * offsetDistance;
        }


        var frontPosition = m_Target.position + m_Target.forward * offsetDistance;

        var position = aimPosition;

        position.y += height;
        position.z += zOffset;

       
        transform.position = Vector3.Lerp(transform.position,position, posSpeed * Time.deltaTime);
        var rot = Quaternion.LookRotation(frontPosition - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotSpeed * Time.deltaTime);

        if (!Application.isPlaying)
        {
            transform.position = position;
            transform.rotation = rot;
        }

    }

    public Vector2 Joystick
    {
        get
        {
            return PlayerInput.JoystickPosition;
        }
    }

    private void OnDrawGizmos()
    {
        var aimPosition = m_Target.position - m_Target.forward * offsetDistance;
        Gizmos.DrawLine(m_Target.position, aimPosition);
    }

}
