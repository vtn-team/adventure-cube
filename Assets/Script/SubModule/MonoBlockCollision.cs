using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MonoBlockCollision : MonoBehaviour
{
    public class HitObject
    {
        public MonoBlock Object;
        public float Timer;
    }
    protected List<HitObject> HitObjects = new List<HitObject>();

    public List<HitObject> GetObjects() { return HitObjects; }

    private void OnTriggerEnter(Collider col)
    {
        var Obj = MonoBlockCache.GetCache(col.gameObject);
        if (Obj)
        {
            HitObjects.Add(new HitObject() { Object = Obj, Timer = 0.0f });
            //Attack(Obj);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        var List = HitObjects.Where(o => {
            if (o.Object == null) return false;
            return o.Object.gameObject.GetInstanceID() == col.gameObject.GetInstanceID();
        }).ToList();
        List.All(o => HitObjects.Remove(o));
    }
}
