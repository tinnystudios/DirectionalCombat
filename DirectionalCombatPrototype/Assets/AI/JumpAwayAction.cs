using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAwayAction : CoolDownAction
{
    public float force = 5;
    public float height = 2;
    public float duration = 0.5F;

    public AnimationCurve m_JumpCurve = null;
    public AnimationCurve m_DodgeCurve = null;

    public override bool IsDone
    {
        get
        {
            return DistToTarget > m_Range;
        }
    }

    public override void Perform()
    {
        var dir = Target.position - transform.position;
        dir.Normalize();
        dir.y = 0;

        var newPosition = transform.position + (-dir * force);
        newPosition.y = height;

        this.MoveToPosition(transform, newPosition, duration, m_JumpCurve, m_DodgeCurve);
    }

}


public static class TransformUtils
{
    public static Coroutine MoveToPosition(this MonoBehaviour mono, Transform transform, Vector3 newPosition, float length, AnimationCurve jumpCurve, AnimationCurve moveCurve)
    {
        return mono.StartCoroutine(MoveTo(transform, newPosition, length, jumpCurve, moveCurve));
    }

    public static IEnumerator MoveTo(Transform transform, Vector3 newPosition, float length, AnimationCurve jumpCurve, AnimationCurve moveCurve)
    {
        var a = transform.position;
        var b = new Vector3(newPosition.x, transform.position.y, newPosition.z);

        for (float i = 0; i < 1.0F; i += Time.deltaTime / length)
        {
            var y = Mathf.LerpUnclamped(a.y, newPosition.y, jumpCurve.Evaluate(i));
            b.y = y;

            transform.position = Vector3.LerpUnclamped(a, b, moveCurve.Evaluate(i));
            yield return null;
        }
    }
}