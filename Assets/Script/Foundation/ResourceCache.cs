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
    UI,
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
    public enum CacheType
    {
        Resources,          //Resourcesを参照します
        AssetDatabase,      //AssetDatabaseを参照します(Editorのみ)
        AssetBundleLocal,   //ローカルビルドしたアセットバンドルを参照します
        AssetBundleRelease, //アセットバンドルは、リリース時と同様の参照方法を取ります
    }
    static public CacheType Type => Instance._type;

    StringBuilder Path = new StringBuilder(256);
    static ResourceCache Instance = new ResourceCache();

    CacheType _type = CacheType.Resources;

    CacheTemplate<GameObject> PrefabCache = new CacheTemplate<GameObject>();
    CacheTemplate<Material> MaterialCache = new CacheTemplate<Material>();

    Dictionary<int, MasterData.Cube> CubeMasterCache = new Dictionary<int, MasterData.Cube>();

    /*
    CubeSheet CubeSheet = null;
    static public CubeSheet CubeMaster => Instance.CubeSheet;
    static public void SetupCubeSheet(string name)
    {
        Instance.CubeSheet = Resources.Load<CubeSheet>(Instance.GetPrefabPath(ResourceType.Cube, name));
    }
    */

    static public void SetupResourceManager(CacheType type)
    {
        Instance._type = type;
        switch(Type)
        {
            case CacheType.Resources:
            case CacheType.AssetDatabase:
                break;

            case CacheType.AssetBundleLocal:
            case CacheType.AssetBundleRelease:
                {
                    AssetBundleManager.Setup();
                }
                break;
        }
    }

    static public void SetupCubeCache(MasterData.Cube[] master)
    {
        Instance.CubeMasterCache.Clear();
        foreach (var d in master)
        {
            Instance.CubeMasterCache.Add(d.Id, d);
        }
    }

    static public GameObject GetCube(int id)
    {
        if(!Instance.CubeMasterCache.ContainsKey(id))
        {
            Debug.LogError(id + "のキューブはデータがありません。");
            return null;
        }
        return Instance.PrefabCache.GetCache(Instance.GetPrefabPath(ResourceType.Cube, Instance.CubeMasterCache[id].Prefab));
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
        string ext = "";
        switch(Type)
        {
            case CacheType.Resources: ext = ""; break;
            case CacheType.AssetDatabase: ext = ""; break;
            case CacheType.AssetBundleLocal: ext = ".prefab"; break;
            case CacheType.AssetBundleRelease: ext = ".prefab"; break;
        }
        switch (type)
        {
            case ResourceType.FieldMap: Path.AppendFormat("Field/{0}{1}", name, ext); break;
            case ResourceType.Cube: Path.AppendFormat("Blocks/{0}{1}", name, ext); break;
            case ResourceType.Bullet: Path.AppendFormat("Bullet/{0}{1}", name, ext); break;
            case ResourceType.UI: Path.AppendFormat("UI/{0}{1}", name, ext); break;
        }
        return Path.ToString();
    }

    string GetMaterialPath(ResourceType type, string name)
    {
        Path.Length = 0;
        string ext = "";
        switch (Type)
        {
            case CacheType.Resources: ext = ""; break;
            case CacheType.AssetDatabase: ext = ""; break;
            case CacheType.AssetBundleLocal: ext = ".mat"; break;
            case CacheType.AssetBundleRelease: ext = ".mat"; break;
        }
        switch (type)
        {
            case ResourceType.FieldMaterial: Path.AppendFormat("Material/Field/{0}{1}", name, ext); break;
            case ResourceType.CubeMaterial: Path.AppendFormat("Material/Cube/{0}{1}", name, ext); break;
        }
        return Path.ToString();
    }
}
