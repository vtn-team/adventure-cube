using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Block
{
    /// <summary>
    /// 攻撃力をあげる
    /// </summary>
    public class AttackPlus : MonoBlock, IPassiveBlock
    {
        public PassiveType PassiveType => PassiveType.DamageBuff;      // タイプを返す実装が必要

        // パッシブ効果を処理する実装が必要
        public int PassiveEvent(int param, int subparam)
        {
            return figure;
        }
    }
}
