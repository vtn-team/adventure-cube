using UnityEngine;
using System.Collections;

/// <summary>
/// 防衛ブロック
/// 
/// NOTE: シールドを持つキューブにこのインタフェースをつける
/// </summary>
public interface IShieldBlock
{
    void Defence(DamageCaster dc);  //シールド処理する実装が必要
}
