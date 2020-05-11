using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Field
{
    public class MapTile
    {
        public enum TileType
        {
            Invalid,
            Path
        }

        public class MapChip
        {
            public int ChipId;
            GameObject ChipObject;
        }

        int Size = 1;
        int KeyPoint = 0;
        Transform Parent = null;
        GameObject Root = new GameObject();
        List<MapChip> Chips = new List<MapChip>();

        public MapTile(int size, int x, int y, int kp, Transform parent)
        {
            Size = size;
            KeyPoint = kp;
            Root.name = String.Format("MapTile({0},{1})", x, y);
            Root.transform.parent = parent;
            Root.isStatic = true;
            Root.transform.localPosition = new Vector3(Size * x, 0, Size * y);

            Chips.Capacity = Size * Size;
            for (int i = 0; i < Size * Size; ++i)
            {
                Chips.Add(new MapChip());
            }
        }

        //道生成
        public void GenerateRoute()
        {

        }

        public void Generate()
        {
            for(int y = 0; y < Size; ++y)
            {
                for(int x=0; x < Size; ++x)
                {
                    GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    Material mat = ResourceCache.GetMaterialCache(ResourceType.FieldMaterial, "Grass");

                    go.isStatic = true;
                    go.transform.parent = Root.transform;
                    go.transform.localPosition = new Vector3(-Size/2 + x, 0, -Size/2 + y);

                    //Material差し替え
                    MeshRenderer mr = go.GetComponent<MeshRenderer>();
                    mr.material = mat;
                    mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                    mr.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
                }
            }
        }
    }
}
