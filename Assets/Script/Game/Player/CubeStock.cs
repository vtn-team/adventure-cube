using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Block;
using UnityEngine;

/// <summary>
/// 装備キューブの管理場所
/// 
/// NOTE: キューブはBlankになることがある
/// NOTE: 指定されたタイプ以外のキューブは装備できない
/// </summary>
[Serializable]
public class CubeStock
{
    [SerializeField] int CubeId;
    [SerializeField] Vector3 PositionOffset = Vector3.zero;
    [SerializeField] MonoBlock.BlockType _StockType = MonoBlock.BlockType.Normal;

    MonoBlock Cube = null;
    MasterCube MasterCube = null;

    public bool HasCube { get { return CubeId > 0; } }
    public MonoBlock.BlockType StockType => _StockType;
    public MonoBlock CurrentCube => Cube;
    public bool IsAlive() { return Cube ? Cube.IsAlive() : false; }


    public void BreakDown()
    {

    }

    public void SetRoot(MasterCube master)
    {
        MasterCube = master;
    }

    public void Equip(int id)
    {
        CubeId = id;
        Build(id);
    }

    MonoBlock Build(int id)
    {
        Cube = MonoBlock.Build(id, MasterCube);
        Cube.transform.SetParent(MasterCube.transform);
        Cube.transform.localPosition = PositionOffset;
        return Cube;
    }
}
