using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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

    [SerializeField] MasterCube Player = null;

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
        TurnCount = 0;
    }

    private void Update()
    {
        switch(Phase)
        {
            case GamePhase.Init:
                Phase = GamePhase.DeckBuild;
                break;

            case GamePhase.DeckBuild:
                Player.Build();
                Phase = GamePhase.GameMain;
                break;
        }
    }


    public static void Death(int playerId)
    {
        Instance.DeathCount++;
        Instance.Result.SetLoser(playerId);
        Instance.DeathCheck();
    }

    void DeathCheck()
    {
        if (DeathCount >= 2)
        {
            Result.gameObject.SetActive(true);
        }
    }
    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}
