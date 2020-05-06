using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectSummoner
{
    static ObjectSummoner Instance = new ObjectSummoner();

    Dictionary<string, GameObject> PrefabCache = new Dictionary<string, GameObject>();

    static public GameObject GetCache(string name)
    {
        if (!Instance.PrefabCache.ContainsKey(name))
        {
            Instance.PrefabCache.Add(name, Resources.Load<GameObject>("Summon/"+name));
        }
        return Instance.PrefabCache[name];
    }
}
