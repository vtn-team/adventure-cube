using UnityEngine;
using System.Collections;

public interface IAttackable
{
    int Atk { get; }

    //衝突時にお互いに発生する挙動
    void Attack(MonoBlock target);
}
