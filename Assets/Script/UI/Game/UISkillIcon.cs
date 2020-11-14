using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UISkillIcon : UIPane
{
    enum BlockType
    {
        None,
        Auto,
        Skill
    }

    [SerializeField] UnityEngine.UI.Image CooldownUI;

    ICooldownTimer Timer = null;
    protected override void Setup()
    {
        LifeCycleManager.AddUpdate(UnityUpdate, this.gameObject, 0);
    }

    public void SetTimerObject(ICooldownTimer timer)
    {
        Timer = timer;
        Renderer.cull = false;
    }

    void UnityUpdate()
    {
        CooldownUI.fillAmount = Timer.CurrentInterval;

        //Maxだったら何かする？
    }
}
