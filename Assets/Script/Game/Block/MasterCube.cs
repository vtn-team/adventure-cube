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
    [SerializeField] protected List<CubeStock> Blocks = new List<CubeStock>();
    [SerializeField] protected int friendId = 0;

    protected MasterCubeParameter Param = new MasterCubeParameter();
    protected List<MonoBlock> Inventory = new List<MonoBlock>();

    public CubeCoordinates Coord { get; private set; }
    protected float YOffset = 0.0f;

    public int FriendId => friendId;

    public DamageCaster TakeDamageCaster { get; private set; }
    public DamageCaster AttackDamageCaster { get; private set; }

    protected ICoreBlock CoreCube = null;
    
    public virtual void Build()
    {
        GameObjectCache.AddCharacterCache(this);

        YOffset = 0;
        Coord.SetPosition(transform.position);
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

        //キューブ更新したタイミングで呼び出す
        UpdateCube();

        //コア設定
        CoreCube = GetComponentInChildren<ICoreBlock>();
        if(CoreCube==null)
        {
            Debug.LogError("コアが設定されていないブロックです。破棄します。");
            Death();
        }

        TakeDamageCaster = new DamageCaster(Param);
        AttackDamageCaster = new DamageCaster(Param);
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
        UpdatePassiveCube();
    }

    protected void UpdatePassiveCube()
    {
        //Linqを使った検索
        //Inventory.Select(c => c as IPassive).Where(c => c as IPassive != null).ToList();

        //便利なGetComponentの使い方
        var PassiveList = GetComponentsInChildren<IPassiveBlock>();
    }

    protected void UpdateAttackCube()
    {
        var AttackList = GetComponentsInChildren<IAttackBlock>();
    }

    protected virtual void Death()
    {
        //tbd
        //とりあえず
        LifeCycleManager.RegisterDestroy(this.gameObject);
    }
}
