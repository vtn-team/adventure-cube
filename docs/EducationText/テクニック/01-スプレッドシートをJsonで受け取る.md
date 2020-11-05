# スプレッドシートをJsonで受け取る

### 1. 新規のスプレッドシートを作成しましょう  

Google Driveを開く→新規→新規スプレッドシート  


### 2. スクリプトを記述しましょう  

シート上部のツール→>スクリプトエディタを選択してください。  
※Googleに複数のアカウントでログインしていた際、エラーになることがあるので注意してください。  

以下のスクリプトを参考に記入してください。  
Idは自分のシートのものを入力してください。  
自分のプロジェクトに応用する際は、適宜修正してください。  

```
function doGet(param) {
  var id = "[URLのd/のあとの文字列]";
  
  //シート名を指定してJsonを返す。シート名はリクエストの後ろに?shhet=xxxxの形で指定する。シート名が付与されていなかった場合、Cubeマスタを返す
  var sheetName = "Cube";
  if(param && param.parameter && param.parameter.sheet)
  {
    sheetName = param.parameter.sheet;
  }
  
  //シートを拾ってくる
  var sheet = SpreadsheetApp.openById(id).getSheetByName(sheetName);
  var rows = sheet.getDataRange().getValues();
  var keys = [];
  
  //データ開始行を検索する。B列が空欄の場所はコメントや説明行になる。
  //データ開始行は、Jsonのキーになる。
  var index = 0;
  for(index=0; index<rows.length; ++index)
  {
    if(rows[index][1] == "") continue;
    keys = rows[index];
    break;
  };
  
  //1行目のF列にバージョンが入っているので取得する
  var version = rows[0][5];
  
  //キー列のあと、データ列だけを切り出す
  rows.splice(0, index+1);
  var datum = rows.map(function(row) {
    var obj = {};
    row.map(function(item, index) {
      if(String(keys[index]) == "") return;
      if(String(keys[index]).indexOf("_") == 0) return;
      obj[String(keys[index])] = String(item);
    });
    return obj;
  });
  
  //バージョン情報を付ける感じで成形
  var json = {
    Version: version,
    Data: datum
  };
  
  //jsonデータを文字列にして返す
  return ContentService.createTextOutput(JSON.stringify(json, null, 2))
  .setMimeType(ContentService.MimeType.JSON);
}
```

### 3. Web Appとして公開する

スクリプトエディタのメニューから、  
公開→ウェブアプリケーションとして導入を選択してください。  

以下の設定の部分を変更してください。  

- Project versionを「New」に
- Execute the app asを「Me(自分のメールアドレス)」に
- Who has access to the appを「Anyone, even anonymos」に

変更したら更新を押します。


### 4. スクリプトを修正した場合

スクリプトエディタのメニューから、再度、  
公開→ウェブアプリケーションとして導入 を選択してください。  

Project versionを「New」にして、更新ボタンを押します。  


## 通信

### 型を気にせず受け取りたい場合

dynamicと、Utf8Jsonというアセットを使います  
※ほかのアセットでもいいですが、これが一番いいと思います。  

UTF8Json  
https://www.fast-system.jp/unity-utf8json-howto/  
をプロジェクトにImportしてください。  

```
Network.WebRequest.Request<Network.WebRequest.GetDynamic>("https://script.google.com/macros/s/AKfycbyc6WmX57vj8_V5tRL7eN4QCWMcLUQx8Jtu_B_JyqnMRGxH0Uk/exec?sheet=Cube", Network.WebRequest.ResultType.Json, (dynamic json) =>
{
  Debug.Log(json["Data"][0]["Id"]);
});
```


### 型を気にする場合

JsonUtilityを使ってクラスにします。  
```
Network.WebRequest.Request<Network.WebRequest.GetString>("https://script.google.com/macros/s/AKfycbyc6WmX57vj8_V5tRL7eN4QCWMcLUQx8Jtu_B_JyqnMRGxH0Uk/exec?sheet=Cube", Network.WebRequest.ResultType.String, (string json) =>
{
    var data = JsonUtility.FromJson<MasterData.MasterDataClass<MasterData.Cube>>(json);
    Debug.Log(data.Data[0].Id);
});
```


## 参考文献

このあたりを参考にしています。  

Google Spreadsheet のデータを JSON 形式で取得する Web API をサクッと作る  
https://qiita.com/takatama/items/7aa1097aac453fff1d53  

Googleスプレッドシートの中身を外部からAPIを使わずJSON形式で取得する方法  
http://pineplanter.moo.jp/non-it-salaryman/2019/11/07/google-sp-json/  

Google Sheets API v4を適当に叩いて適当にデータをJSONで取得する  
https://taiki-t.hatenablog.com/entry/2016/10/14/031124  