using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BehaviourAttachment : MonoBehaviour
{
    public delegate void LifeCycleEvent();

    LifeCycleEvent UpdateCallback = null;
    LifeCycleEvent DestroyCallback = null;

    private void Update()
    {
        UpdateCallback();
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
