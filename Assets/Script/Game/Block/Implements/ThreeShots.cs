using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletObject;

namespace Block
{
    public class ThreeShots : MonoBlock, IAttackBlock
    {
		public bool CanIAttack => true;

		public void Attack()
		{
			Debug.Log("Attack");
			var Obj = Bullet.Build<BulletObject.RollingBlock>("RollingBlock", MasterCube, this, null);
			Obj.SetTarget(TargetHelper.SearchTarget(MasterCube, TargetHelper.SearchLogicType.NearestOne));
			Obj.transform.localScale *= 2;
		}

		public override void BreakDown()
		{
			Attack();
			base.BreakDown();
		}
	}
}
