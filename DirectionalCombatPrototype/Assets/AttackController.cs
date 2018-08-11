using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public LayerMask m_LayerMask;
    public float range = 1;
    public float size = 1;
    public Vector3 FrontPosition
    {
        get
        {
            return transform.position + (transform.forward * size/2) + (transform.up);
        }
    }

    public void CalculateHit()
    {
        var dir = transform.forward;
        var position = TargetCube.m_SelectedTarget == null ? FrontPosition : TargetCube.m_SelectedTarget.transform.position;
        var outputSize = TargetCube.m_SelectedTarget == null ? 0.5F : size;
        var enemies = Physics.SphereCastAll(position, size, dir, 0, m_LayerMask);

        foreach (var enemyHit in enemies)
        {
            var iDamagable = enemyHit.collider.GetComponent<IDamagable>();
            if (iDamagable != null)
            {
                Debug.Log("Hit: " + enemyHit.collider.name);
            }
        }
    }

    private void OnDrawGizmos()
    {
        var position = TargetCube.m_SelectedTarget == null? FrontPosition : TargetCube.m_SelectedTarget.transform.position;
        var outputSize = TargetCube.m_SelectedTarget == null ? 0.5F : size;
        Gizmos.DrawWireSphere(position, outputSize);
    }
}

public interface IDamagable { }