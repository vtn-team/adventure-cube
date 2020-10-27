# メタプログラミング
実務で覚えておくと便利な、「プログラムで抽象を扱う処理」についてです。  


## メタプログラミングとは？
メタプログラミングとは、プログラムをプログラムを使って作りだす、という行為を指します。  
プログラムコードを抽象化する、というやり方ともいえます。  

具体的に、みんな使っているもので、`List<T>`とかが該当します。  
この`<T>`っていう部分が抽象化されている部分で、「好きな型を入れろよ」という意味を持っています。  


## 同じコードを書きたくない
メタプログラミングですが、なぜやるかというと、  
型や一部の処理が違うだけの処理を複数回書きたくないからです。  

これはList<T>みたいな簡単な処理から、キャラクターの管理クラスなどの複雑なクラスに対しても使用することができます。  


## whereって何？
**型制約**といいます。  
入る型を抽象化したいけど、限度はあるよね…stringとかintとかじゃなくて、もっとゲーム中のオブジェクトだけで考えたいです…みたいなクラスに対して、型を制限してメタプログラミングする実装です。  

具体的には、
```
CacheTemplate.cs
public class CacheTemplate<T> where T : UnityEngine.Object
{
}
```
や  
```
MonoBlock.cs
static public T Build<T>(BlockType type, int index, MasterCube master) where T : MonoBlock
{
}
```
等の処理が該当します。  

```
where T : 型名
```
をクラス名か、関数名の後ろにつけて使用します。


## ゲーム中の実装から確認するメタプログラミングの便利な使い方

さきほど例に挙げた2つの処理が該当します。それぞれ解説しましょう。  

まず、クラス名に<T>をつけているこのクラスです。  
```
CacheTemplate.cs
public class CacheTemplate<T> where T : UnityEngine.Object
{
    Dictionary<string, T> Cache = new Dictionary<string, T>(); //キャッシュ辞書

    /// <summary>
    /// Prefabを取得する
    /// NOTE: キャッシュが無かったら探してきて読み込む
    /// </summary>
    /// <param name="path">アセットやPrefabのパス</param>
    /// <returns>アセットやPrefab</returns>
    public T GetCache(string path)
    {
        if (!Cache.ContainsKey(path))
        {
            Cache.Add(path, Resources.Load<T>(path));
        }
        return Cache[path];
    }
}
```
このクラスは、Resourcesにあるさまざまな型のPrefabをキャッシュするためのクラスです。  
Prefabには、MonoBlockや、Materialが入る予定があります。  
それをイチイチ書いていると面倒ですし、型が違うだけなのでまとめられそうですよね。  

また、将来的にやる予定でやってないものとして、AssetBundle対応というのがUnityにはあります。  
このとき、基本的にはResources.Loadは使わなくなっていきます。  

参考)  
https://qiita.com/k7a/items/df6dd8ea66cbc5a1e21d  

つまり、AssetBundle対応をやるとき、Rersources.Loadしている場所はすべて影響を受けます。  
なので、今後の対応のためにも、読み込み部分はまとまっていると楽ですよね。  


次に、MonoBlockのビルダーです。  
ビルダーとは何ぞや？という人は、次にやる[デザインパターン](/EducationText/デザインパターン/01-Builderパターン.md)編を見てください。  
```
MonoBlock.cs
static public T Build<T>(BlockType type, int index, MasterCube master) where T : MonoBlock
{
  var prefab = ResourceCache.GetCache(ResourceType.MonoBlock, type.ToString());
  var obj = GameObject.Instantiate(prefab);
  var block = obj.GetComponent<T>();
  block.MasterCube = master;
  block.Type = type;
  block.Index = index;
  block.Setup();
  return block;
}
```
このコード、実はジェネリックで型を受け取る意味はそこまでないです。  
よって制約をつけている意味も実はそこまでない(すべてMonoBlockで直打ちしても通る処理)んですが、型制約をつけることで、どのブロックを作ったのかというのがコード上で確認しやすくなります。  
