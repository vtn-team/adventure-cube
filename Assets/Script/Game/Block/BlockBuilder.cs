using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Block
{
    /// <summary>
    /// Unity上の、特定の型のオブジェクトプール
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectPool<T> where T : UnityEngine.Object, IObjectPool
    {
        T BaseCube = null;
        List<T> Pool = new List<T>();
        int Index = 0;

        public ObjectPool(T cube)
        {
            BaseCube = cube;
        }

        public void SetCapacity(int size)
        {
            //既にオブジェクトサイズが大きいときは更新しない
            if (size < Pool.Count) return;

            for(int i = Pool.Count-1; i<size; ++i)
            {
                var Obj = GameObject.Instantiate(BaseCube);
                Obj.DisactiveForInstantiate();
                Pool.Add(Obj);
            }
        }

        public T Instantiate()
        {
            T ret = null;
            for (int i = 0; i < Pool.Count; ++i)
            {
                int index = (Index + i) % Pool.Count;
                if(Pool[index].IsActive) continue;

                Pool[index].Create();
                ret = Pool[index];
                break;
            }

            return ret;
        }
    }
}