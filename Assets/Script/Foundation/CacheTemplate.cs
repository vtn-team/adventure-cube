using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CacheTemplate<T> where T : UnityEngine.Object
{
    Dictionary<string, T> Cache = new Dictionary<string, T>();

    public T GetCache(string path)
    {
        if (!Cache.ContainsKey(path))
        {
            Cache.Add(path, Resources.Load<T>(path));
        }
        return Cache[path];
    }
}
