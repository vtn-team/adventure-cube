using UnityEngine;
using System.Collections;

/// <summary>
/// 攻撃ブロック
/// 
/// NOTE: オート攻撃をするキューブにこのインタフェースをつける
/// </summary>
public interface IAttackBlock
{
    bool CanIAttack { get; }    // 攻撃インターバル、または攻撃可能を知らせる実装が必要

    void Attack();              // 攻撃の実装が必要
}
