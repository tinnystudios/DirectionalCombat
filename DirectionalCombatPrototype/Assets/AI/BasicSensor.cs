using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSensor : MonoBehaviour
{
    public Transform m_Target;
    public float range = 10;

    private bool mSeenPlayer = false;
    private bool mInRange = false;

    private float mStartTime = 0;

    public bool SeenPlayer
    {
        get { return mSeenPlayer; }
    }

    public void ProcessSensor()
    {
        //Other AI Detection
        var otherAIs = TargettingUtils.GetNearestTarget<BasicAI>(transform,3);

        foreach (var ai in otherAIs)
        {
            if (ai.gameObject == gameObject)
                continue;

            //Notify
            //Check their state
            //Anything
            //These are seperate components
        }


        //Player detection
        var dist = Vector3.Distance(transform.position, m_Target.position);
        if (dist <= range)
        {
            if (mInRange == false)
            {
                mStartTime = Time.time;
                mInRange = true;
            }

            mSeenPlayer = true;
        }
        else
        {
            mInRange = false;

            //Player out of range for 5 seconds
            if (Time.time - mStartTime >= 5)
            {
                //Let's forget him
                mSeenPlayer = false;
            }
        }
    }
}
