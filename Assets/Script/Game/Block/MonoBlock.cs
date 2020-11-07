using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Block
{
    /// <summary>
    /// ブロック基底
    /// 
    /// NOTE: このクラスをベースにブロック処理を記述する。すべてのブロックに共通する処理がここに記載される。
    /// NOTE: このクラスは継承することを前提に作られている
    /// </summary>
    public class MonoBlock : MonoBehaviour
    {
        public enum BlockType
        {
            Normal,
            Core,
            Attack,
            Skill,
            Speed
        }

        [SerializeField] protected int life = 1;
        [SerializeField] protected int figure = 1; //一応。
        [SerializeField] protected int rare = 1;

        public MasterCube MasterCube { get; private set; }
        public int Index { get; private set; }
        public BlockType Type { get; private set; }

        public int Life { get; protected set; }
        public int Figure { get; protected set; }
        public int Rare => rare;


        void Awake()
        {
            Life = life;
            Figure = figure;
            GameObjectCache.AddMonoBlockCache(this);
        }

        protected virtual void Setup()
        {

        }

        public bool IsFriend(int friendId)
        {
            return MasterCube.IsFriend(friendId);
        }

        public virtual bool IsAlive()
        {
            return Life > 0;
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


        private void OnCollisionEnter(Collision collision)
        {
            /*
            // Bulletが当たった
            if (collision.gameObject.CompareTag("Bullet"))
            {
                var mc = GameObjectCache.GetCharacter(collision.gameObject);
                if (mc == null)
                {
                    Debug.Log("不正な設定のオブジェクト:" + collision.gameObject.name);
                    return;
                }
                //if (mc.IsFriend(FriendId)) return;
                Callback(mc);
            }
            */
        }


        /// <summary>
        /// ブロックを作る(idから)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        static public MonoBlock Build(int id, MasterCube master)
        {
            var obj = Instantiate(ResourceCache.CubeMaster.GetAsset(id));
            var block = obj.GetComponent<MonoBlock>();
            block.MasterCube = master;
            block.Setup();
            return block;
        }
        
        /// <summary>
        /// ブロックを作る(インスタンスから/今後消す予定)
        /// </summary>
        /// <param name="prefab">元オブジェクト</param>
        /// <param name="master">親</param>
        /// <returns></returns>
        static public MonoBlock Build(MonoBlock prefab, MasterCube master)
        {
            var obj = GameObject.Instantiate(prefab);
            var block = obj.GetComponent<MonoBlock>();
            block.MasterCube = master;
            block.Setup();
            return block;
        }
    }
}