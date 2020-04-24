using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Block
{
    public class MonoBlockCache
    {
        static MonoBlockCache Instance = new MonoBlockCache();
        Dictionary<int, MonoBlock> Cache = new Dictionary<int, MonoBlock>();
        Dictionary<int, List<MonoBlock>> DeckList = new Dictionary<int, List<MonoBlock>>();

        static public void Setup()
        {
            Instance = new MonoBlockCache();
        }

        static public void SetPlayerDeck(int PlayerId, List<MonoBlock> deck)
        {
            Instance.DeckList.Add(PlayerId, deck);
        }
        static public List<MonoBlock> GetPlayerDeck(int PlayerId)
        {
            return Instance.DeckList[PlayerId];
        }
        static public MonoBlock GetPlayerBlock(int PlayerId, int x, int y)
        {
            if (x + y * 3 >= Instance.DeckList[PlayerId].Count)
            {
                Debug.Log("GetPlayerBlock: null" + x + "/" + y);
                return null;
            }
            return Instance.DeckList[PlayerId][x + y * 3];
        }

        static public void AddCache(MonoBlock mb)
        {
            if (Instance == null) return;

            Instance.Cache.Add(mb.gameObject.GetInstanceID(), mb);
        }
        static public void DelCache(MonoBlock mb)
        {
            if (Instance == null) return;

            Instance.Cache.Remove(mb.gameObject.GetInstanceID());
        }

        static public MonoBlock GetCache(GameObject go)
        {
            if (Instance == null) return null;
            if (!Instance.Cache.ContainsKey(go.GetInstanceID())) return null;
            return Instance.Cache[go.GetInstanceID()];
        }
    }
}