using UnityEngine;
using System.Collections;

namespace Block
{
    public class Core : MonoBlock, ICoreBlock
    {
        private void Start()
        {
            Renderer Renderer = GetComponent<Renderer>();
            Debug.Log(MasterCube.FriendId);
            if (MasterCube.FriendId == 1) Renderer.material = ResourceCache.GetMaterialCache(ResourceType.CubeMaterial, "Player");
            if (MasterCube.FriendId == 2) Renderer.material = ResourceCache.GetMaterialCache(ResourceType.CubeMaterial, "Enemy"); ;
        }

        public bool IsBreakdown()
        {
            return Life <= 0;
        }
    }
}
