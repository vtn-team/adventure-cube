using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using Block;

public class FieldEnemy : MasterCube
{
    [SerializeField] MonoBlock DropCube;

    public override void Build(int charId)
    {
        FriendId = 2;

        base.Build(charId);
    }

    protected override void Death()
    {
        base.Death();

        FieldBlock.Drop(this, DropCube);
    }
}
