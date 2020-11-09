#　Facadeパターン
ファサードと読むようです。  
Facadeパターンは、機能をひとつまとめるSingletonのようなクラスを作って、コード上のまとまりを良くしようという試みで使用します。  

## UIまとめ役としてのFacade

よく使っていたのが、ゲーム中のデータのまとめ役、UIのまとめ役としてのFacadeです。  

データのまとめ役は
```
public class　GameParameter
{
  PlayerParam player;
  static public PlayerParam Player => player;
}
```
