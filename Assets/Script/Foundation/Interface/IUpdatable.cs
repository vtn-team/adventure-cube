using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public interface IUpdatable
{
    GameObject gameObject { get; }
    void UnityUpdate();
}
