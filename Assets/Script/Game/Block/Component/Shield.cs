using UnityEngine;
using System.Collections;

namespace Block
{
    /// <summary>
    /// シールドブロック
    /// 
    /// NOTE: シールドブロックの[F]は盾の回数≒ライフ
    /// </summary>
    public class Shield : MonoBlock, IShieldBlock
    {
        /// <summary>
        /// ダメージ軽減する際の処理
        /// </summary>
        /// <param name="dc"></param>
        public void Defence(DamageCaster dc)
        {

        }
    }
}