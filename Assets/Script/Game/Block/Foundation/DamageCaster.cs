using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using Block;

/// <summary>
/// ダメージ計算器
/// 
/// NOTE: 関心の分離やStrategyパターンの活用
/// NOTE: ゲーム中のダメージはこのクラスで処理する
/// </summary>
public class DamageCaster
{
    public class AttackSet
    {
        public bool IsPowerfull = false;
        public int Atk;
        public MasterCube Master;
        public MasterCube Target;
        public GameObject Attacker;
    }

    public class DamageSet
    {
        public bool IsKnockback = false;
        public int TargetIndex = 0;
        public int ShieldDamage = 0;
        public int Damage = 0;
        public AttackSet AttackSet = null;
        public MasterCube Target = null;
    }

    MasterCube Owner;
    MasterCubeParameter OwnerParam; //オーナーとなるキューブが持つ値。参照のみ。
    public DamageCaster(MasterCube owner, MasterCubeParameter param)
    {
        Owner = owner;
        OwnerParam = param;
    }

    //内部関数
    protected void PreventDamage(AttackSet atkSet, ref DamageSet dmg)
    {
        //シールド解決
        foreach (var sh in Owner.ShieldCubes)
        {
            int before = dmg.Damage;
            dmg.Damage = sh.Defence(dmg.Damage);
            dmg.ShieldDamage += before - dmg.Damage;
            if (dmg.Damage <= 0) break;
        }
    }

    protected void Counter()
    {
        foreach (var counterCube in Owner.CounterCubes)
        {
            foreach (var attackCube in Owner.AttackCubes)
            {
                counterCube.Counter(attackCube);
            }
        }
    }

    protected int CalcDamage(AttackSet atkSet)
    {
        int dmg = atkSet.Atk;

        //攻撃側のパッシブ効果を探す
        atkSet.Master.PassiveCubes.ForEach(p => {
            if(p.PassiveType == PassiveType.DamageBuff)
            {
                dmg += p.PassiveEvent(0);
            }
        });

        //防御側のパッシブ効果を探す
        atkSet.Target.PassiveCubes.ForEach(p => {
            if (p.PassiveType == PassiveType.DamageBuff)
            {
                dmg += p.PassiveEvent(1);
            }
        });

        return dmg;
    }


    //ダメージ評価(確定はしない)
    static public DamageSet Evaluate(AttackSet atkSet)
    {
        DamageSet dmg = new DamageSet();
        if (atkSet == null) return dmg;
        if (atkSet.Target == null) return dmg;
        if (atkSet.Master == null) return dmg;

        dmg.AttackSet = atkSet;
        dmg.Target = atkSet.Target;

        //ダメージ値の決定
        dmg.Damage = atkSet.Master.DamageCaster.CalcDamage(atkSet);

        //防御対象と軽減ダメージを検索する
        dmg.TargetIndex = -1;
        atkSet.Target.DamageCaster.PreventDamage(atkSet, ref dmg);

        //ダメージがすべて軽減されたらそれで返す
        if (dmg.Damage <= 0)
        {
            dmg.IsKnockback = false;
            dmg.Damage = 0;
            return dmg;
        }

        //防御してくれなかったらターゲットを選ぶ
        //選ぶ優先度はアタックキャスターが決める
        //dmg.TargetIndex = atkSet.Master.AttackDamageCaster.DecideTarget(atkSet);

        atkSet.Target.DamageCaster.Counter();
        
        return dmg;
    }

    //ダメージを確定する
    static public void CastDamage(AttackSet atkSet)
    {
        var dmg = Evaluate(atkSet);
        CastDamage(dmg);
        if (dmg.Damage > 0)
        {
            DamagePopup.Pop(dmg.Target.gameObject, dmg.Damage, Color.red);
        }
        if (dmg.ShieldDamage > 0)
        {
            DamagePopup.Pop(dmg.Target.gameObject, dmg.ShieldDamage, Color.green);
        }
    }
    //ダメージを確定する
    static public void CastDamage(DamageSet dmg)
    {
        //dmg.Target.Damage(dmg.Damage);
        dmg.Target.UpdateDamage(dmg);
    }
}
