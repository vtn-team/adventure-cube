using Block;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "CubeCreate", menuName = "ObjectCreate_Tasuku/TaskSP", order = 1)]
public class Test : ScriptableObject
{
    [Serializable]
    class CubeAsset
    {
        public int Id;
        public string Key;
        public MonoBlock Block;
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
        if (count == 0) return 0;

        int d = UnityEngine.Random.Range(0, count);
        return CubeAssetList.IndexOf(list.ElementAt(d));
    }
}
