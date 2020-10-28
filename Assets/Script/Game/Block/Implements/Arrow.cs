using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Block
{
    public class Arrow : MonoBlock, IAttackBlock
    {
        [SerializeField] int interval;
        public int Interval => interval; // 攻撃インターバルの実装が必要
        
        public void Attack(DamageCaster dc)
        {
            // 攻撃の実装が必要
        }
    }
}
