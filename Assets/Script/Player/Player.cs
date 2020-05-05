using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using Block;

public class Player : MasterCube
{
    [SerializeField]
    protected List<MonoBlock.BlockType> Deck = new List<MonoBlock.BlockType>();

    public override void Build()
    {
        int x = -1;
        int y = -1;
        int z = -1;
        int index = 1;
        Vector3 Center = this.transform.position;
        foreach (var b in Deck)
        {
            var block = MonoBlock.Build(b, index, x, y, z, this);
            ChildBlocks.Add(block);
            block.transform.parent = this.transform;
            block.transform.position = new Vector3(Center.x + x, Center.y + y + 2.0f, Center.z + z);

            x++;
            if (x > 1)
            {
                x = -1;
                y++;
                if (y > 1)
                {
                    y = -1;
                    z++;
                    if (z > 1) break;
                }
            }
            index++;
        }

        base.Build();
    }
}

