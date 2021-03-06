﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Damage : UIPane, IObjectPool
{
    [SerializeField] AnimationCurve AnimCurve = null;

    Text Text;
    float Timer = 0.0f;
    Vector3 MovVec = new Vector3(0, 0.2f, 0);
    Vector3 Mov = Vector3.zero;
    Vector2 Random = Vector2.zero;
    GameObject Target = null;

    protected override void Setup()
    {
        Text = GetComponent<Text>();
    }

    // Update is called once per frame
    void UnityUpdate()
    {
        if (Renderer.cull) return;
        if (Target == null)
        {
            Destroy();
            return;
        }

        Timer += Time.deltaTime;

        Mov += MovVec * AnimCurve.Evaluate(Timer);
        RectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, Target.transform.position) + Random;
        RectTransform.position += Mov;

        if (Timer > 1.0f)
        {
            Destroy();
        }
    }

    public void Set(GameObject go, int dmg)
    {
        Target = go;
        Text.text = dmg.ToString();
    }

    public void SetColor(Color col)
    {
        Text.color = col;
    }

    //オブジェクトプールの実装
    public bool IsActive => !Renderer.cull;

    public void DisactiveForInstantiate()
    {
        Renderer.cull = true;
        LifeCycleManager.AddUpdate(UnityUpdate, this.gameObject, 0);
    }

    public void Create()
    {
        Timer = 0.0f;
        Mov = Vector3.zero;
        Random = new Vector2(UnityEngine.Random.Range(-10.0f, 10.0f), UnityEngine.Random.Range(-5.0f, 5.0f));
        Renderer.cull = false;
    }

    public void Destroy()
    {
        Renderer.cull = true;
    }
}
