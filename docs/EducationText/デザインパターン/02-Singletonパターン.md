# Singletonパターン
だれもがどのプロジェクトで1回は使うであろうパターンです。


## Singletonとは
シングルトンとは、「一つしか存在が許されない」、ということを保証する実装です。

そのため、以下の実装を保証する必要があります。
- 同じ型のインスタンスが private なクラス変数として定義されている。
- コンストラクタの可視性が private である。
- 同じ型のインスタンスを返す getInstance() がクラス関数として定義されている。

よって、C#であれば以下のような実装を持ちます。

```
private LifeCycleManager instance = new LifeCycleManager();     // privateなクラス変数

private LifeCycleManager(){} //privateなコンストラクタ

public LifeCycleManager Instance => instance; // インスタンスを返す関数
```


## グローバル変数としてのSingleton
Singletonはその特性上、staticにして、どのソースコードからも参照可能なものとして組まれることが多いです。  
つまり、グローバル変数のような使い方をすることができます。  

ただし、グローバル変数の多用はのちのち設計に悪い影響を与える要素が多いです。  
(修正の影響範囲が大きい、オブジェクト処理のフローが多数できることにより、処理が衝突する、など)  

まあとはいえ便利で使いやすいのと、Facadeパターンなどと組み合わせるSingletonは処理の見通しもよくなります。  
変数の参照が必要なために設計を変更したい、と感じたときは採用を検討してみましょう。  
とくに、プレイヤーや敵の情報はどこからでも参照できて問題ないはずです。  


## グローバルアクセス可能なシングルトンのデザイン

```
static private LifeCycleManager instance = new LifeCycleManager();     // privateなクラス変数

private LifeCycleManager(){} //privateなコンストラクタ

static public LifeCycleManager Instance => instance; // インスタンスを返す関数

//呼び出されるメンバ関数をstaticにすることで、冗長なアクセスを軽減するテクニックもあります。
//LifeCycleManager.Instance.AddUpdate()が、LifeCycleManager.AddUpdate()で書けます。
static public void AddUpdate()
{
  instance.addUpdate
}
void addUpdate()
{

}
```
参考にしてみてください。
