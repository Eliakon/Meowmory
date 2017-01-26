using System;
using System.Collections;
using UnityEngine;

public class ActionAfterDelay
{
    public static IEnumerator DoAfterDelay(float delay, Action callback)
    {
        yield return new WaitForSeconds(delay);
        callback.Invoke();
    }
}
