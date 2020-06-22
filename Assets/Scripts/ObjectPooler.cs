using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField]
    private List<PoolConf> poolConfigs;
    private static Dictionary<GameObject, Pool> typedPools;

    // Start is called before the first frame update
    void Start()
    {
        typedPools = new Dictionary<GameObject, Pool>();
        foreach (PoolConf conf in poolConfigs)
        {
            Pool pool = new Pool(conf.prefab, transform, conf.poolSize, conf.strictPoolSize);
            typedPools.Add(conf.prefab, pool);
        }
    }

    public static Pool GetPool(GameObject prefab)
    {
        if (!typedPools.ContainsKey(prefab)) return null;

        return typedPools[prefab];
    }

    // Update is called once per frame
    void Update()
    {

    }
}

[Serializable]
public class PoolConf
{
    public GameObject prefab;
    public int poolSize;
    public bool strictPoolSize;
}

public class Pool
{
    private GameObject prefab;
    private List<GameObject> objects;
    private List<GameObject> activeObjects;
    private List<GameObject> inactiveObjects;
    private int size;
    private bool strict;

    public Pool(GameObject prefab, Transform parent, int size = 100, bool strict = true)
    {
        this.strict = strict;
        this.size = size;
        objects = new List<GameObject>();
        activeObjects = new List<GameObject>();
        inactiveObjects = new List<GameObject>();

        for (int i = 0; i < size; i++)
        {
            GameObject obj = GameObject.Instantiate(prefab, new Vector3(0, 1000f, 0), Quaternion.identity);
            obj.SetActive(false);
            obj.transform.parent = parent;
            obj.AddComponent<Pooled>();
            objects.Add(obj);
            inactiveObjects.Add(obj);
        }
    }

    public void DeactivateObject(GameObject obj)
    {
        if (objects.Contains(obj))
        {
            obj.SetActive(false);
            obj.transform.position = Vector3.zero;
            activeObjects.Remove(obj);
            inactiveObjects.Add(obj);
        }
        else
        {
            GameObject.Destroy(obj);
        }
    }

    public GameObject ActivateObject(Vector3 position, bool activate = true)
    {
        GameObject obj;

        if (inactiveObjects.Count > 0)
        {
            obj = inactiveObjects[0];
            inactiveObjects.RemoveAt(0);
        }
        else if (!strict)
        {
            obj = GameObject.Instantiate(prefab);
            objects.Add(obj);
        }
        else
        {
            return null;
        }

        activeObjects.Add(obj);
        obj.transform.position = position;
        obj.SetActive(activate);

        return obj;
    }
}
