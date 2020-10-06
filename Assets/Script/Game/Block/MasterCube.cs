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
    protected List<MonoBlock> ChildBlocks = new List<MonoBlock>();
    protected List<MonoBlock> RemoveList = new List<MonoBlock>();
    protected bool PowerOff = false;
    protected IObjectGroup<SummonObject>[] SummonGroup = new IObjectGroup<SummonObject>[(int)SummonObject.SummonType.MAX];

    [SerializeField]
    protected int friendId = 0;
    //protected UnitStatus Stats = new UnitStatus();

    public int FriendId => friendId;
    public DamageCaster TakeDamageCaster { get; private set; }
    public DamageCaster AttackDamageCaster { get; private set; }


    public virtual void Build()
    {
        GameObjectCache.AddCharacterCache(this);

        TakeDamageCaster = new DamageCaster(ChildBlocks);
        AttackDamageCaster = new DamageCaster(ChildBlocks);
    }

    public void CallEvent()
    {

    }

    public bool IsFriend(int fId)
    {
        return friendId == fId;
    }

    public void AddSummonGroup(SummonObject.SummonType type, SummonObject summon)
    {
        if(SummonGroup[(int)type] == null)
        {
            SummonGroup[(int)type] = SummonObject.Build(type, this);
        }
        SummonGroup[(int)type].Add(summon);
        SummonGroup[(int)type].Replace();
    }
    
    public void UpdateDamage(DamageCaster.DamageSet dmg)
    {
        RemoveList.Clear();

        bool isAliveCore = false;
        foreach(var b in ChildBlocks)
        {
            if (b.IsAlive())
            {
                if (b.Type == MonoBlock.BlockType.Core) isAliveCore = true;
                continue;
            }

            b.BreakDown();
            Debug.Log("breakdown");
            RemoveList.Add(b);
        }

        //コアが全部消えたら死
        if(!isAliveCore)
        {
            Death();
        }

        RemoveList.ForEach(rm => ChildBlocks.Remove(rm));
    }
    
    protected virtual void Death()
    {
        //tbd
        //とりあえず
        LifeCycleManager.RegisterDestroy(this.gameObject);
    }
}
