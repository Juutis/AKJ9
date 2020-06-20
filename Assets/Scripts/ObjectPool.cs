using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private int poolSize = 1000;
    [SerializeField]
    private bool strictPoolSize = true;
    [SerializeField]
    private GameObject prefab;

    private List<GameObject> objects;
    private List<GameObject> activeObjects;
    private List<GameObject> inactiveObjects;

    // Start is called before the first frame update
    void Start()
    {
        objects = new List<GameObject>();
        activeObjects = new List<GameObject>();
        inactiveObjects = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            obj.transform.parent = transform;
            objects.Add(obj);
            inactiveObjects.Add(obj);
            obj.transform.position = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
