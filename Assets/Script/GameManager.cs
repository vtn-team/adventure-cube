using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager
{
    static GameManager Instance = new GameManager();

    List<MonoBlock.BlockType> PlayerDeck = null;
    List<MonoBlock.BlockType> EnemyDeck = null;

    static public void SavePlayerDeck(List<MonoBlock.BlockType> deck)
    {
        Instance.PlayerDeck = deck;
    }

    static public void CreateRandomEnemyDeck()
    {
        Instance.EnemyDeck = new List<MonoBlock.BlockType>();

        int human = Random.Range(0, 15);

        for (int i = 0; i < 15; ++i)
        {
            if (human == i)
            {
                Instance.EnemyDeck.Add(MonoBlock.BlockType.Core);
            }
            else
            {
                Instance.EnemyDeck.Add((MonoBlock.BlockType)Random.Range(2, (int)MonoBlock.BlockType.MAX));
            }
        }
    }

    static public List<MonoBlock.BlockType> GetDeck(int playerId)
    {
        if (playerId == 0) return Instance.PlayerDeck;
        if (playerId == 1) return Instance.EnemyDeck;
        return null;
    }

    static public void Death(int plNo)
    {

    }
}
