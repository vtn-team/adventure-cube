using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterData
{
    [Serializable]
    public class Cube
    {
        public int Language;
        public int Id;
        public string Name;
        public string Prefab;
        public int Life;
        public int Figure;
        public int Formula;
    }

    [Serializable]
    public class Character
    {
        public int Id;
        public string Name;
        public int IsPlayable;
        public int FriendId;
        public int Level;
        public int Life;
    }

    [Serializable]
    public class CharacterDeck
    {
        public int CharId;
        public string Name;
        public int Type;
        public int InitCubeId;
        public float OffsetX;
        public float OffsetY;
        public float OffsetZ;
    }

    [Serializable]
    public class MasterDataClass<T>
    {
        public string Version;
        public T[] Data;
    }
}
