using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using Block;

[CreateAssetMenu(fileName = "CubeSheet", menuName = "ScriptableObjects/CubeSheet", order = 1)]
public class CubeSheet : ScriptableObject
{
    [Serializable]
    public class CubeAsset
    {
        public Language language;
        public int Id;
        public string Key;
        public MonoBlock Block;
        public int Rare;
    }

    [SerializeField] List<CubeAsset> CubeAssetList = new List<CubeAsset>();

    public MonoBlock GetAsset(int id)
    {
        if (id < 0) return null;
        if (id >= CubeAssetList.Count) return null;

        return CubeAssetList[id].Block;
    }

    public string GetAssetKey(int id)
    {
        if (id < 0) return null;
        if (id >= CubeAssetList.Count) return null;

        return CubeAssetList[id].Key;
    }

    public int GetRandomCubeId(MonoBlock.BlockType type)
    {
        //いろいろ重いけどとりあえず我慢
        var list = CubeAssetList.Where(c => c.Block && c.Block.Type == type);
        int count = list.Count();
        if (count == 0) return 0;

        int d = UnityEngine.Random.Range(0, count);
        return CubeAssetList.IndexOf(list.ElementAt(d));
    }

    public int GetRandomCubeIdWithRare(MonoBlock.BlockType type, int rare)
    {
        //いろいろ重いけどとりあえず我慢
        var list = CubeAssetList.Where(c => c.Block && c.Block.Type == type && c.Block.Rare == rare);
        int count = list.Count();
        if(count == 0) return 0;

        int d = UnityEngine.Random.Range(0, count);
        return CubeAssetList.IndexOf(list.ElementAt(d));
    }

#if UNITY_EDITOR
    public void SetData(List<CubeAsset> data)
    {
        CubeAssetList = data;
    }
#endif
}

public enum Language
{
    None = 0,
    JP = 1,
    En = 2,
}
