using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Block;


/// <summary>
/// プレイヤーブロック基底
/// </summary>
public class MasterCube : MonoBehaviour
{
    protected List<CubeStock> Blocks = new List<CubeStock>();

    protected MasterCubeParameter Param = new MasterCubeParameter();
    protected List<MonoBlock> Inventory = new List<MonoBlock>();

    public CubeCoordinates _Coord = new CubeCoordinates();
    public CubeCoordinates Coord { get; protected set; }
    
    protected float YOffset = 0.0f;

    public int CharcaterId { get; private set; }
    public int FriendId { get; protected set; }

    public DamageCaster DamageCaster { get; private set; }

    //それぞれのキューブリスト
    public ICoreBlock CoreCube { get; protected set; }
    public List<IAttackBlock> AttackCubes { get; protected set; }
    public List<ISkillBlock> SkillCubes { get; protected set; }
    public List<IShieldBlock> ShieldCubes { get; protected set; }
    public List<IPassiveBlock> PassiveCubes { get; protected set; }
    

    public virtual void Build(int charId)
    {
        //マスターからデッキを取得して、生成すべきキューブを特定
        var deck = GameManager.CharacterDeckMaster.Where(d => d.CharId == charId);

        GameObjectCache.AddCharacterCache(this);

        YOffset = 0;
        Vector3 Center = this.transform.position;
        foreach (var b in deck)
        {
            Vector3 offset = new Vector3(b.OffsetX, b.OffsetY, b.OffsetZ);
            CubeStock stock = new CubeStock();
            stock.SetRoot(this);
            stock.SetUp(b.InitCubeId, offset, (MonoBlock.BlockType)b.Type);

            if (stock.HasCube)
            {
                Inventory.Add(stock.CurrentCube);
            }

            //Y計算
            if (YOffset < b.OffsetY) YOffset = b.OffsetY;

            Blocks.Add(stock);
        }

        _Coord.Top = YOffset;
        YOffset = 0;

        //Y移動
        this.transform.Translate(0, YOffset, 0);

        _Coord.SetPosition(transform.position);

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
        return FriendId == fId;
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
