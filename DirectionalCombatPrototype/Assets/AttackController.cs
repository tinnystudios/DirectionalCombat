using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour, IDamageSender
{
    public bool canCombo = false;
    public int currentCombo = 0;

    public LayerMask m_LayerMask;
    public float range = 1;
    public float size = 1;
    public float height;

    public Vector3 FrontPosition
    {
        get
        {
            return transform.position + (transform.forward * size/2) + (transform.up * height);
        }
    }

    public bool Stunable
    {
        get
        {
            return currentCombo != 0;
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

                var flick = enemyHit.transform.GetComponentInChildren<IFlicker<Color>>();
                if (flick != null) flick.Flicker(Color.red);

                iDamagable.TakeDamage(1, this);
            }
        }
    }

    public void SetComboState(bool state)
    {
        canCombo = state;
    }

    private void OnDrawGizmos()
    {
        var position = TargetCube.m_SelectedTarget == null? FrontPosition : TargetCube.m_SelectedTarget.transform.position;
        var outputSize = TargetCube.m_SelectedTarget == null ? 0.5F : size;
        Gizmos.DrawWireSphere(position, outputSize);
    }
}

public interface IDamagable
{
    void TakeDamage(int i, IDamageSender sender);
}