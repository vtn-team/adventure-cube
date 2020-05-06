using UnityEngine;
using System.Collections;

using Block;

namespace Summon
{
    public class Sword : SummonObject
    {
        [SerializeField] AttackCollider Collider;

        protected override void Setup()
        {
            Type = SummonType.Sword;
            base.Setup();
        }

        public void SetupAttackCallback(AttackCollider.MonoBlockHitCallback callback, int friendId)
        {
            Collider.Setup(callback, friendId);
        }
    }
}
