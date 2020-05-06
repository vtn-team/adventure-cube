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
            Collider.Setup(Attack, FriendId);
        }

        void Attack(MonoBlock target)
        {
            Debug.Log("attack");
        }
    }
}
