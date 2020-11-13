using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Block;

public partial class GameManager
{
    public enum GameState
    {
        Init,
        Build,
        InGame,
    }
    public GameState CurrentState { get; private set; }
    public InputObserver InputObs { get; private set; }

    void UnityUpdate()
    {
        switch(CurrentState)
        {
        case GameState.Init: Init(); break;
        case GameState.Build: Build(); break;
        case GameState.InGame: InGame(); break;
        }
    }

    void Init()
    {
        //マスタを読んでいる間は何もしない
        if (LoadingCount > 0) return;

        //キューブキャッシュを用意する
        ResourceCache.SetupCubeCache(CubeMaster);

        //キャラデータを設定していく
        var gameObject = new GameObject("Player");
        gameObject.layer = LayerMask.NameToLayer("Player"); //
        PlayableChar = gameObject.AddComponent<Player>();
        var plList = CharacterMaster.Where(d => d.IsPlayable==1 && d.FriendId == 1);
        var playable = plList.ElementAt(Random.Range(0,plList.Count()));
        PlayableChar.Build(playable.Id);

        //処理終わり
        CurrentState = GameState.Build;
    }

    void Build()
    {
        //敵適当に作る
        var enList = CharacterMaster.Where(d => d.FriendId==2);
        for (int i = 0; i < 3; ++i)
        {
            var gameObject_enemy = new GameObject("Enemy");
            FieldEnemy enemy = gameObject_enemy.AddComponent<FieldEnemy>();
            var selected = enList.ElementAt(Random.Range(0, enList.Count()));
            enemy.Build(selected.Id);
            enemy.transform.position = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
            Enemy.Add(enemy);
        }

        //処理終わり
        CurrentState = GameState.InGame;
    }

    string[] ButtonLabels = new string[]{ "Fire1", "Fire2" };
    void InGame()
    {
        foreach (var label in ButtonLabels)
        {
            if (Input.GetButtonDown(label))
            {
                InputObs.NotifyObserver(InputObserver.CreateInput(label));
            }
        }
    }
}
