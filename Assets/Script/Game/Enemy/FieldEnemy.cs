using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using Block;

class FieldEnemy : MasterCube
{
    [SerializeField] MonoBlock CoreCube;
    [SerializeField] MonoBlock DropCube;

    private void Awake()
    {
        MonoBlock.Assign(0, CoreCube.gameObject, this);
        ChildBlocks.Add(CoreCube);
        Build();
    }
}
