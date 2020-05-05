using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    class PriorityQueue
    {
        int Priority;
        IUpdatable Target;
        public PriorityQueue(int p, IUpdatable t) { Priority = p; Target = t; }
        public bool Update()
        {
            if (Target == null) return true;
            Target.UnityUpdate();
            return false;
        }
    }

    static UpdateManager Instance = null;
    List<PriorityQueue> Queue = new List<PriorityQueue>();
    List<PriorityQueue> Remove = new List<PriorityQueue>();

    private void Awake()
    {
        Instance = this;
    }

    static public void Add(IUpdatable target, int priority)
    {
        Instance.Queue.Add(new PriorityQueue(priority, target));
    }

    private void Update()
    {
        Remove.Clear();
        foreach (var q in Instance.Queue)
        {
            if (q.Update()) Remove.Add(q);
        }
        foreach (var q in Remove)
        {
            Instance.Queue.Remove(q);
        }
    }
}
