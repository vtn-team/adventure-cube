using UnityEngine;
using System.Collections;

/// <summary>
/// スキルブロック
/// 
/// NOTE: スキル攻撃をするキューブにこのインタフェースをつける
/// </summary>
public interface ISkillBlock
{
    int Cooltime { get; }           // クールタイムの実装が必要
    void Skill(DamageCaster dc);    // スキル攻撃用の実装が必要
}
