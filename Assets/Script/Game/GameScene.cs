using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;

using Block;

public class GameScene : MonoBehaviour
{
    public enum GamePhase {
        Init,
        DeckBuild,
        GameMain,
        Pause,
    }

    [SerializeField] GameCamera Camera = null;
    [SerializeField] ResultUI Result = null;
    [SerializeField] MasterCube PlayableChar = null;
    [SerializeField] List<MasterCube> Enemy = new List<MasterCube>();
    [SerializeField] List<MasterCube> NPC = new List<MasterCube>();

    static GameScene Instance = null;

    int TurnCount = 0;
    GamePhase Phase = GamePhase.Init;

    int DeathCount = 0;
    bool IsBulletTime = false;
    float Timer = 0.0f;
    int NextCount = 0;

    List<FieldEnemy> EnemyList = new List<FieldEnemy>();

    private void Awake()
    {
        Instance = this;
        GameObjectCache.Setup();
        ResourceCache.SetupCubeSheet("CubeMasterTest");
    }

    private void Start()
    {
        var lists = GameObject.FindObjectsOfType<FieldEnemy>();
        foreach (var e in lists)
        {
            Enemy.Add(e);
        }
    }

    private void Update()
    {
        switch(Phase)
        {
            case GamePhase.Init:
                Phase = GamePhase.DeckBuild;
                break;

            case GamePhase.DeckBuild:
                PlayableChar.Build();
                Phase = GamePhase.GameMain;
                break;
        }
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}
