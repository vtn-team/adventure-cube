using UnityEngine;
using System.Collections;
using Block;

namespace Summon
{
    public class SummonObject : MonoBehaviour
    {
        public enum SummonType
        {
            Blank,
            Sword,
            MAX
        }

        protected MasterCube MasterCube { get; private set; }
        protected MonoBlock OwnerCube { get; private set; }

        void Awake()
        {
        }
        
        protected virtual void Setup()
        {
        }

        static public SummonObject Build(SummonType type, string name, MonoBlock owner, MasterCube master)
        {
            var prefab = ObjectSummoner.GetCache(name);
            var obj = GameObject.Instantiate(prefab, master.transform);
            var summon = obj.GetComponent<SummonObject>();
            summon.MasterCube = master;
            summon.OwnerCube = owner;
            summon.Setup();
            master.AddSummonGroup(type, summon);
            return summon;
        }

        static public ISummonGroup Build(SummonType type)
        {
            switch(type)
            {
                case SummonType.Sword: return new SwordGroup();
            }
            return null;
        }
    }
}
