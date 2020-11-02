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

            this.transform.position = MasterCube.transform.position; // new Vector3(MasterCube.Coord.X, MasterCube.Coord.Top + 1.0f, MasterCube.Coord.Z);
        }

        public void SetTarget(MasterCube target)
        {
            //雑な放物線
            //中間点を計算する
            Vector3 sub = target.transform.position - this.transform.position;
            sub *= 0.5f;
            sub.y += 4.0f;//山なりになるように
            TopPos = this.transform.position + sub;

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
                this.transform.position = Vector3.Lerp(TopPos, TargetPos, (Timer - Life / 2.0f) / (Life - Life / 2.0f));
            }

            //適当に回転させる
            this.transform.Rotate(Time.deltaTime * 300, Time.deltaTime * 400, Time.deltaTime * 500);

            if(Timer >= Life)
            {
                LifeCycleManager.RegisterDestroy(this.gameObject);
            }
        }
        
        protected override void Attack(MasterCube hitObject)
        {
            if (IsAttacked) return;
            
            AttackSet.Target = hitObject;
            DamageCaster.CastDamage(AttackSet);
            IsAttacked = true;

            LifeCycleManager.RegisterDestroy(this.gameObject);
        }
    }
}
