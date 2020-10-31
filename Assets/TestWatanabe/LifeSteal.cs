using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using BulletObject;

namespace Block 
{
    public class LifeSteal : MonoBlock, IAttackBlock, IShieldBlock
    {
        [SerializeField] float interval = 8;
        public bool CanIAttack => false;
        AutoAttackTimer attackTimer = new AutoAttackTimer();

        protected override void Setup()
        {
            attackTimer.Setup(interval);
            LifeCycleManager.AddUpdate(LifeAttackUpdate, this.gameObject, 0);
            Life = Figure;
            Debug.Log(Life);
        }

        public void Attack()
        {
            var obj = Bullet.Build<BulletObject.RollingBlock>("RollingBlock", MasterCube, this, null);
            obj.SetTarget(TargetHelper.SearchTarget(MasterCube, TargetHelper.SearchLogicType.NearestOne));
            MasterCubeParameter Param = new MasterCubeParameter();
            DamageCaster damageCaster = new DamageCaster(MasterCube,Param);
            Life += damageCaster.LifeDamege;
            DamagePopup.Pop(this.gameObject, damageCaster.LifeDamege, Color.blue);
            Debug.Log(Life);
        }

        public int Defence(int dmg)
        {
            if (Life > dmg)
            {
                Life -= dmg;
                return 0;
            }
            else
            {
                int r = dmg - Life;
                Life = 0;
                return r;
            }
        }

        public void LifeAttackUpdate() 
        {
            attackTimer.Update();
            if (attackTimer.IsAttackOK)
            {
                Attack();
                attackTimer.ResetTimer();
            }
        }
    }
}