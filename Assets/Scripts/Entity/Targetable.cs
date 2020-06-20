using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour
{
    protected virtual void OnEnable()
    {
        TargetableSphere targetable = Prefabs.Instantiate<TargetableSphere>();
        targetable.transform.parent = transform;
        targetable.transform.position = transform.position;
        targetable.Parent = gameObject;
    }
}
