using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Block;

public class IntervalBuff : Shooter , IPassiveBlock
{
    public PassiveType PassiveType => PassiveType.IntervalBuff;

    public int PassiveEvent(float param, float subparam)
    {
        return (int)(float)interval;
    }
}
