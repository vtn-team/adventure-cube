using UnityEngine;
using System.Collections;

public class Enemy : FieldObject
{
    public enum ActionPattern
    {
        None,
        StaticObject,
        RandomMove,
    }
    public enum AttackPattern
    {
        None,
        OnCircle,
        Counter,
    }



}
