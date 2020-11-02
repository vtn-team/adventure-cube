using UnityEngine;
using System.Collections;

/// <summary>
/// 防衛ブロック
/// 
/// NOTE: シールドを持つキューブにこのインタフェースをつける
/// </summary>
public interface IShieldBlock
{
    int Defence(int dmg);  //シールド処理する実装が必要
}
