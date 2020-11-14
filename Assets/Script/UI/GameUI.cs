using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GameUI
{
    static public GameUI Instance => instance;
    static GameUI instance = new GameUI();
    private GameUI() { }

    Canvas CanvasRoot = null;
    UISkillIconCtrl iconCtrl = null;
    static public UISkillIconCtrl IconCtrl => instance.iconCtrl;

    public void Setup()
    {
        CanvasRoot = GameObject.Find("/2DCanvas")?.GetComponent<Canvas>();

        //UIを作る
        var go = GameObject.Instantiate(ResourceCache.GetCache(ResourceType.UI, "IconCtrl"), CanvasRoot.transform);
        iconCtrl = go.GetComponent<UISkillIconCtrl>();
        iconCtrl.RectTransform.localPosition = new Vector3(450, -250, 0);
        iconCtrl.Setup();
    }
}
