using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Block
{
    public class BlockBuilder
    {
        static BlockBuilder Instance = new BlockBuilder();

        Dictionary<MonoBlock.BlockType, GameObject> PrefabCache = new Dictionary<MonoBlock.BlockType, GameObject>();

        static public GameObject GetCache(MonoBlock.BlockType type)
        {
            if (!Instance.PrefabCache.ContainsKey(type))
            {
                Instance.PrefabCache.Add(type, Resources.Load<GameObject>(GetPath(type)));
            }
            return Instance.PrefabCache[type];
        }

        static string GetPath(MonoBlock.BlockType type)
        {
            return "Blocks/" + type.ToString();
        }
    }
}