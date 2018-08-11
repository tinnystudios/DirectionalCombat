using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explodeTime = 5;
    public float size = 10;
    public Transform m_EnergyParticle;
    public LayerMask hitMask;

    //Should blow up on contact?
    IEnumerator Start()
    {
        var timeLapsed = 0.0F;

        while (timeLapsed < explodeTime)
        {
            timeLapsed += Time.deltaTime;

            var scaleA = Vector3.zero;
            var scaleB = Vector3.one * size;
            var t = timeLapsed / explodeTime;

            m_EnergyParticle.localScale = Vector3.Lerp(scaleA, scaleB, t);

            yield return null;
        }

        yield return new WaitForSeconds(0.5F);

        //Check attack
        var hits = Physics.SphereCastAll(transform.position, size * 0.5F, transform.right, size * 0.5F,hitMask);
        
        foreach (var hit in hits)
        {
            var status = hit.transform.GetComponent<Status>();
            if (status != null)
            {
                status.TakeDamage(3);
                Destroy(gameObject);
            }
        }

        yield return null;

        Destroy(gameObject);
    }

}
