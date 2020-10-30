using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using Block;

namespace BulletObject
{
    public class PhysicsBall : Bullet
    {
        [SerializeField]
        float Life = 1.5f;

        bool IsSetUp = false;
        float Timer = 0.0f;
        Rigidbody RigidBody;

        protected override void Setup()
        {
            LifeCycleManager.AddUpdate(UnityUpdate, this.gameObject, 0);
            RigidBody = GetComponent<Rigidbody>();
            Timer = 0.0f;

            base.Setup();

            AttackSet.IsPowerfull = true;
            AttackSet.Atk = 1;
            AttackSet.Attacker = this.gameObject;
            AttackSet.Master = MasterCube;
        }

        public void AddForce(Vector3 force)
        {
            RigidBody.AddForce(force);
        }

        void UnityUpdate()
        {
            Timer += Time.deltaTime;
            if (Timer >= Life)
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
