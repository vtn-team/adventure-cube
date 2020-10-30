using UnityEngine;
using System.Collections;

/// <summary>
/// ダメージ数字をポップアップさせる
/// 
/// NOTE: 3D座標から2D座標に変換している
/// </summary>
public class DamagePopup : MonoBehaviour
{
    [SerializeField] Damage DamageTemplate = null;

    static DamagePopup Instance = null;

    private void Awake()
    {
        Instance = this;
    }

    static public void Pop(GameObject go, int dmg, Color col)
    {
        var obj = Instantiate(Instance.DamageTemplate.gameObject, Instance.transform.parent);
        var rt = obj.GetComponent<RectTransform>();
        rt.position = RectTransformUtility.WorldToScreenPoint(Camera.main, go.transform.position);

        var damage = obj.GetComponent<Damage>();
        damage.Set(go, dmg);
        damage.SetColor(col);
    }
}
