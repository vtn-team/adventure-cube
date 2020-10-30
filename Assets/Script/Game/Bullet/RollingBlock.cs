using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using Block;

namespace BulletObject
{
    public class RollingBlock : Bullet
    {
        [SerializeField]
        float Life = 1.5f;

        bool IsSetUp = false;
        float Timer = 0.0f;
        Vector3 TopPos;
        Vector3 StartPos;
        Vector3 TargetPos;

        protected override void Setup()
        {
            LifeCycleManager.AddUpdate(UnityUpdate, this.gameObject, 0);
            Timer = 0.0f;

            base.Setup();

            AttackSet.IsPowerfull = true;
            AttackSet.Atk = 1;
            AttackSet.Attacker = this.gameObject;
            AttackSet.Master = MasterCube;
        }

        public void SetTarget(MasterCube target)
        {
            //中間点を計算する
            Vector3 sub = target.transform.position - this.transform.position;
            sub /= 2.0f;
            sub.y += 8.0f;//山なりになるように
            TopPos = sub;
            StartPos = this.transform.position;
            TargetPos = target.transform.position;

            Timer = 0.0f;
            IsSetUp = true;
        }
        
        void UnityUpdate()
        {
            Timer += Time.deltaTime;

            //山なりに着弾点に飛んでいく
            if (Timer <= Life / 2.0f)
            {
                this.transform.position = Vector3.Lerp(StartPos, TopPos, Timer / (Life/2.0f));
            }
            else
            {
                this.transform.position = Vector3.Lerp(TopPos, TargetPos, Timer / (Life - Life / 2.0f));
            }

            //適当に回転させる
            this.transform.Rotate(Time.deltaTime * 30, Time.deltaTime * 40, Time.deltaTime * 50);

            if(Timer >= Life)
            {
                LifeCycleManager.RegisterDestroy(this.gameObject);
            }
        }
        
        protected override void Attack(MasterCube hitObject)
        {
            if (IsAttacked) return;

            Debug.Log("attack");
            AttackSet.Target = hitObject;
            DamageCaster.CastDamage(AttackSet);
            IsAttacked = true;

            LifeCycleManager.RegisterDestroy(this.gameObject);
        }
    }
}
