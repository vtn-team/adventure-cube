using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletObject;

namespace Block
{
    public class TestAttack : MonoBlock, IAttackBlock
    {
        [SerializeField] int interval;

        public bool CanIAttack => false;
        AutoAttackTimer AttackTimer = new AutoAttackTimer();

        protected override void Setup()
        {
            AttackTimer.Setup(interval);
            LifeCycleManager.AddUpdate(UnityUpdate, this.gameObject, 0);
        }

        public void Attack()
        {
            var obj = Bullet.Build<BulletObject.RollingBlock>("RollingBlock", MasterCube, this, null);
            obj.SetTarget(TargetHelper.SearchTarget(MasterCube,TargetHelper.SearchLogicType.NearestOne));


            var obj1 = Bullet.Build<BulletObject.PhysicsBall>("PhysicsBall",MasterCube, null,null);
            obj1.transform.position = MasterCube.transform.position + MasterCube.transform.forward * 2.0f;
            obj1.AddForce(MasterCube.transform.forward * 250.0f);
        }

        private void UnityUpdate()
        {
            AttackTimer.Update();
            if (AttackTimer.IsAttackOK)
            {
                Attack();
                AttackTimer.ResetTimer();
            }
        }


    }
}
