using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using BulletObject;

namespace Block
{
    /// <summary>
    /// 何かを飛ばす実装
    /// </summary>
    public class RollingBlock : MonoBlock, IAttackBlock
    {
        [SerializeField] int interval;
        public bool CanIAttack => false;
        AutoAttackTimer AutoAttack;

        protected override void Setup()
        {
            LifeCycleManager.AddUpdate(UnityUpdate, this.gameObject, 0);
        }

        // 攻撃の実装が必要
        public void Attack()
        {
            //キューブを作って飛ばす
            //キューブはマスターキューブの直上+1mにつくる
            var Obj = Bullet.Build<BulletObject.RollingBlock>("RollingBlock", MasterCube, this, null);
            Obj.transform.position = new Vector3(MasterCube.Coord.X, MasterCube.Coord.Top + 1.0f, MasterCube.Coord.Z);
        }

        void UnityUpdate()
        {

        }
    }
}
