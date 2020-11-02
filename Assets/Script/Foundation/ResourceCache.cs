using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

//
public enum ResourceType
{
    InvalidType,

    PrefabTypeStart = 10,
    FieldMap,
    Cube,
    Bullet,
    PrefabTypeEnd,

    MaterialTypeStart = 100,
    FieldMaterial,
    CubeMaterial,
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

    CubeSheet CubeSheet = null;
    CacheTemplate<GameObject> PrefabCache = new CacheTemplate<GameObject>();
    CacheTemplate<Material> MaterialCache = new CacheTemplate<Material>();

    static public CubeSheet CubeMaster => Instance.CubeSheet;

    static public void SetupCubeSheet(string name)
    {
        Instance.CubeSheet = Resources.Load<CubeSheet>(Instance.GetPrefabPath(ResourceType.Cube, name));
    }

    static public GameObject GetCube(int id)
    {
        return Instance.PrefabCache.GetCache(Instance.GetPrefabPath(ResourceType.Cube, Instance.CubeSheet.GetAssetKey(id)));
    }

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
                case ResourceType.FieldMap:   Path.AppendFormat("Field/{0}", name); break;
                case ResourceType.Cube:       Path.AppendFormat("Blocks/{0}", name); break;
                case ResourceType.Bullet:     Path.AppendFormat("Bullet/{0}", name); break;
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
                case ResourceType.CubeMaterial:  Path.AppendFormat("Material/Cube/{0}", name); break;
            }
        }
        return Path.ToString();
    }
}
