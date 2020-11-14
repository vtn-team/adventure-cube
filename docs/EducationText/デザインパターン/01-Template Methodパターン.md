# Template Methodパターン
継承を使用する際、基本となるパターンです。


## 仮想関数を使用した処理の実装
 モデル図である程度きまった「処理」のめどがついたら、このパターンを使って、おおまかな実装をするという手段をとります。  

よく使用される処理として、
- 初期化
- 攻撃
- 移動

などが考えられるでしょうか？

まずは基底クラスに、abstractか、virtualな処理を書きましょう。  
adventure-cubeでは、次のような関数が当てはまります。  
```
public class MonoBlock : MonoBehaviour
{
    void Awake()
    {
        Life = life;
        Figure = figure;
    }

    protected virtual void Setup()
    {

    }

    public virtual bool IsAlive()
    {
        return Life > 0;
    }

    public virtual void Damage(int dmg)
    {
        //ダメージのとき
        if (dmg > 0)
        {
            Life -= dmg;
        }
    }

    public virtual void BreakDown()
    {
      //こわれたとき
    }
}
```

セットアップ、生きているかどうか、ダメージ処理、破壊時処理は、キューブごとに変えることが出来そうですよね。  

たとえば、考えている実装では、「超固い盾」を作ろうとしていました。
ダメージを全部肩代わりしてくれて、無敵だけど、一定時間で消滅する、みたいなやつです。  

これはダメージ処理と生存チェック関数を修正すれば実装できますよね。  

```
public class DivineShield : MonoBlock
{
    void Awake()
    {
        Life = life;
        Figure = figure;

        Timer = Figure;
    }

    public override bool IsAlive()
    {
        return Timer > 0;
    }

    public override void Damage(int dmg)
    {
        //ダメージはない
    }

    void Update()
    {
        Timer -= Time.deltaTime;
    }
}
```

こんな感じです。


## 応用範囲の広いパターン
Template Methodパターンは広く応用可能な、基礎的なデザインパターンです。
モデル図で関連図を考えたあと、どのクラスにこのパターンを適用すればいいかを考えてみてください。  
