using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MonoBlock : MonoBehaviour
{
    public enum BlockType
    {
        Blank,
        Core,
        Wood,
        Iron,
        Needle,
        Laser,
        Jumper,
        Bomb,
        MAX
    }
    public enum ElementType
    {
        Neutral,
        Fire,
        Ice,
        Wood,
        MAX
    }

    [SerializeField] protected int Life = 100;
    protected bool IsActivate = false;
    
    protected bool isHit = true;
    public int OwnerPlayerId { get; private set; }
    public int DeckX { get;private set; }
    public int DeckY { get; private set; }
    public int DeckZ { get; private set; }
    public ElementType Element { get; private set; }

    void Awake()
    {
        MonoBlockCache.AddCache(this);
        Setup();
    }

    //こわれる
    protected virtual void Breakdown()
    {
        MonoBlockCache.DelCache(this);
        Destroy(this.gameObject);
    }

    protected virtual void Setup()
    {

    }

    public virtual void Damage(int dmg)
    {
        if (dmg > 0)
        {
            Life -= dmg;
            DamagePopup.Pop(this.gameObject, dmg);
        }
        if (Life < 0)
        {
            Breakdown();
        }
    }

    private void OnTriggerEnter(Collider player)
    {
        if(player.CompareTag("Runner"))
        {
            //拾われた際に何かするなら
        }
    }

    static public MonoBlock Build(BlockType type, int ownerId, int x, int y, int z)
    {
        var prefab = BlockBuilder.GetCache(type);
        var obj = GameObject.Instantiate(prefab);
        var block = obj.GetComponent<MonoBlock>();
        block.OwnerPlayerId = ownerId;
        block.DeckX = x;
        block.DeckY = y;
        block.DeckZ = z;
        return block;
    }
}
