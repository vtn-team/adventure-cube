# ScriptableObject
Unity状のデータを管理するときに便利な話です。

## ScriptableObjectでデータを管理する
ScriptableObjectは、Unity上で以下の特性を持つデータ構造体です。  
・Inspectorと違い、実行終了してもデータが残る  
・ゲーム内でクラスとして使用できる(関数や別の変数定義もできる)  
・アセットとして読み込みができる  


## シリアライズ可能

ScriptableObjectに適用するデータは、シリアライズ可能である必要があります。  
[シリアライズ可能]()で確認しましょう。  


## エディタ拡張とのコラボ

ScriptableObjectそのままでは設定/使用しづらいデータとかはあります。  
そういう時にエディタ拡張を使って、入力や設定をヘルプするという手法を使います。  

エディタ拡張はやりすぎるとゲーム進行が一切進まなくなり、沼から帰ってこれなくなります。  
気を付けましょう。  


# ScriptableObjectの例

```
[CreateAssetMenu(fileName = "CubeSheet", menuName = "ScriptableObjects/CubeSheet", order = 1)]
public class CubeSheet : ScriptableObject
{
    [Serializable]
    class CubeAsset
    {
        public int Id;
        public string Key;
        public MonoBlock Block;
    }

    [SerializeField] List<CubeAsset> CubeAssetList = new List<CubeAsset>();

    public MonoBlock GetAsset(int id)
    {
        if (id < 0) return null;
        if (id >= CubeAssetList.Count) return null;

        return CubeAssetList[id].Block;
    }

    public string GetAssetKey(int id)
    {
        if (id < 0) return null;
        if (id >= CubeAssetList.Count) return null;

        return CubeAssetList[id].Key;
    }

    public int GetRandomCubeId(MonoBlock.BlockType type)
    {
        //いろいろ重いけどとりあえず我慢
        var list = CubeAssetList.Where(c => c.Block && c.Block.Type == type);
        int count = list.Count();
        if (count == 0) return 0;

        int d = UnityEngine.Random.Range(0, count);
        return CubeAssetList.IndexOf(list.ElementAt(d));
    }

    public int GetRandomCubeIdWithRare(MonoBlock.BlockType type, int rare)
    {
        //いろいろ重いけどとりあえず我慢
        var list = CubeAssetList.Where(c => c.Block && c.Block.Type == type && c.Block.Rare == rare);
        int count = list.Count();
        if(count == 0) return 0;

        int d = UnityEngine.Random.Range(0, count);
        return CubeAssetList.IndexOf(list.ElementAt(d));
    }
}
```

## お約束系
まず、この一文をクラスの前につけます。  
```
[CreateAssetMenu(fileName = "CubeSheet", menuName = "ScriptableObjects/CubeSheet", order = 1)]
```
これは、Unityに、このScriptableObjectを作るためのメニューを定義するよ、という命令です。  
Unity用の命令です。  

記入すると、Projectウインドウで右クリックした際、ScriptableObjectsというメニューと、その中の項目にCubeSheetというのが出てきます。  
それを押すと、このクラス型のデータを作ることができるようになります。  

## [Serializable]
クラス内で使用するデータは、すべてシリアライズ可能(Unityのエディタ上でで設定できる状態)にしてください。  
```
[Serializable]
Class CubeAsset
{
    public int Id;
    public string Key;
    public MonoBlock Block;
}

[SerializeField] List<CubeAsset> CubeAssetList = new List<CubeAsset>();
```

## 関数定義
ふつうにクラスとして使用できるので、データを整形して返すことができます。  
便利なので必要に応じて追加ましょう。  
