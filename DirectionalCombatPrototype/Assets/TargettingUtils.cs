using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class TargettingUtils
{

    public static T GetNearestTarget<T>(Transform rootTransform) where T : MonoBehaviour
    {
        var list = GameObject.FindObjectsOfType<T>().ToList();
        return GetNearestTarget(rootTransform, list);
    }

    public static T GetNearestTarget<T>(Transform rootTransform, List<T> list) where T : MonoBehaviour
    {
        var nClosest = list.OrderBy(t => (t.transform.position - rootTransform.position).sqrMagnitude)
                                   .ToArray().FirstOrDefault();

        return nClosest;
    }

    public static T[] GetNearestTarget<T>(Transform rootTransform, int amount) where T : MonoBehaviour
    {
        var list = GameObject.FindObjectsOfType<T>().ToList();
        return GetNearestTarget(rootTransform, list, amount);
    }

    public static T[] GetNearestTarget<T>(Transform rootTransform, List<T> list, int amount) where T : MonoBehaviour
    {
        var nClosest = list.OrderBy(t => (t.transform.position - rootTransform.position).sqrMagnitude)
                                   .Take(amount)
                                   .ToArray();

        return nClosest;
    }

}