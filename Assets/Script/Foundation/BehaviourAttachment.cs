using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// ライフサイクルイベントを別のクラスで使う時に使うクラス
/// 
/// NOTE: あまり使わないほうがいい
/// </summary>
public class BehaviourAttachment : MonoBehaviour
{
    public delegate void LifeCycleEvent();

    LifeCycleEvent UpdateCallback = null;
    LifeCycleEvent DestroyCallback = null;

    void Update()
    {
        UpdateCallback();
    }

    void OnDestroy()
    {
        DestroyCallback();
    }

    public void SetUpdateCallback(LifeCycleEvent evt)
    {
        UpdateCallback = evt;
    }

    public void SetDestroyCallback(LifeCycleEvent evt)
    {
        DestroyCallback = evt;
    }
}
