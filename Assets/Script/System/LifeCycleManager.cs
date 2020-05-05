using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class LifeCycleManager
{
    class PriorityQueue
    {
        int Priority;
        GameObject Object;
        IUpdatable Target;
        public PriorityQueue(int p, IUpdatable t) { Priority = p; Target = t; Object = Target.gameObject; }
        public bool Update()
        {
            if (Target == null) return true;
            if (Object == null) return true;
            Target.UnityUpdate();
            return false;
        }
    }

    static BehaviourAttachment attachment = null;
    static LifeCycleManager instance = null;
    static public LifeCycleManager Instance { get
        {
            if(instance == null)
            {
                attachment = GameObject.FindObjectOfType<BehaviourAttachment>();
                if(attachment != null)
                {
                    instance = new LifeCycleManager();
                    attachment.SetUpdateCallback(instance.UpdateCallback);
                    attachment.SetDestroyCallback(instance.DestroyCallback);
                }
            }
            return instance;
        }
    }

    List<PriorityQueue> UpdateQueue = new List<PriorityQueue>();
    List<PriorityQueue> AddUpdateQueue = new List<PriorityQueue>();
    List<GameObject> DestroyQueue = new List<GameObject>();
    List<PriorityQueue> Remove = new List<PriorityQueue>();
    
    static public void AddUpdate(IUpdatable target, int priority)
    {
        Instance.AddUpdateQueue.Add(new PriorityQueue(priority, target));
    }

    static public void RegisterDestroy(GameObject obj)
    {
        Instance.DestroyQueue.Add(obj);
    }

    void UpdateCallback()
    {
        Remove.Clear();
        foreach (var q in UpdateQueue)
        {
            if (q.Update())
            {
                Remove.Add(q);
            }
        }
        foreach (var q in Remove)
        {
            UpdateQueue.Remove(q);
        }
        UpdateQueue.AddRange(AddUpdateQueue);
        AddUpdateQueue.Clear();
    }

    void DestroyCallback()
    {
        foreach (var obj in DestroyQueue)
        {
            GameObject.Destroy(obj);
        }
    }
}
