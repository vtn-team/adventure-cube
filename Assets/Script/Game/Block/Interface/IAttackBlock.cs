using UnityEngine;
using System.Collections;

/// <summary>
/// 攻撃ブロック
/// 
/// NOTE: オート攻撃をするキューブにこのインタフェースをつける
/// </summary>
public interface IAttackBlock
{
    int Interval { get; }           // 攻撃インターバルの実装が必要

    void Attack(DamageCaster dc);   // 攻撃の実装が必要
}
