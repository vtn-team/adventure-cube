# 2つのFactoryパターンとBuilder
Factoryは、オブジェクトの生成を抽象化する際によく使用されます。  
この項ですが、書いてて本と結構違うなと思いました。

## 2つのFactory
Factoryは、オブジェクトの生成方法に関連したパターンです。  
Abstract Factoryパターンと、Factory Methodパターンの2種類がデザインパターンとして存在します。  

## Builder
他にもBuilderパターンというものがあります。しかし、Factoryパターンで表現可能なものも多く、昨今では非推奨なパターンでもあようです。  
私は使うので、これも合わせて軽く解説します。  


## Abstract Factory - 生成手順を提供する
Abstract Factoryは、オブジェクトの適切な生成方法を提供してくれるメソッドです。  
敵の種類などに対応して、複雑に設定を行う際、使用する…かもしれません。  
正直、このパターンよりBuilderパターンの方が使用する機会が多いと思います。  

adventure-cubeでは実装していませんので、具体例は以下のQiitaなどを参考にしてみてください。(4.10と4.11を見てください)  
https://qiita.com/i-tanaka730/items/21c52a36bb2ffded5dde  

カードゲームの実装などで使用する機会はあるかもしれません。  


## Factory Method - オブジェクトの生成を抽象化する
Factory Methodは、Unityではよく使用されます。  

理由としては、Prefabの読み出し方法の切り分けや、オブジェクトプールの実装、GameObject.Instantiateを呼び出す必要があるなど、生成が複雑になるためです。  
よって、後述するInstantiateのBuilderパターンなど、オブジェクト生成をクラスメソッドに委ねることが多くあります。  

まずは、Factory Methodの典型例である、オブジェクトプールの実装を示します。  

```
public interface IObjectPool
{
    bool IsActive { get; }
    void DisactiveForInstantiate();
    void Create();
    void Destroy();
}
```

```
/// <summary>
/// Unity上の、特定の型のオブジェクトプール
/// </summary>
/// <typeparam name="T"></typeparam>
public class ObjectPool<T> where T : UnityEngine.Object, IObjectPool
{
    T Base = null;
    List<T> Pool = new List<T>();
    int Index = 0;

    public ObjectPool(T obj)
    {
        Base = obj;
    }

    public void SetCapacity(int size)
    {
        //既にオブジェクトサイズが大きいときは更新しない
        if (size < Pool.Count) return;

        for(int i = Pool.Count-1; i<size; ++i)
        {
            var Obj = GameObject.Instantiate(Base);
            Obj.DisactiveForInstantiate();
            Pool.Add(Obj);
        }
    }

    public T Instantiate()
    {
        T ret = null;
        for (int i = 0; i < Pool.Count; ++i)
        {
            int index = (Index + i) % Pool.Count;
            if(Pool[index].IsActive) continue;

            Pool[index].Create();
            ret = Pool[index];
            break;
        }

        return ret;
    }
}
```

オブジェクトプールの実装は、あらかじめすべてのオブジェクトをメモリ上に確保しておき、非表示にするなどして、ゲーム上機能しないがメモリにある状態にしておきます。  
オブジェクトプールのInstantiateは、IObjectPoolを実装するクラスに、生成時の状態を定義するよう求めます。これはnewとは違い、既に生成されたオブジェクトを、「使える」状態にすることを意味しています。  

このOblectPoolクラスを使用し、Bulletを作ると、高速にBulletを作ることができます。直接ObjectPoolを使ってもいいと思いますが、生成をもっと抽象化して、以下のように生成メソッドを切り替えると良い実装になると思います。  
これは、BuilderにStorategyのような生成戦略パターンを適用する事例になります。  

```
/// <summary>
/// 弾を作る
/// </summary>
/// <param name="id"></param>
/// <returns></returns>
static public T Build<T>(GameObject base, MasterCube master) where T : Bullet, IObjectPool
{
    //通常
    //var obj = GameObject.Instantiate(base);
    var obj = ObjectPoolList.getPool<T>().Instantiate(); //実装は省略します
    var blt = obj.GetComponent<T>();
    blt.Setup();
    return blt;
}
```


## Builder - 設定を隠蔽する
個人的には、UnityではFactoryパターンよりも使用する実装です。  
設計的にはFactory Methodとも関連します。  

原著ではBuilderクラスを用意し、パラメータの設定をBuilderに投げるのですが、同クラスに持たせるとsetterが必要ないというメリットがあります。  
以下のように、クラス名を一致させてstaticな公開関数として持たせると便利です。  

````
/// <summary>
/// ブロックを作る(idから)
/// </summary>
/// <param name="id"></param>
/// <returns></returns>
static public MonoBlock Build(int id, MasterCube master)
{
    var obj = Instantiate(ResourceCache.GetCube(id));
    var block = obj.GetComponent<MonoBlock>();
    block.MasterCube = master;
    block.Setup();
    return block;
}
```

継承クラスを生成するこの実装は、コンストラクタでは表現できません。  
なぜなら、実装者はMonoBehaviourを継承したスクリプトを単体で作りたいのではなく、MonoBehaviourがついているGameObject(Prefabのコピー)を作りたいからです。  
よって、GameObject.Instantiateを実行したのち、必要な変数を設定、その後Setup関数を呼び出すことで、オブジェクトの安全な生成を提供します。  

複雑なオブジェクトは引数が多くなりがちですが、ゲームでは対応するデータがあらかじめ決まっていることもあり、idを渡してBuilder内部で値を解釈して作ってもらうことが多くなると思います。  
なので、このようにすっきりとしたビルド関数になることは多いのではないかと思います。  


## デザインパターン本との実装との違い
実際のところ、本で書かれているものとは結構異なります。なので、自分でもデザインパターン通りなのかな？と思いながら書いていました。  
※とくにBuilderは、「自分のことをBuilderパターンだとだと思っているstatic関数」の可能性が高いです。  

ただまあデザインパターンも元は古い話なので、現代において厳密な正解はないと思います。  
自分でも色々調べてみるといいでしょう。  
将来Code Completeなどを見て、より使いこなせるようになれるといいでしょう。  
