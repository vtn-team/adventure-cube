using System;
using System.Collections.Generic;
using UnityEngine;

namespace Summon
{
    public class SwordGroup : IObjectGroup<SummonObject>
    {
        List<Sword> Swords = new List<Sword>();
        
        public void Add(SummonObject s)
        {
            Swords.Add((Sword)s);
        }

        public void Remove(SummonObject s)
        {
            Swords.Remove((Sword)s);
        }
        
        //剣はルービックキューブの横に並び、円周上に等間隔に配置される。
        public void Replace()
        {
            float angle = 360.0f / Swords.Count;
            for (int i = 0; i < Swords.Count; ++i)
            {
                Swords[i].transform.rotation = Quaternion.AngleAxis(i * angle, Vector3.up);
            }
        }

        public void Update()
        {
            for (int i = 0; i < Swords.Count; ++i)
            {
                Swords[i].transform.Rotate(Vector3.up, 1.0f);
            }
        }
    }
}
