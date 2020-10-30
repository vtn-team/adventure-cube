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
        public int Damage = 0;
        public AttackSet AttackSet = null;
        public MasterCube Target = null;
    }

    MasterCubeParameter OwnerParam; //オーナーとなるキューブが持つ値。参照のみ。
    public DamageCaster(MasterCubeParameter param)
    {
        OwnerParam = param;
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
        if (atkSet.Target == null) return dmg;
        if (atkSet.Master == null) return dmg;

        dmg.AttackSet = atkSet;
        dmg.Target = atkSet.Target;

        //ダメージ値の決定
        dmg.Damage = atkSet.Master.AttackDamageCaster.CalcDamage(atkSet);

        //防御対象と軽減ダメージを検索する
        dmg.TargetIndex = -1;
        atkSet.Target.TakeDamageCaster.PreventDamage(atkSet, ref dmg);

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
        //dmg.Target.Damage(dmg.Damage);
        dmg.Target.UpdateDamage(dmg);
    }
}
