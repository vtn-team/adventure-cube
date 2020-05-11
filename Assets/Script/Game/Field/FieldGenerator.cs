using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using Field;

public class FieldGenerator : MonoBehaviour
{
    [Serializable]
    class KeyPoint
    {
        public int KeyId;
        Vector2 Point;
    }

    [SerializeField]
    int FieldSize = 10;

    [SerializeField]
    int TileSize = 5;

    [SerializeField]
    List<MapTile> MapTiles = new List<MapTile>();

    [SerializeField]
    List<KeyPoint> KeyPoints = new List<KeyPoint>();

    private void Start()
    {
        Generate();
    }

    public void Generate()
    {
        int keyPoint = 0;
        for (int y = 0; y < FieldSize; ++y)
        {
            for (int x = 0; x < FieldSize; ++x)
            {
                var mt = new MapTile(TileSize,  -FieldSize/2 + x, -FieldSize / 2 + y, keyPoint, this.transform);
                MapTiles.Add(mt);
            }
        }

        //
        MapTiles.ForEach(mt => mt.Generate());
    }
}
