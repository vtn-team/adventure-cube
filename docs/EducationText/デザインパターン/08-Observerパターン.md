# Observerパターン
購読型と呼ばれるパターンです。  
無意識に使っていることが多いパターンです。  
このデザインパターンはイベントドリブンという考え方と近しく、何かが入力されたり、何かの処理が終わった時に、イベントを呼び出して、処理を実行するという考え方です。  
ゲームでは、UIやアニメーションなどで多く使われます。  


## UniRxとObserverパターン
Observerパターンは、リアクティブプログラミングという考え方のベースの実装にもなっています。  
UniRxに優れたObserverの実装があるので、より使い込みたい人はそちらを使用するといいと思います。  


## シンプルなオブザーバー実装

通知する人
```
public interface IObservable<T>
{
    void AddObserver(IObserver<T> target);
    void DeleteObserver(IObserver<T> target);
    void NotifyObserver(T obj);
}
```

通知を受ける人
```
public interface IObserver<T>
{
    void NotifyUpdate(T obj);
}
```


## adventure-cubeのObserver使用例
キー入力にObserverパターンを使ってみました。

Observerパターンは、通知を送るという挙動をうまく活用し、複数の異なるクラス型のオブジェクトに対して一括して処理を実行させたい、というパターンで使用できます。  
今回の例では、所持しているキューブから必要に応じてオブザーバー登録し、イベント発生時に登録オブジェクト全員に対してキー入力を通知します。  
スキルキューブの実装で使用しています。  


GameManagerが通知を行います。オブザーバー管理クラスを作り、内部にリストを持っています。クラスを介して登録を受け付けられるようにしています。  
ボタンイベントが起きたら、NotifyObserverを発行して、すべての登録オブジェクトにInputDataオブジェクトを送信します。  
```
GameManager.cs

public InputObserver InputObs { get; private set; }

void Update()
{
    string[] ButtonLabels = { "Fire1", "Fire2"};
    foreach (var label in ButtonLabels)
    {
        if (Input.GetButtonDown(label))
        {
            InputObs.NotifyObserver(InputObserver.CreateInput(label));
        }
    }
}
```

スキルキューブの実装です。  
生成時にオブザーバー登録を行って、死んだときに登録解除しています。  

NotifyUpdateが通知されたとき(ボタンが押されたとき)、攻撃可能であればショットを生成します。  
```
SkillShooter.cs

protected override void Setup()
{
    GameManager.Instance.InputObs.AddObserver(this);
}

public override void BreakDown()
{
    GameManager.Instance.InputObs.DeleteObserver(this);
}

public void NotifyUpdate(InputObserver.InputData input)
{
    if (input.Type == InputObserver.InputType.Skill && IntervalTimer.IsAttackOK)
    {
        Skill();
        IntervalTimer.ResetTimer(false);
    }
}
```


## 楽をしたいアニメーション処理
adventure-cubeでの実装はありません。  

たとえば、アニメーションが終わったときに、別のモーションを呼び出す処理を書きたい…として、updateで常時「モーション再生が終わったかどうか」を検知するループ処理を書いて、切り替え処理をループ内で呼び出す、という形で実装はできます。  

ただこの処理は、必ずupdateを中継する分処理が飛び飛びになるので、分岐ケースが多いと見づらくなったり、バグも多くなりそうですよね。  
そういう時に、モーションを再生している場所で、「終わった時をにこういうことをしてくれ」という処理をあわせて書けると、見通しが良くなるというわけです。


### 参考ソース
[IObservable.cs](https://github.com/vtn-team/adventure-cube/blob/develop/Assets/Script/Foundation/Interface/IObservable.cs)  
[IObserver.cs](https://github.com/vtn-team/adventure-cube/blob/develop/Assets/Script/Foundation/Interface/IObserver.cs)  
[InputObserver.cs](https://github.com/vtn-team/adventure-cube/blob/develop/Assets/Script/Game/System/InputObserver.cs)  
[GameManager.GameManagement.cs](https://github.com/vtn-team/adventure-cube/blob/develop/Assets/Script/Game/GameManager.GameManagement.cs)  