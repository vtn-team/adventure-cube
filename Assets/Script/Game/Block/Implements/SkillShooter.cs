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
    public class SkillShooter : MonoBlock, ISkillBlock, IObserver<InputObserver.InputData>
    {
        [SerializeField] int interval;
        public bool CanIAttack => false;
        AttackTimer IntervalTimer = new AttackTimer();

        ObjectPool<RollingBlock> ObjPool = new ObjectPool<RollingBlock>();

        protected override void Setup()
        {
            GameManager.Instance.InputObs.AddObserver(this);
            LifeCycleManager.AddUpdate(UnityUpdate, this.gameObject, 0);
            IntervalTimer.Setup(interval);

            //作っておいてプールする
            ObjPool.Pooling(Bullet.Build<RollingBlock>("RollingBlock", MasterCube, this, null));

            base.Setup();
        }

        public override void BreakDown()
        {
            GameManager.Instance.InputObs.DeleteObserver(this);
            base.BreakDown();
        }

        // 攻撃の実装が必要
        public void Skill()
        {
            //キューブを作って飛ばす
            //キューブはマスターキューブの直上+1mにつくる
            var target = TargetHelper.SearchTarget(MasterCube, TargetHelper.SearchLogicType.NearestOne);
            if (target)
            {
                var Obj = ObjPool.Instantiate();
                Obj.SetTarget(target);
            }
            else
            {
                Debug.Log("ターゲットが見つかりませんでした。");
            }
        }

        void UnityUpdate()
        {
            IntervalTimer.Update();
        }

        public void NotifyUpdate(InputObserver.InputData input)
        {
            if (input.Type == InputObserver.InputType.Skill && IntervalTimer.IsAttackOK)
            {
                Skill();
                IntervalTimer.ResetTimer(false);
            }
        }
    }
}
