using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Block;
using Summon;


/// <summary>
/// プレイヤーブロック基底
/// </summary>
public class MasterCube : MonoBehaviour
{
    [SerializeField] protected List<CubeStock> Blocks = new List<CubeStock>();
    [SerializeField] protected int friendId = 0;

    protected MasterCubeParameter Param = new MasterCubeParameter();
    protected List<MonoBlock> Inventory = new List<MonoBlock>();

    public int FriendId => friendId;

    public DamageCaster TakeDamageCaster { get; private set; }
    public DamageCaster AttackDamageCaster { get; private set; }
    
    public virtual void Build()
    {
        GameObjectCache.AddCharacterCache(this);

        Vector3 Center = this.transform.position;
        foreach (var b in Blocks)
        {
            b.SetRoot(this);
            if (!b.HasCube)
            {
                int id = ResourceCache.CubeMaster.GetRandomCubeId(b.StockType);
                b.Equip(id);
            }
            Inventory.Add(b.CurrentCube);
        }
        
        //キューブ更新したタイミングで呼び出す
        UpdateCube();

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
        var PassiveList = GetComponentsInChildren<IPassive>();
    }

    protected virtual void Death()
    {
        //tbd
        //とりあえず
        LifeCycleManager.RegisterDestroy(this.gameObject);
    }
}
