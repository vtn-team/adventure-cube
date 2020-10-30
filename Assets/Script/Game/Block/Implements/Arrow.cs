﻿using System;
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
            LifeCycleManager.AddUpdate(UnityUpdate, this.gameObject, 0);
        }

        // 攻撃の実装が必要
        public void Attack()
        {
            //キューブを作って飛ばす

        }

        void UnityUpdate()
        {

        }
    }
}
