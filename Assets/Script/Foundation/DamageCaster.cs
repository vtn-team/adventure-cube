using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using Block;

public class DamageCaster
{
    public delegate int DamageEffectDelegate(int dir, ref int dmg);
    public DamageEffectDelegate DamageEffect { get; set; }

    protected List<MonoBlock> Children = null;

    private DamageCaster() { }
    public DamageCaster(List<MonoBlock> c)
    {
        Children = c;
    }


    public class AttackSet
    {
        bool IsPowerfull = false;
        float Power;
        int Atk;
        MasterCube Master;
        GameObject Attacker;
    }

    public class DamageSet
    {
        //ダメージ用汎用
        bool IsKnockback = false;
        int Damage = 0;
    }

    public void PreventDamage(AttackSet atkSet, ref DamageSet dmg)
    {
        //tbd
        //シールドはここで解決する
    }

    public DamageSet CastDamage(AttackSet atkSet, MasterCube target)
    {
        DamageSet dmg = new DamageSet();
        if (target == null) return dmg;

        target.TakeDamageCaster.PreventDamage(atkSet, ref dmg);

        return dmg;
    }
}
