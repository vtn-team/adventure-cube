using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Block 
{
    public class Maeda : MonoBlock, IShieldBlock
    {
        public int Defence(int dmg)
        {
            if (dmg > 0)
            {
                dmg -= dmg;
                return dmg;
            }
            else
            {
                return 0;
            }
        }

        private void Start()
        {
            Renderer Renderer = GetComponent<Renderer>();
            if (MasterCube.FriendId == 1) Renderer.material = ResourceCache.GetMaterialCache(ResourceType.CubeMaterial, "Player");
            if (MasterCube.FriendId == 2) Renderer.material = ResourceCache.GetMaterialCache(ResourceType.CubeMaterial, "Enemy");
            life = figure;
        }
    }
}
