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

        protected override void Setup()
        {
            Summon = SummonObject.Build<Summon.Sword>("Sword", MasterCube, this, MasterCube.transform);
            SummonObject.SummonType type = Summon.Type;
            MasterCube.AddSummonGroup(type, Summon);
            Summon.SetupAttackCallback(Attack, MasterCube.FriendId);
        }

        void Attack(MonoBlock target)
        {
            Debug.Log("attack");
        }
    }
}
