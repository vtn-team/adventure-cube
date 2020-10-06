using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

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


    /// <summary>
    /// Prefabを取得する
    /// NOTE: キャッシュが無かったら探してきて読み込む
    /// </summary>
    /// <param name="path">アセットやPrefabのパス</param>
    /// <returns>アセットやPrefab</returns>
    public T GetCache(string path)
    {
        if (!Cache.ContainsKey(path))
        {
            Cache.Add(path, Resources.Load<T>(path));
        }
        return Cache[path];
    }
}
