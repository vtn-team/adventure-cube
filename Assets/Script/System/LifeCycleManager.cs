using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class LifeCycleManager
{
    class PriorityQueue
    {
        public int Priority { get; private set; }
        GameObject Object;
        IUpdatable Target;
        public PriorityQueue(int p, IUpdatable t)
        {
            Priority = p;
            Target = t;
            Object = Target != null ? Target.gameObject : null;
        }
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

    LinkedList<PriorityQueue> UpdateQueue = new LinkedList<PriorityQueue>();
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

        if (AddUpdateQueue.Count > 0)
        {
            foreach (var q in AddUpdateQueue)
            {
                var node = UpdateQueue.LastOrDefault(u => u.Priority <= q.Priority);
                if (node == null)
                {
                    UpdateQueue.AddFirst(q);
                }
                else
                {
                    UpdateQueue.AddAfter(UpdateQueue.Find(node), q);
                }
            }
            //UpdateQueue = UpdateQueue.Union(AddUpdateQueue).OrderBy(q => -q.Priority).ToList();
            AddUpdateQueue.Clear();
        }
    }

    void DestroyCallback()
    {
        foreach (var obj in DestroyQueue)
        {
            GameObject.Destroy(obj);
        }
    }
}
