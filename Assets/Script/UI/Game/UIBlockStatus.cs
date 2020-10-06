using UnityEngine;
using System.Collections;
using TMPro;

using Block;

/// <summary>
/// キューブの情報を表示するUI
/// 
/// NOTE: キューブをもらって更新する
/// </summary>
public class UIBlockStatus : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CubeName; //キューブ名

    public void UpdateCube(MonoBlock cube)
    {

    }
}
