using UnityEngine;

public static class PlayerInput
{
    public static Vector2 LastJoystickDirection;

    public static Vector2 JoystickPosition
    {
        get
        {
            var pos = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (pos.sqrMagnitude >= 0.2F)
                LastJoystickDirection = pos;
            return pos;
        }
    }

    public static Vector2 JoystickRPosition
    {
        get
        {
            var pos = new Vector2(Input.GetAxisRaw("HorizontalR"), Input.GetAxisRaw("VerticalR"));
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

    public static bool BasicAttackUp
    {
        get
        {
            return Input.GetButtonUp("BasicAttack");
        }
    }

    public static bool JumpUp
    {
        get
        {
            return Input.GetButtonUp("Jump");
        }
    }

    public static bool Jump
    {
        get
        {
            return Input.GetButton("Jump");
        }
    }

    public static bool RightTriggerUp
    {
        get
        {
            return Input.GetButtonUp("RightTrigger");
        }
    }
}

