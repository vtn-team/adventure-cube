﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

//
public enum ResourceType
{
    InvalidType,

    PrefabTypeStart = 10,
    FieldMap,
    MonoBlock,
    SummonObject,
    PrefabTypeEnd,

    MaterialTypeStart = 100,
    FieldMaterial,
    MaterialTypeEnd,
}

/// <summary>
/// 
/// </summary>
public class ResourceCache
{
    bool AssetBundleSwitch = false;
    StringBuilder Path = new StringBuilder(256);
    static ResourceCache Instance = new ResourceCache();

    CacheTemplate<GameObject> PrefabCache = new CacheTemplate<GameObject>();
    CacheTemplate<Material> MaterialCache = new CacheTemplate<Material>();

    static public GameObject GetCache(ResourceType type,  string name)
    {
        if(type < ResourceType.PrefabTypeStart || type >= ResourceType.PrefabTypeEnd)
        {
            Debug.Log("不正な呼び出し");
            return null;
        }
        return Instance.PrefabCache.GetCache(Instance.GetPrefabPath(type, name));
    }

    static public Material GetMaterialCache(ResourceType type, string name)
    {
        if (type < ResourceType.MaterialTypeStart || type >= ResourceType.MaterialTypeEnd)
        {
            Debug.Log("不正な呼び出し");
            return null;
        }
        return Instance.MaterialCache.GetCache(Instance.GetMaterialPath(type, name));
    }

    string GetPrefabPath(ResourceType type, string name)
    {
        Path.Length = 0;
        if (AssetBundleSwitch)
        {
            //tbd
        }
        else
        {
            switch(type)
            {
                case ResourceType.FieldMap:     Path.AppendFormat("Field/{0}", name); break;
                case ResourceType.MonoBlock:    Path.AppendFormat("Blocks/{0}", name); break;
                case ResourceType.SummonObject: Path.AppendFormat("Summon/{0}", name); break;
            }
        }
        return Path.ToString();
    }

    string GetMaterialPath(ResourceType type, string name)
    {
        Path.Length = 0;
        if (AssetBundleSwitch)
        {
            //tbd
        }
        else
        {
            switch (type)
            {
                case ResourceType.FieldMaterial: Path.AppendFormat("Material/Field/{0}", name); break;
            }
        }
        return Path.ToString();
    }
}
