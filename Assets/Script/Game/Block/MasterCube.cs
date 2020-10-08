using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Block;
using Summon;


/// <summary>
/// プレイヤーブロック基底
/// </summary>
public class MasterCube : MonoBehaviour
{
    [SerializeField] protected List<CubeStock> Blocks = new List<CubeStock>();
    [SerializeField] protected int friendId = 0;
    
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
            int id = ResourceCache.CubeMaster.GetRandomCubeId(b.StockType);
            var block = b.Build(id, this.transform);
        }

        TakeDamageCaster = new DamageCaster(Inventory);
        AttackDamageCaster = new DamageCaster(Inventory);
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
    
    protected virtual void Death()
    {
        //tbd
        //とりあえず
        LifeCycleManager.RegisterDestroy(this.gameObject);
    }
}
