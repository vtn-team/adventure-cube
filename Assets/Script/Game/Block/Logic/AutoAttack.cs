using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Block
{
    public class AutoAttack
    {
        IAttackBlock AttackBlock = null;
        float Timer = 0;
        float Interval;

        public bool IsSame(IAttackBlock block)
        {
            return AttackBlock == block;
        }

        public void Setup(IAttackBlock block)
        {
            AttackBlock = block;
            Timer = 0;
            Interval = block.Interval;
        }

        public void Update()
        {
            Timer += Time.deltaTime;
            if(Timer >= Interval)
            {
                AttackBlock.Attack();
            }
        }
    }
}
