# Storategyパターン
Storategyパターンは、アルゴリズムや実装を切り替えることができるパターンです。  
関心の分離と相性のいいデザインパターンといえます。  


## 用途
ゲームで、キャラクターについているスキルは切り替えることができ、すべてではなく装備されているものが実行されるパターンがあります。  
そういった用途に使用できます。  

また、敵を検索するなどのアルゴリズムにおいても、サブルーチンやクラスを切り替えることで実装できると楽ですよね。  


## adventure-cubeでの実装
ダメージ処理のクラスと、ターゲットヘルパーが該当します。  


## ダメージ処理クラス - DamageCaster

```
/// <summary>
/// ダメージ計算器
/// </summary>
public class DamageCaster
{
    //ダメージ評価(確定はしない)
    static public DamageSet Evaluate(AttackSet atkSet)
    {
      //ここですべてのダメージ処理を行う
    }

    //ダメージを確定する
    static public void CastDamage(DamageSet dmg)
    {
      //
    }
}
```
ダメージ処理は、今のところは1種ですが、継承して置き換えが可能なクラスであってもよいと思います。  
評価システムは、例えばスキル使用時に画面を止めるなどの処理を行う際に役に立ちます。  
AIの処理に組み込むこともできると思います。あと、カードゲームにも予告処理はありますよね。  


## ターゲットヘルパー
```
public class TargetHelper
{
    public enum SearchLogicType
    {
        RandomOne,
        NearestOne
    };

    static public MasterCube SearchTarget(MasterCube from, SearchLogicType logic)
    {
        List<MasterCube> targetList = null;
        if (from.FriendId == 1) targetList = GameManager.GetEnemyList();
        if (from.FriendId == 2) return GameManager.GetPlayableChar();

        if (targetList == null) return null;
        if (targetList.Count == 0) return null;

        MasterCube target = null;
        switch(logic)
        {
            case SearchLogicType.RandomOne:
                target = targetList[UnityEngine.Random.Range(0, targetList.Count)];
                break;

            case SearchLogicType.NearestOne:
                {
                    float length = -1;
                    foreach (var t in targetList)
                    {
                        if (length != -1 && length < (t.transform.position - from.transform.position).magnitude) continue;
                        length = (t.transform.position - from.transform.position).magnitude;
                        target = t;
                    }
                }
                break;
        }
        return target;
    }
}
```

攻撃対象を決定する関数です。  
LogicTypeによって帰るものが変わります。  
また必要に応じてSearchTargetと似たような別の処理を作り、関数を切り替えることもできます。  

サブルーチン(関数)レベルで切り替えが可能な処理は、このようにヘルパーとして切り出すと汎用性が上がります。  
ヘルパーは厳密にはStorategyパターンではありませんが、このような検索処理をクラス内部に埋め込む必要があるときは、アルゴリズムを切り替えられるように意識できるとよいでしょう。
