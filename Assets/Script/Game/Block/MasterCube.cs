﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Block;


/// <summary>
/// プレイヤーブロック基底
/// </summary>
public class MasterCube : MonoBehaviour
{
    [SerializeField] protected List<CubeStock> Blocks = new List<CubeStock>();
    [SerializeField] protected int friendId = 0;

    protected MasterCubeParameter Param = new MasterCubeParameter();
    protected List<MonoBlock> Inventory = new List<MonoBlock>();

    public CubeCoordinates _Coord = new CubeCoordinates();
    public CubeCoordinates Coord { get; protected set; }
    
    protected float YOffset = 0.0f;

    public int FriendId => friendId;

    public DamageCaster DamageCaster { get; private set; }

    //それぞれのキューブリスト
    public ICoreBlock CoreCube { get; protected set; }
    public List<IAttackBlock> AttackCubes { get; protected set; }
    public List<ISkillBlock> SkillCubes { get; protected set; }
    public List<IShieldBlock> ShieldCubes { get; protected set; }
    public List<IPassiveBlock> PassiveCubes { get; protected set; }

    public void CreateDeck(int charId)
    {
        GameManager.GetDeckFromCharId();
    }

    public virtual void Build()
    {
        GameObjectCache.AddCharacterCache(this);

        YOffset = 0;
        Vector3 Center = this.transform.position;
        foreach (var b in Blocks)
        {
            b.SetRoot(this);
            b.SetUpInitCube(); //教材用実装変更
            /*
            if (!b.HasCube)
            {
                int id = ResourceCache.CubeMaster.GetRandomCubeId(b.StockType);
                b.Equip(id);
            }
            */
            Inventory.Add(b.CurrentCube);

            //Y計算
            if (YOffset < b.CurrentCube.transform.position.y) YOffset = b.CurrentCube.transform.position.y;
        }

        //Y移動
        this.transform.Translate(0, YOffset, 0);

        _Coord.SetPosition(transform.position);
        _Coord.Top = YOffset;

        //キューブ更新したタイミングで呼び出す
        UpdateCube();

        //コア設定
        CoreCube = GetComponentInChildren<ICoreBlock>();
        if(CoreCube==null)
        {
            Debug.LogError("コアが設定されていないブロックです。破棄します。");
            Death();
        }

        DamageCaster = new DamageCaster(this,Param);
    }

    public void CallEvent()
    {

    }

    public bool IsFriend(int fId)
    {
        return friendId == fId;
    }
    
    public void UpdateDamage(DamageCaster.DamageSet dmg)
    {
        foreach(var b in Blocks)
        {
            if (!b.IsAlive())
            {
                b.BreakDown();
                Debug.Log("breakdown");
            }
        }
    }

    protected virtual void UpdateCube()
    {
        //Linqを使った検索(冗長)
        //PassiveCubes = Inventory.Select(c => c as IPassive).Where(c => c as IPassive != null).ToList();

        //便利なGetComponentの使い方
        PassiveCubes = GetComponentsInChildren<IPassiveBlock>().ToList();
        AttackCubes = GetComponentsInChildren<IAttackBlock>().ToList();
        SkillCubes = GetComponentsInChildren<ISkillBlock>().ToList();
        ShieldCubes = GetComponentsInChildren<IShieldBlock>().ToList();
    }

    protected virtual void Death()
    {
        //tbd
        //とりあえず
        LifeCycleManager.RegisterDestroy(this.gameObject);
    }
}
