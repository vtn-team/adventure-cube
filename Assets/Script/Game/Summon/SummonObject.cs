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
            Bullet,
            MAX
        }
        
        protected MasterCube MasterCube { get; private set; }
        protected MonoBlock OwnerCube { get; private set; }
        public SummonType Type { get; protected set; }
        
        protected virtual void Setup()
        {
        }

        static public T Build<T>(string name, MasterCube master, MonoBlock owner, Transform parent) where T : SummonObject
        {
            var prefab = ObjectSummoner.GetCache(name);
            GameObject obj = null;
            if (parent == null)
            {
                obj = GameObject.Instantiate(prefab);
            }
            else
            {
                obj = GameObject.Instantiate(prefab, parent);
            }
            var summon = obj.GetComponent<T>();
            summon.MasterCube = master;
            summon.OwnerCube = owner;
            summon.Setup();
            return summon;
        }

        static public IObjectGroup<SummonObject> Build(SummonType type)
        {
            switch(type)
            {
                case SummonType.Sword: return new SwordGroup();
            }
            return null;
        }
    }
}
