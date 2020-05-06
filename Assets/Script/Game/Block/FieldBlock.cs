using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Block
{
    public class FieldBlock
    {

        static public void Drop(MasterCube owner, MonoBlock drop)
        {
            var newCube = GameObject.Instantiate(drop);
            newCube.transform.position = owner.transform.position;

            var collider = newCube.gameObject.AddComponent<BoxCollider>();
            collider.isTrigger = true;
        }
    }
}
