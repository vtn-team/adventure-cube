using System.Collections.Generic;
using UnityEngine;

using Summon;

namespace Block
{
    public class Sword : MonoBlock
    {
        SummonObject SwordObj;

        protected override void Setup()
        {
            SwordObj = SummonObject.Build(SummonObject.SummonType.Sword, "Sword", this, MasterCube);
        }
    }
}
