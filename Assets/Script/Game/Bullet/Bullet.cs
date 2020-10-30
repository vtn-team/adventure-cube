using UnityEngine;
using System.Collections;
using Block;

namespace BulletObject
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        AttackCollider Collider;

        protected bool IsAttacked = false;
        protected DamageCaster.AttackSet AttackSet = new DamageCaster.AttackSet();

        protected MasterCube MasterCube { get; private set; }
        protected MonoBlock OwnerCube { get; private set; }
        
        protected virtual void Setup()
        {
            SetupAttackCallback();
        }

        protected void SetupAttackCallback()
        {
            Collider.Setup(Attack, MasterCube.FriendId);
        }

        protected virtual void Attack(MasterCube hitObject)
        {

        }

        static public T Build<T>(string name, MasterCube master, MonoBlock owner, Transform parent) where T : Bullet
        {
            var prefab = ResourceCache.GetCache(ResourceType.Bullet, name);
            GameObject obj = null;
            if (parent == null)
            {
                obj = GameObject.Instantiate(prefab);
            }
            else
            {
                obj = GameObject.Instantiate(prefab, parent);
            }

            var blt = obj.GetComponent<T>();
            blt.MasterCube = master;
            blt.OwnerCube = owner;
            blt.Setup();
            return blt;
        }
    }
}
