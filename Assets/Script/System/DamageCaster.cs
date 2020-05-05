using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

    public int CastDamage(int dir, ref int dmg)
    {
        return dmg;
    }
}
