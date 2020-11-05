using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SaveManager
{
    static SaveManager Instance = new SaveManager();

#if RELEASE
const string FILEPATH = "save.dat";
#else
    const string FILEPATH = "debugsave.txt";
#endif

    SaveData Data = null;

    static public void Load()
    {
        Instance.Data = LocalData.Load<SaveData>(FILEPATH);
    }

    static public SaveData GetData()
    {
        if (Instance.Data == null)
        {
            Load();
        }
        return Instance.Data;
    }

    static public void Save()
    {
        LocalData.Save<SaveData>(FILEPATH, Instance.Data);
    }
}
