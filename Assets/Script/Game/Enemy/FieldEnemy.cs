using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using Block;

public class FieldEnemy : MasterCube
{
    [SerializeField] MonoBlock DropCube;

    private void Awake()
    {
        Build();
    }

    protected override void Death()
    {
        base.Death();

        FieldBlock.Drop(this, DropCube);
    }
}
