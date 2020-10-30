using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Block;

/// <summary>
/// ゲーム管理クラス
/// 
/// NOTE: 
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] MasterCube PlayableChar = null;
    [SerializeField] List<MasterCube> Enemy = new List<MasterCube>();
    [SerializeField] List<MasterCube> NPC = new List<MasterCube>();

    static GameManager Instance = new GameManager();

    private void Awake()
    {
        Instance = this;
        GameObjectCache.Setup();
        ResourceCache.SetupCubeSheet("CubeMasterTest");
    }

    private void Start()
    {
        PlayableChar.Build();

        var lists = GameObject.FindObjectsOfType<FieldEnemy>();
        foreach (var e in lists)
        {
            Enemy.Add(e);
        }
    }

    static public MasterCube GetPlayableChar()
    {
        return Instance.PlayableChar;
    }

    static public List<MasterCube> GetEnemyList()
    {
        return Instance.Enemy;
    }
}
