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
/// 
/// NOTE: 教材用に初期キューブ直指定できるようにした。
/// </summary>
[Serializable]
public class CubeStock
{
    int CubeId = 0;
    Vector3 PositionOffset = Vector3.zero;

    MonoBlock Cube = null;
    MasterCube MasterCube = null;

    public bool HasCube { get { return CubeId > 0; } }
    public MonoBlock.BlockType StockType { get; private set; }
    public MonoBlock CurrentCube => Cube;
    public bool IsAlive() { return Cube ? Cube.IsAlive() : false; }


    public void BreakDown()
    {

    }

    public void SetRoot(MasterCube master)
    {
        MasterCube = master;
    }

    //データシート経由でのセットアップ
    public void SetUp(int id, Vector3 offset, MonoBlock.BlockType type)
    {
        CubeId = id;

        //もし-1ならランダムで作る
        if(CubeId == -1)
        {
            CubeId = 0; //コンドヤル
        }

        PositionOffset = offset;
        StockType = type;

        if (CubeId != 0)
        {
            Cube = MonoBlock.Build(CubeId, MasterCube); //仮の挙動
            Cube.gameObject.layer = LayerMask.NameToLayer("Block");
            Cube.transform.SetParent(MasterCube.transform);
            Cube.transform.localPosition = PositionOffset;
        }
    }

    /*
    public void SetUpInitCube()
    {
        CubeId = 1; //ダミー
        Cube = MonoBlock.Build(InitCube, MasterCube); //仮の挙動
        Cube.transform.SetParent(MasterCube.transform);
        Cube.transform.localPosition = PositionOffset;
    }
    */

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
