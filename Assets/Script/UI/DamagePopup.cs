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
    ObjectPool<Damage> DamegeUIPool = new ObjectPool<Damage>();

    private void Awake()
    {
        Instance = this;
        DamegeUIPool.SetBaseObj(DamageTemplate, Instance.transform);
        DamegeUIPool.SetCapacity(10);
    }

    static public void Pop(GameObject go, int dmg, Color col)
    {
        Damage damage = Instance.DamegeUIPool.Instantiate();//Instantiate(Instance.DamageTemplate.gameObject, Instance.transform.parent);
        damage.RectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, go.transform.position);
        damage.Set(go, dmg);
        damage.SetColor(col);
    }
}
