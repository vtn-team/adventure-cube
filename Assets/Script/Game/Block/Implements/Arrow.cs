using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Block
{
    /// <summary>
    /// 矢(？)を飛ばす実装
    /// </summary>
    public class Arrow : MonoBlock, IAttackBlock
    {
        [SerializeField] int interval;
        public bool CanIAttack => false;
        AutoAttackTimer AutoAttack;

        protected override void Setup()
        {
            CanIAttack
            LifeCycleManager.AddUpdate(UnityUpdate, this.gameObject, 0);
        }

        
        public void Attack(DamageCaster dc)
        {
            // 攻撃の実装が必要
        }

        void UnityUpdate()
        {

        }
    }
}
