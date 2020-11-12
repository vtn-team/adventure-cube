using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(CubeSheet)), CanEditMultipleObjects]
public class CubeSheetUpdator : Editor
{
    CubeSheet sheet;

    /// <summary>
    /// InspectorのGUIを更新
    /// </summary>
    public override void OnInspectorGUI()
    {
        sheet = target as CubeSheet;

        //更新ボタンを表示
        if (GUILayout.Button("スプレッドシートから更新"))
        {
            Network.WebRequest.Request<Network.WebRequest.GetString>("https://script.google.com/macros/s/AKfycbyc6WmX57vj8_V5tRL7eN4QCWMcLUQx8Jtu_B_JyqnMRGxH0Uk/exec?sheet=Cube", Network.WebRequest.ResultType.String, (string json) =>
            {
                var dldata = JsonUtility.FromJson<MasterData.MasterDataClass<MasterData.Cube>>(json);
                Debug.Log("Update:" + dldata);

                //データからリストを作る
                List<CubeSheet.CubeAsset> AssetList = new List<CubeSheet.CubeAsset>();
                foreach(var d in dldata.Data)
                {
                    CubeSheet.CubeAsset cs = new CubeSheet.CubeAsset();
                    cs.Id = d.Id;
                    cs.Key = d.Prefab;
                    cs.Block = Resources.Load<Block.MonoBlock>("Blocks/" + d.Prefab);
                    AssetList.Add(cs);
                }

                //更新
                sheet.SetData(AssetList);

                // 保存
                EditorUtility.SetDirty(sheet);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            });
        }

        //元のInspector部分を表示
        base.OnInspectorGUI();
    }
}