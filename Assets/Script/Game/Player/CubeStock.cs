using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Block;
using UnityEngine;

[Serializable]
public class CubeStock
{
    [SerializeField] Vector3 PositionOffset = Vector3.zero;
    [SerializeField] MonoBlock.BlockType _StockType = MonoBlock.BlockType.Blank;

    MonoBlock Cube = null;

    public MonoBlock.BlockType StockType => _StockType;
    public MonoBlock CurrentCube => Cube;
    public bool IsAlive() { return Cube ? Cube.IsAlive() : false; }
    public void BreakDown()
    {

    }

    public MonoBlock Build(int id, Transform transform)
    {
        if (Cube != null) return null;
        
        Cube = MonoBlock.Build<MonoBlock>(id);
        Cube.transform.SetParent(transform);
        Cube.transform.localPosition = PositionOffset;
        return Cube;
    }
}
