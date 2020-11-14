using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Block;

/// <summary>
/// ゲーム管理クラス
/// 
/// NOTE: パーシャルクラスにして、進行と管理を分けました
/// </summary>
public partial class GameManager : MonoBehaviour
{
    //ゲーム中のオブジェクトデータ
    [SerializeField] bool IsVersionUpFlag = false;
    [SerializeField] Player PlayableChar = null;
    [SerializeField] List<MasterCube> Enemy = new List<MasterCube>();
    [SerializeField] List<MasterCube> NPC = new List<MasterCube>();

    //ゲームデータ
    MasterData.MasterDataClass<MasterData.Cube> cubeMaster;
    MasterData.MasterDataClass<MasterData.Character> characterMaster;
    MasterData.MasterDataClass<MasterData.CharacterDeck> characterDeckMaster;
    static public MasterData.Cube[] CubeMaster => Instance.cubeMaster.Data;
    static public MasterData.Character[] CharacterMaster => Instance.characterMaster.Data;
    static public MasterData.CharacterDeck[] CharacterDeckMaster => Instance.characterDeckMaster.Data;
    int LoadingCount = 0;
    delegate void LoadMasterDataCallback<T>(T data);

    static GameManager instance = null;
    static public GameManager Instance => instance;
    private GameManager() { }

    private void Awake()
    {
        instance = this;
        InputObs = new InputObserver();
        GameObjectCache.Setup();

        LoadMasterData("Cube", (MasterData.MasterDataClass<MasterData.Cube> data) => cubeMaster = data);
        LoadMasterData("Character", (MasterData.MasterDataClass<MasterData.Character> data) => characterMaster = data);
        LoadMasterData("CharacterDeck", (MasterData.MasterDataClass<MasterData.CharacterDeck> data) => characterDeckMaster = data);

        LifeCycleManager.AddUpdate(UnityUpdate, this.gameObject, 0);
    }

    private void LoadMasterData<T>(string file, LoadMasterDataCallback<T> callback)
    {
        var data = LocalData.Load<T>(file);
        if(data == null || IsVersionUpFlag)
        {
            LoadingCount++;
            Network.WebRequest.Request<Network.WebRequest.GetString>("https://script.google.com/macros/s/AKfycbyc6WmX57vj8_V5tRL7eN4QCWMcLUQx8Jtu_B_JyqnMRGxH0Uk/exec?sheet="+file, Network.WebRequest.ResultType.String, (string json) =>
            {
                Debug.Log(json);
                var dldata = JsonUtility.FromJson<T>(json);
                LocalData.Save<T>(file, dldata);
                callback(dldata);
                Debug.Log("Network download. : " + file + " / " + json + "/" + dldata);
                --LoadingCount;
            });
        }
        else
        {
            Debug.Log("Local load. : " + file + " / " + data);
            callback(data);
        }
    }
    
    static public Player GetPlayableChar()
    {
        return Instance.PlayableChar;
    }

    static public List<MasterCube> GetEnemyList()
    {
        return Instance.Enemy;
    }
}
