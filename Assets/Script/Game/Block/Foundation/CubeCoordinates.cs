using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// キューブ用の座標の集合体
/// </summary>
public struct CubeCoordinates
{
    public float X;
    public float Y;
    public float Z;

    public float Top { get; set; }
    public float Bottom { get; set; }

    public void SetPosition(Vector3 pos)
    {
        X = pos.x;
        Y = pos.y;
        Z = pos.z;
    }
}
