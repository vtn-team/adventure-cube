using UnityEngine;
using System;
using System.Collections;

namespace Summon
{
    public interface ISummonGroup
    {
        void Add(SummonObject summon);
        void Remove(SummonObject summon);
        void Update();
        void Replace();
    }
}
