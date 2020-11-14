using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using BulletObject;

namespace Block
{
    /// <summary>
    /// 何かを飛ばす実装
    /// </summary>
    public class Shooter : MonoBlock, IAttackBlock, ICooldownTimer
    {
        [SerializeField] int interval;
        public float CurrentInterval => AutoAttack.Current;
        public bool CanIAttack => false;
        AttackTimer AutoAttack = new AttackTimer();

        ObjectPool<RollingBlock> ObjPool = new ObjectPool<RollingBlock>();

        protected override void Setup()
        {
            AutoAttack.Setup(interval);
            LifeCycleManager.AddUpdate(UnityUpdate, this.gameObject, 0);

            //作っておいてプールする
            ObjPool.Pooling(Bullet.Build<RollingBlock>("RollingBlock", MasterCube, this, null));
            ObjPool.Pooling(Bullet.Build<RollingBlock>("RollingBlock", MasterCube, this, null));
            ObjPool.Pooling(Bullet.Build<RollingBlock>("RollingBlock", MasterCube, this, null));
        }

        // 攻撃の実装が必要
        public void Attack()
        {
            //キューブを作って飛ばす
            //キューブはマスターキューブの直上+1mにつくる
            var target = TargetHelper.SearchTarget(MasterCube, TargetHelper.SearchLogicType.NearestOne);
            if (target)
            {
                var Obj = ObjPool.Instantiate();
                if (Obj)
                {
                    Obj.SetTarget(target);
                }
            }
            else
            {
                Debug.Log("ターゲットが見つかりませんでした。");
            }
        }

        void UnityUpdate()
        {
            AutoAttack.Update();
            if(AutoAttack.IsAttackOK)
            {
                Attack();
                AutoAttack.ResetTimer();
            }
        }
    }
}
