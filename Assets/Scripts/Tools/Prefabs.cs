using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefabs: MonoBehaviour
{

    private static Dictionary<string, GameObject> cachedPrefabs = new Dictionary<string, GameObject>();

    public static T Instantiate<T>() {
        return Object.Instantiate(Get<T>(), Vector2.zero, Quaternion.identity).GetComponent<T>();
    }

    public static GameObject Get<T>() {
        return GetPrefab(typeof(T).Name);
    }

    private static GameObject GetPrefab(string name) {
        GameObject prefab;
        if (cachedPrefabs.ContainsKey(name)) {
            prefab = cachedPrefabs[name];
        } else {
            prefab = Load(name);
            cachedPrefabs[name] = prefab;
        }
        return prefab;
    }

    private static GameObject Load(string path) {
        UnityEngine.Object loadedObject = Resources.Load("Prefabs/{0}".Format(path));
        if (loadedObject == null) {
            Debug.Log("Couldn't find Prefab at Prefabs/{0}!".Format(path));
            return null;
        }
        return loadedObject as GameObject;
    }

}
