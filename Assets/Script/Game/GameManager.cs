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
    //ゲーム中のオブジェクトデータ
    [SerializeField] MasterCube PlayableChar = null;
    [SerializeField] List<MasterCube> Enemy = new List<MasterCube>();
    [SerializeField] List<MasterCube> NPC = new List<MasterCube>();

    //ゲームデータ


    static GameManager Instance = new GameManager();

    private void Awake()
    {
        Instance = this;
        GameObjectCache.Setup();
        ResourceCache.SetupCubeSheet("CubeMasterTest");

        Network.WebRequest.Request<Network.WebRequest.GetDynamic>("https://script.google.com/macros/s/AKfycbyc6WmX57vj8_V5tRL7eN4QCWMcLUQx8Jtu_B_JyqnMRGxH0Uk/exec?sheet=Cube", Network.WebRequest.ResultType.Json, (dynamic json) =>
        {
            Debug.Log(json["data"][0]["Id"]);
        });
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
