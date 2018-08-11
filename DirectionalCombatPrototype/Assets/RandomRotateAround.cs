using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotateAround : MonoBehaviour
{
    public float dist = 1.0F;

    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            transform.localPosition = Random.onUnitSphere * dist;
        }
    }
}
