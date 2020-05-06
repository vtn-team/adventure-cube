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

        [SerializeField] protected int life = 1;
        [SerializeField] protected int figure = 1; //一応。

        protected MasterCube MasterCube { get; private set; }
        public int Index { get; private set; }

        public int Life { get; protected set; }
        public int Figure { get; protected set; }


        void Awake()
        {
            Life = life;
            Figure = figure;
            MonoBlockCache.AddCache(this);
        }

        public bool IsFriend(int friendId)
        {
            return MasterCube.IsFriend(friendId);
        }

        protected virtual void Setup()
        {

        }

        public virtual void UpdateBlock()
        {

        }

        public bool Damage(int dmg)
        {
            if (dmg > 0)
            {
                Life -= dmg;
            }
            return Life <= 0;
        }

        private void OnTriggerEnter(Collider player)
        {
            if (player.CompareTag("Runner"))
            {
                //拾われた際に何かするなら
            }
        }

        static public T Build<T>(BlockType type, int index, MasterCube master) where T : MonoBlock
        {
            var prefab = BlockBuilder.GetCache(type);
            var obj = GameObject.Instantiate(prefab);
            var block = obj.GetComponent<T>();
            block.MasterCube = master;
            block.Index = index;
            block.Setup();
            return block;
        }

        static public void Assign(int index, GameObject root, MasterCube master)
        {
            var block = root.GetComponent<MonoBlock>();
            block.MasterCube = master;
            block.Index = index;
            block.Setup();
        }
    }
}