using UnityEngine;
using System.Collections;
using Block;

/// <summary>
/// コアブロック
/// 
/// NOTE: コアなのでHPとかのパラメータが必要
/// </summary>
public interface ICoreBlock
{
    int Life { get; }   //ライフが必要
    bool IsBreakdown(); //破壊されたかどうかの判定が必要
}
