using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Block
{
    public class Attacker : MonoBlock, IAttackBlock
    {
        public bool CanIAttack => false;
        AutoAttackTimer m_timer = new AutoAttackTimer();
        [SerializeField] int m_attack = 5;
        [SerializeField] float m_attackDistance = 0.5f;
        [SerializeField] int m_interbal = 1;

        protected override void Setup()
        {
            m_timer.Setup(m_interbal);
            LifeCycleManager.AddUpdate(AttackUpdate, this.gameObject, 1);
        }
        public void Attack()
        {
            var obj = TargetHelper.SearchTarget(MasterCube, TargetHelper.SearchLogicType.NearestOne);
            if (Vector3.Distance(MasterCube.gameObject.transform.position,obj.transform.position) < m_attackDistance)
            {
                DamageCaster.AttackSet aset = new DamageCaster.AttackSet()
                {
                    Atk = m_attack,
                    IsPowerfull = false,
                    Master = this.MasterCube,
                    Target = obj
                };
                DamageCaster.DamageSet dset = DamageCaster.Evaluate(aset);
                DamageCaster.CastDamage(dset);
                if (obj.CoreCube.Life <= 0)
                {
                    DamagePopup.Pop(obj.gameObject, dset.Damage, Color.red);
                }
                else
                {
                    DamagePopup.Pop(obj.gameObject, dset.Damage, Color.green);
                }
            }
            
            
        }

        public void AttackUpdate()
        {
            m_timer.Update();
            if (m_timer.IsAttackOK)
            {
                Attack();
                m_timer.ResetTimer();
            }
        }
    }

}