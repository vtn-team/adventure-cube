using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Block;

/// <summary>
/// ゲーム管理クラス
/// 
/// NOTE: 
/// </summary>
public class GameManager
{
    static GameManager Instance = new GameManager();

    List<MonoBlock.BlockType> PlayerDeck = null;
    List<MonoBlock.BlockType> EnemyDeck = null;

    static public void SavePlayerDeck(List<MonoBlock.BlockType> deck)
    {
        Instance.PlayerDeck = deck;
    }

    //ランダムで敵を作る
    static public void CreateRandomEnemyDeck()
    {
        Instance.EnemyDeck = new List<MonoBlock.BlockType>();

        int human = Random.Range(0, 15);
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
