# Jsonを扱うやり方応用

Jsonにはいろいろな使い方があります。


## JsonUtility

使ったことある人は多いと思います。

https://www.fast-system.jp/unity-utf8json-howto/

http://pineplanter.moo.jp/non-it-salaryman/2019/11/07/google-sp-json/
https://taiki-t.hatenablog.com/entry/2016/10/14/031124



function doGet(param) {
  var id = "1_wQnx9FGCRz2-cVT1fpzTFTcXGA9gTcyq4-R8CuiRQA";
  var sheetName = "Cube";
  if(param && param.parameter && param.parameter.sheet)
  {
    sheetName = param.parameter.sheet;
  }
  var sheet = SpreadsheetApp.openById(id).getSheetByName(sheetName);
  var rows = sheet.getDataRange().getValues();
  var keys = rows.splice(0, 1)[0];
  var json = rows.map(function(row) {
    var obj = {};
    row.map(function(item, index) {
      obj[String(keys[index])] = String(item);
    });
    return obj;
  });
  
  return ContentService.createTextOutput(JSON.stringify(json, null, 2))
  .setMimeType(ContentService.MimeType.JSON);
}

https://qiita.com/takatama/items/7aa1097aac453fff1d53