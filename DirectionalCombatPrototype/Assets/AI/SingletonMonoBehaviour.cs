using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T mInstance;
    public static T Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = FindObjectOfType<T>();
            }

            return mInstance;
        }
    }
}
