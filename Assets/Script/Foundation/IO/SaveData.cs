using SerializableCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// ローカルセーブ用クラス
/// </summary>
[Serializable]
public class SaveData
{
    /// <summary>
    /// キューブそれぞれが復元に必要な情報を記録する
    /// </summary>
    [Serializable]
    public class CubeData : SerializableDictionary<string, string>
    {
            
    }

    [Serializable]
    public class MasterCubeSave
    {
        public int Type;
        public List<CubeData> Cubes = new List<CubeData>();
    }

    public string Version = "0.1";
    public List<MasterCubeSave> CubeDataList = new List<MasterCubeSave>();
}