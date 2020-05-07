using System.Collections.Generic;
using UnityEngine;

using Summon;

namespace Block
{
    public class Sword : MonoBlock
    {
        [SerializeField]
        string PrefabName = "Sword";
        Summon.Sword Summon;
        DamageCaster.AttackSet attack = new DamageCaster.AttackSet();

        protected override void Setup()
        {
            Summon = SummonObject.Build<Summon.Sword>("Sword", MasterCube, this, MasterCube.transform);
            SummonObject.SummonType type = Summon.Type;
            MasterCube.AddSummonGroup(type, Summon);
            Summon.SetupAttackCallback(Attack, MasterCube.FriendId);

            attack.IsPowerfull = true;
            attack.Power = 1;
            attack.Attacker = Summon.gameObject;
            attack.Master = MasterCube;
        }

        void Attack(MonoBlock target)
        {
            Debug.Log("attack");
            attack.Atk = Figure;
            attack.TargetBlock = target;
            DamageCaster.CastDamage(attack);
        }
    }
}
