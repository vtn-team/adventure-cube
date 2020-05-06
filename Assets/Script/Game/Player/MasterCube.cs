using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Block;
using Summon;

public class MasterCube : MonoBehaviour
{
    protected Rigidbody RigidBody;
    protected List<MonoBlock> ChildBlocks = new List<MonoBlock>();
    protected bool PowerOff = false;
    protected IObjectGroup<SummonObject>[] SummonGroup = new IObjectGroup<SummonObject>[(int)SummonObject.SummonType.MAX];

    [SerializeField]
    protected int FriendId = 0;
    //protected UnitStatus Stats = new UnitStatus();

    public DamageCaster TakeDamageCaster { get; private set; }
    public DamageCaster AttackDamageCaster { get; private set; }


    public virtual void Build()
    {
        RigidBody = GetComponent<Rigidbody>();
        
        TakeDamageCaster = new DamageCaster(ChildBlocks);
        AttackDamageCaster = new DamageCaster(ChildBlocks);
    }

    public void CallEvent()
    {

    }

    public bool IsFriend(int friendId)
    {
        return FriendId == friendId;
    }

    public void AddSummonGroup(SummonObject.SummonType type, SummonObject summon)
    {
        if(SummonGroup[(int)type] == null)
        {
            SummonGroup[(int)type] = SummonObject.Build(type);
        }
        SummonGroup[(int)type].Add(summon);
        SummonGroup[(int)type].Replace();
    }
    
    /*
    public void Damage(int dir, int dmg)
    {
        int TargetId = 0;
        TargetId = TakeDamageCaster.CastDamage(dir, ref dmg);
        if (TargetId == -1) return;

        Random.Range(0,26);

    }
    */
}
