using UnityEngine;
using System.Collections;

public class Attacker : MonoBehaviour
{
    public delegate void AttackCollisionCollback(MonoBlock obj);

    [SerializeField] protected int Atk = 0;
    [SerializeField] protected MonoBlock OwnerBlock = null;

    //public AttackCollisionCollback ColEnterCallback = null;
    //public AttackCollisionCollback ColExitCallback = null;

    public void Setup(MonoBlock owner, int atk)
    {
        OwnerBlock = owner;
        Atk = atk;
    }

    public virtual void Attack(MonoBlock block)
    {
        if (block == null) return;

        block.Damage(Atk);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (ColEnterCallback == null) return;
        var Obj = MonoBlockCache.GetCache(collision.gameObject);
        if (Obj == OwnerBlock) return;
        if (Obj)
        {
            Attack(Obj);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        //if (ColEnterCallback == null) return;
        var Obj = MonoBlockCache.GetCache(col.gameObject);
        if (Obj == OwnerBlock) return;
        if (Obj)
        {
            Attack(Obj);
        }
    }

    /*
    private void OnCollisionExit(Collision collision)
    {
        //if (ColExitCallback == null) return;
        var Obj = MonoBlockCache.GetCache(collision.gameObject);
        if (Obj)
        {
            Attack(Obj);
        }
    }
    */
}
