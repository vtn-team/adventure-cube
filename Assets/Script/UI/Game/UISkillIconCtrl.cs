using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UISkillIconCtrl : UIPane
{
    [SerializeField] UISkillIcon BaseObject;
    List<UISkillIcon> AutoAttackIconList = new List<UISkillIcon>();
    List<UISkillIcon> SkillAttackIconList = new List<UISkillIcon>();

    public void Setup()
    {
        //一回だけ通る処理があれば書きたい
        IconCreate();
    }

    public void IconCreate()
    {
        AutoAttackIconList.ForEach(icon => GameObject.Destroy(icon));
        SkillAttackIconList.ForEach(icon => GameObject.Destroy(icon));

        //プレイヤーの攻撃キューブをひろって再生成する
        Player player = GameManager.GetPlayableChar();
        player.AttackCubes.ForEach(c =>
        {
            UISkillIcon Obj = GameObject.Instantiate(BaseObject, this.transform);
            ICooldownTimer timer = c as ICooldownTimer;
            Obj.SetTimerObject(timer);
            AutoAttackIconList.Add(Obj);
        });

        player.SkillCubes.ForEach(c =>
        {
            UISkillIcon Obj = GameObject.Instantiate(BaseObject, this.transform);
            ICooldownTimer timer = c as ICooldownTimer;
            Obj.SetTimerObject(timer);
            Obj.RectTransform.localPosition += new Vector3(120, 0, 0);
            SkillAttackIconList.Add(Obj);
        });
    }
}
