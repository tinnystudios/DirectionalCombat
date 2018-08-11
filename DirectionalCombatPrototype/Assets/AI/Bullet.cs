using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody m_RigidBody;
    public Action<PlayerController> OnHit;
    public LayerMask hitMask;
    private float mSize = 0.5F;
    private float mRange = 5;
    private Vector3 mFiredPosition;

    public AnimationCurve xCurve, yCurve;
    public float xForce = 5, yForce = 5;

    private float xT, yT;

    public void Fire(Vector3 dir, float force, float size, float range)
    {
        transform.forward = dir;

        m_RigidBody.velocity = dir * force;
        StartCoroutine(DestroyOverTime(5));
        transform.localScale = Vector3.one * size;
        mSize = size;
        mRange = range;
        mFiredPosition = transform.position;
    }

    void Update()
    {
        xT += Time.deltaTime * 1;
        yT += Time.deltaTime * 1;

        var xOut = Mathf.Lerp(-1, 1, xCurve.Evaluate(xT)) * xForce;
        var yOut = Mathf.Lerp(-1, 1, yCurve.Evaluate(yT)) * yForce;

        transform.position += transform.up * yOut * Time.deltaTime;
        transform.position += transform.right * xOut * Time.deltaTime;

        if (Vector3.Distance(transform.position, mFiredPosition) >= mRange)
        {
            Destroy(gameObject);
        }

        RaycastHit hit;

        if (Physics.SphereCast(transform.position, mSize, transform.forward, out hit, mSize, hitMask))
        {
            //#TODO Deal damage
            Destroy(gameObject);
        }

    }

    IEnumerator DestroyOverTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

}
