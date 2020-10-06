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
        public float Power;
        public int Atk;
        public MasterCube Master;
        public GameObject Attacker;
        public MonoBlock TargetBlock;
    }

    public class DamageSet
    {
        public bool IsKnockback = false;
        public int TargetIndex = 0;
        public int Damage = 0;
        public AttackSet AttackSet = null;
        public MonoBlock Target = null;
        public MasterCube TargetMaster = null;
    }

    //内部関数
    protected void PreventDamage(AttackSet atkSet, ref DamageSet dmg)
    {
        //tbd
        //シールドはここで解決する
    }

    protected int CalcDamage(AttackSet atkSet)
    {
        //tbd
        //とりあえず
        return atkSet.Atk;
    }


    //ダメージ評価(確定はしない)
    static public DamageSet Evaluate(AttackSet atkSet)
    {
        DamageSet dmg = new DamageSet();
        if (atkSet == null) return dmg;
        if (atkSet.TargetBlock == null) return dmg;
        if (atkSet.Master == null) return dmg;
        if (atkSet.TargetBlock.MasterCube == null) return dmg;

        dmg.AttackSet = atkSet;
        dmg.Target = atkSet.TargetBlock;
        dmg.TargetMaster = atkSet.TargetBlock.MasterCube;

        //ダメージ値の決定
        dmg.Damage = atkSet.Master.AttackDamageCaster.CalcDamage(atkSet);

        //防御対象と軽減ダメージを検索する
        dmg.TargetIndex = -1;
        atkSet.TargetBlock.MasterCube.TakeDamageCaster.PreventDamage(atkSet, ref dmg);

        //ダメージがすべて軽減されたらそれで返す
        if (dmg.Damage <= 0)
        {
            dmg.IsKnockback = false;
            dmg.Damage = 0;
            return dmg;
        }

        //防御してくれなかったらターゲットを選ぶ
        //選ぶ優先度はアタックキャスターが決める
        dmg.TargetIndex = atkSet.Master.AttackDamageCaster.DecideTarget(atkSet);
        
        return dmg;
    }

    //ダメージを確定する
    static public void CastDamage(AttackSet atkSet)
    {
        CastDamage(Evaluate(atkSet));
    }
    //ダメージを確定する
    static public void CastDamage(DamageSet dmg)
    {
        dmg.Target.Damage(dmg.Damage);
        dmg.TargetMaster.UpdateDamage(dmg);
    }
}
