using UnityEngine;
using System.Collections;
using Block;

/// <summary>
/// 常在効果ブロック
/// 
/// NOTE: パッシブ効果を持つキューブにこのインタフェースをつける
/// </summary>
public interface IPassive
{
    Passive.PassiveType PassiveType { get; }        // タイプを返す実装が必要

    void PassiveEvent(Passive.PassiveEffect evt);   // パッシブ効果を処理する実装が必要
}
