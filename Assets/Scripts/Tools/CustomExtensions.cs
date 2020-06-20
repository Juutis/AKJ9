using System;
using UnityEngine;

public static class StringExtension
{
    public static string Format(this string str, params System.Object[] args)
    {
        return string.Format(str, args);
    }
}

public static class MonoBehaviourExtension {
    public static Transform FindChildObject (this MonoBehaviour monoBehaviour, string name) {
        foreach(Transform child in monoBehaviour.transform.GetComponentsInChildren<Transform>()) {
            if (child.name == name) {
                return child.gameObject.transform;
            }
        }
        return null;
    }
}