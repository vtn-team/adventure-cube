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

        float Timer = 0.0f;
        Rigidbody RigidBody;

        protected override void Setup()
        {
            LifeCycleManager.AddUpdate(this, 0);
            RigidBody = GetComponent<Rigidbody>();
            Type = SummonType.Bullet;
            Timer = 0.0f;
            base.Setup();
            Collider.Setup(Attack, FriendId);
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
            Debug.Log("attack");
        }
    }
}
