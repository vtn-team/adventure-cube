using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Block
{
    public class MonoBlock : MonoBehaviour
    {
        public enum BlockType
        {
            Blank,
            Core,
            Wood,
            Sword,
            Shield,
            MAX
        }

        //外部セット用で最初以外使用しない
        [SerializeField] protected BlockType _BlockType = BlockType.Blank;
        [SerializeField] protected int _Life = 1;
        [SerializeField] protected int _Figure = 1;
        [SerializeField] protected int _Rare = 1;
        [SerializeField] protected int _Priority = 1;

        public int Life { get; protected set; }
        public int Figure { get; protected set; }

        public BlockType Type => _BlockType;
        public int Rare => _Rare;
        public int Priority => _Priority;

        void Awake()
        {
            Life = _Life;
            Figure = _Figure;

            GameObjectCache.AddMonoBlockCache(this);
        }

        public virtual bool IsAlive()
        {
            return Life > 0;
        }

        protected virtual void Setup()
        {

        }

        public virtual void UpdateBlock()
        {

        }

        public virtual void Damage(int dmg)
        {
            if (dmg > 0)
            {
                Life -= dmg;
            }
        }

        public virtual void BreakDown()
        {
            //tbd
            //とりあえず
            LifeCycleManager.RegisterDestroy(this.gameObject);
        }



        static public T Build<T>(int id) where T : MonoBlock
        {
            var prefab = ResourceCache.GetCube(id);
            var obj = GameObject.Instantiate(prefab);
            var block = obj.GetComponent<T>();
            block.Setup();
            return block;
        }
    }
}