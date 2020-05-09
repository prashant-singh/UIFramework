using System;
using UnityEngine;
using System.Collections;

public static class EXT
{
    public static Transform GetChildWithName(this Transform targetTransform, string childName)
    {
        try
        {
            return targetTransform.Find(childName);
        }
        catch (System.Exception)
        {
            Debug.LogError("Child not found named " + childName);
            throw;
        }
    }

    public static void DelayThis(this MonoBehaviour mb, float delay, Action callback)
    {
        mb.StartCoroutine(E_Delaying(delay, callback));
    }

    static IEnumerator E_Delaying(float delay, Action callback)
    {
        yield return new WaitForSeconds(delay);
        callback?.Invoke();
    }
}