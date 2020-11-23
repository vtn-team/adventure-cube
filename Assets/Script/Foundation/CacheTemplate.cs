using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// UnityのPrefabをResourcesから読み込んでキャッシュしてくれるやつ
/// 
/// NOTE: メタプログラミング例を使ってるので、特定の型のみ保持します
/// NOTE: AssetBundleを使用する際は、このクラスを拡張して対応するのが楽
/// </summary>
/// <typeparam name="T"></typeparam>
public class CacheTemplate<T> where T : UnityEngine.Object
{
    Dictionary<string, T> Cache = new Dictionary<string, T>(); //キャッシュ辞書


    void CreateCache(string path)
    {
        switch(ResourceCache.Type)
        {
        case ResourceCache.CacheType.Resources:
            Cache.Add(path, Resources.Load<T>(path));
            break;

#if UNITY_EDITOR
        case ResourceCache.CacheType.AssetDatabase:
            Cache.Add(path, AssetDatabase.LoadAssetAtPath<T>(path));
            break;
#endif
        case ResourceCache.CacheType.AssetBundleLocal:
            //Cache.Add(path, RefCounter.Build();
            break;
        }
    }

    /// <summary>
    /// キャッシュを読み込む
    /// </summary>
    public void LoadCache(string path)
    {
        if (!Cache.ContainsKey(path))
        {
            CreateCache(path);
        }
    }

    /// <summary>
    /// キャッシュを捨てる
    /// </summary>
    public void DeleteCache(string path)
    {
        if (Cache.ContainsKey(path))
        {
            DeleteCache(path);
        }
    }

    /// <summary>
    /// Prefabを取得する
    /// NOTE: キャッシュが無かったら探してきて読み込む
    /// </summary>
    /// <param name="path">アセットやPrefabのパス</param>
    /// <returns>アセットやPrefab</returns>
    public T GetCache(string path)
    {
        LoadCache(path);
        return Cache[path];
    }
}