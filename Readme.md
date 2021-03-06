# Adventure Cube
キューブと旅をするゲームです  
教材的な役割を持ったゲームです。  
作っている途中ですが、ある程度は説明できそうなので展開します。  


# ゲーム仕様

## 目的
キューブを手に入れつつ、敵を倒したりうまく回避したりしてゴールを目指そう。  
ゴールは複数あるよ。  


## 操作
・左クリックで移動  
・通常攻撃はオート(選択UIあり)  
・右クリック+選択UIでスキル  

※そのうちコントローラにも対応予定  


## キャラクター
いろいろな種族を選べる。  
種族がもつ「コア」や「キューブ装備数」の違いによって、戦略が変化する。  

## キューブ
各キューブは必ず1つの数字を持っていて、その数字に何かの意味がある。  
この数字を「フィギュア」と呼ぶ。figure: 数字/形  

数字に伴い何かの効果が発揮される。  
通常攻撃やスキルなども、全部キューブの効果によって発生する。  
組み合わせ次第でゲーム展開が色々変化する。  

### コアキューブ
中心になるキューブ。  
破壊されるとキャラクターは死亡する。  
自分のキャラクターのコアが破壊されるとゲームオーバーとなる。  

### アタックキューブ
オート攻撃をするキューブ。  

### スキルキューブ
スキル攻撃をするキューブ。

### シールドキューブ
敵からのダメージを肩代わりしてくれるキューブ。  
通常ダメージはコアに入るが、このキューブを持っていると、コアは無傷で済む。  
多くの場合ダメージを肩代わりできる数がfigureになる。  

### パッシブキューブ
プレイヤーのステータスを底上げしたり、バフをかけたりする。  


# その他資料
開発用の仕様、説明は[こちら](/docs/DevDoc/index.md)  
授業で使用する解説用資料は[こちら](https://vtn-team.github.io/adventure-cube/)
