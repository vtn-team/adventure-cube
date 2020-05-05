using UnityEngine;
using System.Collections;

namespace Summon
{
    public class Sword : SummonObject
    {
        protected override void Setup()
        {
            Type = SummonType.Sword;
            base.Setup();
        }
    }
}
