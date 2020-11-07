using UnityEngine;
using System.Collections;
using Block;

/// <summary>
/// 常在効果ブロック
/// 
/// NOTE: パッシブ効果を持つキューブにこのインタフェースをつける
/// </summary>
public interface IPassiveBlock
{
    PassiveType PassiveType { get; }               // タイプを返す実装が必要

    int PassiveEvent(float param, float subparam=0);   // パッシブ効果を処理する実装が必要
}

public enum PassiveType
{
    None,
    DamageBuff,
    IntervalBuff,
}