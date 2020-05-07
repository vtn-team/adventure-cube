using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using Block;

namespace Summon
{
    public class Bullet : SummonObject, IUpdatable
    {
        [SerializeField]
        float Life = 1.5f;

        [SerializeField]
        AttackCollider Collider;

        bool IsAttacked = false;
        DamageCaster.AttackSet attack = new DamageCaster.AttackSet();
        float Timer = 0.0f;
        Rigidbody RigidBody;

        protected override void Setup()
        {
            LifeCycleManager.AddUpdate(UnityUpdate, this.gameObject, 0);
            RigidBody = GetComponent<Rigidbody>();
            Type = SummonType.Bullet;
            Timer = 0.0f;
            base.Setup();
        }

        public void SetupAttackCallback(int atk, MasterCube master)
        {
            Collider.Setup(Attack, master.FriendId);

            attack.IsPowerfull = true;
            attack.Power = 1;
            attack.Atk = atk;
            attack.Attacker = this.gameObject;
            attack.Master = master;
        }

        public void AddForce(Vector3 force)
        {
            RigidBody.AddForce(force);
        }

        public void UnityUpdate()
        {
            Timer += Time.deltaTime;
            if(Timer >= Life)
            {
                LifeCycleManager.RegisterDestroy(this.gameObject);
            }
        }
        
        void Attack(MonoBlock target)
        {
            if (IsAttacked) return;

            Debug.Log("attack");
            attack.TargetBlock = target;
            DamageCaster.CastDamage(attack);
            IsAttacked = true;

            LifeCycleManager.RegisterDestroy(this.gameObject);
        }
    }
}
