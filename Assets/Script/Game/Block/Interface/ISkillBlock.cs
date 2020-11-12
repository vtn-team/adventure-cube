using UnityEngine;
using System.Collections;

/// <summary>
/// スキルブロック
/// 
/// NOTE: スキル攻撃をするキューブにこのインタフェースをつける
/// </summary>
public interface ISkillBlock
{
    void Skill();        // スキル攻撃用の実装が必要
}
