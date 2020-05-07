using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Block
{
    public class GameObjectCache
    {
        static GameObjectCache Instance = new GameObjectCache();

        Dictionary<int, MonoBlock> MonoBlockCache = new Dictionary<int, MonoBlock>();
        Dictionary<int, MasterCube> CharacterCache = new Dictionary<int, MasterCube>();


        static public void Setup()
        {
            Instance = new GameObjectCache();
        }
        

        static public void AddMonoBlockCache(MonoBlock mb)
        {
            if (Instance == null) return;

            Instance.MonoBlockCache.Add(mb.gameObject.GetInstanceID(), mb);
        }
        static public void DelMonoBlockCache(MonoBlock mb)
        {
            if (Instance == null) return;

            Instance.MonoBlockCache.Remove(mb.gameObject.GetInstanceID());
        }
        static public MonoBlock GetMonoBlock(GameObject go)
        {
            if (Instance == null) return null;
            if (!Instance.MonoBlockCache.ContainsKey(go.GetInstanceID())) return null;
            return Instance.MonoBlockCache[go.GetInstanceID()];
        }


        static public void AddCharacterCache(MasterCube mc)
        {
            if (Instance == null) return;

            Instance.CharacterCache.Add(mc.gameObject.GetInstanceID(), mc);
        }
        static public void DelCharacterCache(MasterCube mc)
        {
            if (Instance == null) return;

            Instance.CharacterCache.Remove(mc.gameObject.GetInstanceID());
        }
        static public MasterCube GetCharacter(GameObject go)
        {
            if (Instance == null) return null;
            if (!Instance.CharacterCache.ContainsKey(go.GetInstanceID())) return null;
            return Instance.CharacterCache[go.GetInstanceID()];
        }
    }
}