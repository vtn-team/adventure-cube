using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AssetBundleManager
{
    AssetBundleData AssetBundleData = new AssetBundleData();
    Dictionary<string, string> PathReference = new Dictionary<string, string>();
    Dictionary<string, AssetBundleRefCounter> AssetBundleCache = new Dictionary<string, AssetBundleRefCounter>();

    static StringBuilder PathCombiner = new StringBuilder(256);

    class AssetBundleRefCounter
    {
        public delegate void Callback();

        public AssetBundle Object;
        public int Counter;

        static public AssetBundleRefCounter LoadAsync(string path, Callback cb)
        {
            PathCombiner.Length = 0;
            PathCombiner.AppendFormat("{0}\\AssetBundle\\{1}",Application.dataPath,path);
            var ret = new AssetBundleRefCounter();
            var req = AssetBundle.LoadFromFileAsync(PathCombiner.ToString());
            req.completed += op => {
                ret.Object = req.assetBundle;
                ret.Counter = 1;
                cb();
            };
            return ret;
        }

        static public AssetBundleRefCounter Load(string path)
        {
            PathCombiner.Length = 0;
            PathCombiner.AppendFormat("{0}\\AssetBundle\\{1}", Application.dataPath, path);
            var ret = new AssetBundleRefCounter();
            ret.Object = AssetBundle.LoadFromFile(PathCombiner.ToString());
            ret.Counter = 1;
            return ret;
        }
    }
    //AssetBundle.LoadFromFile(path).)

    static private AssetBundleManager instance = new AssetBundleManager();
    private AssetBundleManager() { }

    static public void Setup()
    {
        var json = Resources.Load("AssetBundleData") as TextAsset;
        instance.AssetBundleData = JsonUtility.FromJson<AssetBundleData>(json.text);

        instance.AssetBundleData.RefPathList.ForEach(p =>
        {
            instance.PathReference.Add(p.AssetPath, p.RefBundleName);
        });
    }

    static public string GetAssetBundlePathFromPath(string path)
    {
        //アセバンは管理名が全部小文字になる
        path = path.ToLower();
        if (!instance.PathReference.ContainsKey(path))
        {
            Debug.LogError("パスが含まれているアセットバンドルは見つかりませんでした。:" + path);
            return null;
        }
        return instance.PathReference[path];
    }

    static public T LoadFromPath<T>(string path) where T : UnityEngine.Object
    {
        string abpath = GetAssetBundlePathFromPath(path);
        if (abpath == null) return default(T);

        var ab = GetAssetBundle(abpath);
        if (ab == null)
        {
            Debug.LogError("アセットバンドルが開けませんでした:" + path);
            return default(T);
        }

        return ab.LoadAsset<T>("assets/resources/"+path);
    }

    static public AssetBundle GetAssetBundle(string abpath)
    {
        if (!instance.AssetBundleCache.ContainsKey(abpath))
        {
            instance.LoadAssetBundle(abpath);
        }

        return instance.AssetBundleCache[abpath].Object;
    }

    private void LoadAssetBundle(string abpath)
    {
        Debug.Log("LoadAssetBundle:" + abpath);
        AssetBundleData.BundleList.ForEach(ab =>
        {
            if (ab.AssetName == abpath)
            {
                ab.Dependencies.ForEach(dp => LoadAssetBundle(dp));
            }
        });
        if (!AssetBundleCache.ContainsKey(abpath))
        {
            AssetBundleCache.Add(abpath, AssetBundleRefCounter.Load(abpath));
        }
        else
        {
            AssetBundleCache[abpath].Counter++;
        }
    }

    private void DeleteAssetBundle(string abpath)
    {
        Debug.Log("DeleteAssetBundle:" + abpath);
        AssetBundleData.BundleList.ForEach(ab =>
        {
            if (ab.AssetName == abpath)
            {
                ab.Dependencies.ForEach(dp => DeleteAssetBundle(dp));
            }
        });

        if (!AssetBundleCache.ContainsKey(abpath))
        {
            return;
        }
        else
        {
            AssetBundleCache[abpath].Counter--;
            if(AssetBundleCache[abpath].Counter <= 0)
            {
                AssetBundleCache[abpath].Object.Unload(false);
                AssetBundleCache.Remove(abpath);
            }
        }
    }
}
