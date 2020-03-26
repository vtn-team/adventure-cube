using UnityEngine;
using System.Collections;

public class Needle : MonoBlock, IAttackable
{
    [SerializeField] public int Atk { private set; get; }

    public void Attack(MonoBlock block)
    {
        if (block == null) return;
        block.Damage(Atk);
    }
}
