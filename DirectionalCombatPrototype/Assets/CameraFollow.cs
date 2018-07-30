using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AngleOffsets
{
    public float angle = 0;
    public Vector3 offset;
}

[ExecuteInEditMode]
public class CameraFollow : MonoBehaviour
{
    public Transform m_Target;
    public float forwardDistance = 3;
    public float height = 15;
    public Vector3 offset;
    public float angle = 10;
    public bool facingLeft = false;
    public bool facingUp = true;

    public List<AngleOffsets> m_Angles;
    
    private void Update()
    {
        if (m_Target == null) return;

        var position = m_Target.transform.position;
        position.y += height;
        position += offset;

        if (facingLeft)
            position += m_Angles[0].offset;
        else
            position += m_Angles[1].offset;

        transform.position = Vector3.Lerp(transform.position,position,5 * Time.deltaTime);
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        if (Joystick.x == -1)
        {
            var a = new Vector3(25, -angle, 0);
            var q = Quaternion.identity * Quaternion.Euler(a);
            transform.localRotation = Quaternion.Lerp(transform.localRotation,q,3 * Time.deltaTime);
            facingLeft = true;
        }


        if (Joystick.x == 1)
        {
            var a = new Vector3(25, angle, 0);
            var q = Quaternion.identity * Quaternion.Euler(a);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, q, 3 * Time.deltaTime);
            facingLeft = false;
        }


        if (Joystick.y == -1)
        {

        }

        if (Joystick.y == 1)
        {
            facingUp = true;
        }

        if (Joystick.sqrMagnitude == 0)
        {
            //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0, 0);
        }
    }

    public Vector2 Joystick
    {
        get
        {
            return PlayerInput.JoystickPosition;
        }
    }
}
